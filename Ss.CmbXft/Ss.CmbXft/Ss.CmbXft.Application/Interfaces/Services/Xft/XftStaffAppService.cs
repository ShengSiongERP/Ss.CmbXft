using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.Extensions.Logging;
using Ss.CmbXft.Application.Dtos;
using Ss.CmbXft.Application.Dtos.Xft;
using Ss.CmbXft.Common.Models;
using Ss.CmbXft.Domain.Entities;
using Ss.CmbXft.Domain.Repositories;
using Yitter.IdGenerator;

namespace Ss.CmbXft.Application.Services;

/// <summary>
/// 员工应用服务实现
/// </summary>
public class XftStaffAppService : IXftStaffAppService
{
    private readonly IRepository<XftStaff, long> _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<XftStaffAppService> _logger;

    public XftStaffAppService(
        IRepository<XftStaff, long> repository,
        IUnitOfWork unitOfWork,
        ILogger<XftStaffAppService> logger)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task<PageResult<XftStaffDto>> GetPageAsync(XftStaffQueryDto query)
    {
        ArgumentNullException.ThrowIfNull(query);

        // 构建过滤表达式
        var result = await _repository.GetPagedListAsync(
            predicate: r =>
                (string.IsNullOrWhiteSpace(query.StaffSeq) || r.StaffSeq!.Contains(query.StaffSeq)) &&
                (string.IsNullOrWhiteSpace(query.StfName) || r.StfName!.Contains(query.StfName)) &&
                (string.IsNullOrWhiteSpace(query.MobileNumber) || r.MobileNumber!.Contains(query.MobileNumber)) &&
                (string.IsNullOrWhiteSpace(query.StfType) || r.StfType == query.StfType) &&
                (string.IsNullOrWhiteSpace(query.StfStatus) || r.StfStatus == query.StfStatus) &&
                (string.IsNullOrWhiteSpace(query.EnterpriseId) || r.EnterpriseId == query.EnterpriseId) &&
                !r.IsDeleted,
            sorting: "CreateTime DESC, Id DESC",
            pageIndex: query.PageIndex,
            pageSize: query.PageSize
        );

        var items = result.Items.Adapt<List<XftStaffDto>>();
        return PageResult<XftStaffDto>.Create(result.TotalCount, result.PageIndex, result.PageSize, items);
    }

    /// <inheritdoc />
    public async Task<List<XftStaffDto>> GetListAsync()
    {
        var staffList = await _repository.GetListAsync(predicate: s => !s.IsDeleted, orderBy: null);

        return staffList.OrderByDescending(s => s.CreateTime).Adapt<List<XftStaffDto>>();
    }

    /// <inheritdoc />
    public async Task<XftStaffDto?> GetAsync(long id)
    {
        var staff = await _repository.GetByIdAsync(id);
        if (staff == null || staff.IsDeleted) return null;

        return staff.Adapt<XftStaffDto>();
    }

    /// <inheritdoc />
    public async Task<XftStaffDto> CreateAsync(XftStaffSaveDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        // 检查员工序号唯一性
        var exists = await _repository.FirstOrDefaultAsync(s => s.StaffSeq == dto.StaffSeq && !s.IsDeleted);
        if (exists != null)
        {
            throw new InvalidOperationException($"员工序号 '{dto.StaffSeq}' 已存在");
        }

        var staff = dto.Adapt<XftStaff>();
        staff.Id = YitIdHelper.NextId();
        staff.CreateTime = DateTime.Now;

        await _repository.AddAsync(staff);
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("创建员工成功: {StaffSeq} - {StfName}", staff.StaffSeq, staff.StfName);

        return staff.Adapt<XftStaffDto>();
    }

    /// <inheritdoc />
    public async Task<XftStaffDto> UpdateAsync(long id, XftStaffSaveDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        var staff = await _repository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"员工不存在（ID: {id}）");

        if (staff.IsDeleted)
        {
            throw new InvalidOperationException($"员工已被删除（ID: {id}）");
        }

        // 检查员工序号唯一性（排除自身）
        if (dto.StaffSeq != staff.StaffSeq)
        {
            var exists = await _repository.FirstOrDefaultAsync(s => s.StaffSeq == dto.StaffSeq && !s.IsDeleted && s.Id != id);
            if (exists != null)
            {
                throw new InvalidOperationException($"员工序号 '{dto.StaffSeq}' 已存在");
            }
        }

        dto.Adapt(staff);
        staff.UpdateTime = DateTime.Now;

        _repository.Update(staff);
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("更新员工成功: {StaffId} - {StfName}", staff.Id, staff.StfName);

        return staff.Adapt<XftStaffDto>();
    }

    /// <inheritdoc />
    public async Task DeleteAsync(long id)
    {
        var staff = await _repository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"员工不存在（ID: {id}）");

        if (staff.IsDeleted)
        {
            _logger.LogWarning("员工已被删除: {StaffId}", id);
            return;
        }

        staff.IsDeleted = true;
        staff.UpdateTime = DateTime.Now;

        _repository.Update(staff);
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("删除员工成功: {StaffId} - {StfName}", staff.Id, staff.StfName);
    }

    /// <inheritdoc />
    public async Task DeleteBatchAsync(List<long> ids)
    {
        ArgumentNullException.ThrowIfNull(ids);
        if (ids.Count == 0) return;

        var staffList = await _repository.GetListAsync(predicate: s => ids.Contains(s.Id), orderBy: null);
        var toDelete = staffList.Where(s => !s.IsDeleted).ToList();

        if (!toDelete.Any())
        {
            _logger.LogInformation("没有需要删除的员工");
            return;
        }

        foreach (var staff in toDelete)
        {
            staff.IsDeleted = true;
            staff.UpdateTime = DateTime.Now;
        }

        _repository.Update(toDelete);
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("批量删除员工成功: {Ids}", string.Join(",", ids));
    }
}
