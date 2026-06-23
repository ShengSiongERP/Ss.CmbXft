using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Ss.CmbXft.Application.Dtos.Sserp;
using Ss.CmbXft.Common.Models;
using Ss.CmbXft.Domain.Entities.Sserp;
using Ss.CmbXft.Domain.Repositories;

namespace Ss.CmbXft.Application.Services;

/// <summary>
/// 员工信息应用服务实现
/// </summary>
public class EmployeeAppService : IEmployeeAppService
{
    private readonly ISserpRepository<SserpERPTxnEmployee, string> _employeeRepository;
    private readonly ISserpUnitOfWork _unitOfWork;
    private readonly ILogger<EmployeeAppService> _logger;

    public EmployeeAppService(
        ISserpRepository<SserpERPTxnEmployee, string> employeeRepository,
        ISserpUnitOfWork unitOfWork,
        ILogger<EmployeeAppService> logger)
    {
        _employeeRepository = employeeRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task<PageResult<EmployeeDto>> GetPageAsync(EmployeeQueryDto query)
    {
        ArgumentNullException.ThrowIfNull(query);

        var result = await _employeeRepository.GetPagedListAsync(
            predicate: e =>
                (string.IsNullOrWhiteSpace(query.EmployeeCode) || e.EmployeeCode.Contains(query.EmployeeCode)) &&
                (string.IsNullOrWhiteSpace(query.EmployeeNo) || e.EmployeeNo.Contains(query.EmployeeNo)) &&
                (string.IsNullOrWhiteSpace(query.Name) || e.Name.Contains(query.Name)) &&
                (string.IsNullOrWhiteSpace(query.Department) || e.Department.Contains(query.Department)) &&
                (string.IsNullOrWhiteSpace(query.Position) || e.Position.Contains(query.Position)) &&
                (!query.Status.HasValue || e.Status == query.Status.Value),
            sorting: "EmployeeCode ASC",
            pageIndex: query.PageIndex,
            pageSize: query.PageSize
        );

        var items = result.Items.Select(e => MapToDto(e)).ToList();
        return PageResult<EmployeeDto>.Create(result.TotalCount, result.PageIndex, result.PageSize, items);
    }

    /// <inheritdoc />
    public async Task<List<EmployeeDto>> GetListAsync()
    {
        var employees = await _employeeRepository.GetListAsync(orderBy: null);
        return employees.OrderBy(e => e.EmployeeCode).Select(e => MapToDto(e)).ToList();
    }

    /// <inheritdoc />
    public async Task<EmployeeDto?> GetAsync(string employeeCode)
    {
        // SecondaryRepository.GetByIdAsync 非追踪模式硬编码了 "Id" 属性名，
        // 此实体主键为 EmployeeCode，需使用 isTracking: true 走 FindAsync 路径
        var entity = await _employeeRepository.GetByIdAsync(employeeCode, isTracking: true);
        if (entity == null) return null;
        return MapToDto(entity);
    }

    /// <inheritdoc />
    public async Task<EmployeeDto> CreateAsync(EmployeeSaveDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        // 检查员工编码唯一性
        var exists = await _employeeRepository.FirstOrDefaultAsync(e => e.EmployeeCode == dto.EmployeeCode.Trim());
        if (exists != null)
        {
            throw new InvalidOperationException($"员工编码 '{dto.EmployeeCode}' 已存在");
        }

        var entity = new SserpERPTxnEmployee
        {
            EmployeeCode = dto.EmployeeCode.Trim(),
            EmployeeNo = dto.EmployeeNo.Trim(),
            Name = dto.Name.Trim(),
            EnglishName = dto.EnglishName?.Trim() ?? string.Empty,
            Sex = dto.Sex,
            AGE = dto.AGE,
            BIRTHDATE = dto.BIRTHDATE,
            ID = dto.ID?.Trim() ?? string.Empty,
            Department = dto.Department?.Trim() ?? string.Empty,
            Position = dto.Position?.Trim() ?? string.Empty,
            Status = dto.Status,
            IsAccessAllOutlet = dto.IsAccessAllOutlet,
            WorkingLocationCode = dto.WorkingLocationCode?.Trim() ?? string.Empty,
            AccessGroupCode = dto.AccessGroupCode?.Trim() ?? string.Empty,
            CreateUser = "SYSTEM",
            CreateDate = DateTime.Now
        };

        await _employeeRepository.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("创建员工成功: {EmployeeCode} - {Name}", entity.EmployeeCode, entity.Name);

        return MapToDto(entity);
    }

    /// <inheritdoc />
    public async Task<EmployeeDto> UpdateAsync(string employeeCode, EmployeeSaveDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        var entity = await _employeeRepository.GetByIdAsync(employeeCode, isTracking: true)
            ?? throw new KeyNotFoundException($"员工不存在（编码: {employeeCode}）");

        entity.EmployeeNo = dto.EmployeeNo.Trim();
        entity.Name = dto.Name.Trim();
        entity.EnglishName = dto.EnglishName?.Trim() ?? string.Empty;
        entity.Sex = dto.Sex;
        entity.AGE = dto.AGE;
        entity.BIRTHDATE = dto.BIRTHDATE;
        entity.ID = dto.ID?.Trim() ?? string.Empty;
        entity.Department = dto.Department?.Trim() ?? string.Empty;
        entity.Position = dto.Position?.Trim() ?? string.Empty;
        entity.Status = dto.Status;
        entity.IsAccessAllOutlet = dto.IsAccessAllOutlet;
        entity.WorkingLocationCode = dto.WorkingLocationCode?.Trim() ?? string.Empty;
        entity.AccessGroupCode = dto.AccessGroupCode?.Trim() ?? string.Empty;
        entity.ModifyDate = DateTime.Now;

        _employeeRepository.Update(entity);
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("更新员工成功: {EmployeeCode} - {Name}", entity.EmployeeCode, entity.Name);

        return MapToDto(entity);
    }

    /// <inheritdoc />
    public async Task DeleteAsync(string employeeCode)
    {
        var entity = await _employeeRepository.GetByIdAsync(employeeCode, isTracking: true)
            ?? throw new KeyNotFoundException($"员工不存在（编码: {employeeCode}）");

        _employeeRepository.Delete(entity);
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("删除员工成功: {EmployeeCode} - {Name}", entity.EmployeeCode, entity.Name);
    }

    /// <inheritdoc />
    public async Task DeleteBatchAsync(List<string> employeeCodes)
    {
        ArgumentNullException.ThrowIfNull(employeeCodes);
        if (employeeCodes.Count == 0) return;

        var employees = await _employeeRepository.GetListAsync(predicate: e => employeeCodes.Contains(e.EmployeeCode), orderBy: null);
        if (employees.Count == 0)
        {
            throw new KeyNotFoundException("未找到指定的员工记录");
        }

        _employeeRepository.DeleteRange(employees);
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("批量删除员工成功: {Codes}", string.Join(",", employeeCodes));
    }

    /// <inheritdoc />
    public async Task<List<EmployeeDto>> CreateBatchAsync(List<EmployeeSaveDto> dtos)
    {
        ArgumentNullException.ThrowIfNull(dtos);
        if (dtos.Count == 0)
        {
            throw new ArgumentException("批量添加列表不能为空");
        }

        // 检查重复编码
        var codes = dtos.Select(d => d.EmployeeCode.Trim()).ToList();
        var duplicateCodes = codes.GroupBy(c => c).Where(g => g.Count() > 1).Select(g => g.Key).ToList();
        if (duplicateCodes.Count > 0)
        {
            throw new InvalidOperationException($"批量添加中存在重复编码: {string.Join(",", duplicateCodes)}");
        }

        // 检查已存在的编码
        var existingCodes = new List<string>();
        foreach (var code in codes)
        {
            var exists = await _employeeRepository.AnyAsync(e => e.EmployeeCode == code);
            if (exists) existingCodes.Add(code);
        }
        if (existingCodes.Count > 0)
        {
            throw new InvalidOperationException($"以下员工编码已存在: {string.Join(",", existingCodes)}");
        }

        var entities = dtos.Select(dto => new SserpERPTxnEmployee
        {
            EmployeeCode = dto.EmployeeCode.Trim(),
            EmployeeNo = dto.EmployeeNo.Trim(),
            Name = dto.Name.Trim(),
            EnglishName = dto.EnglishName?.Trim() ?? string.Empty,
            Sex = dto.Sex,
            AGE = dto.AGE,
            BIRTHDATE = dto.BIRTHDATE,
            ID = dto.ID?.Trim() ?? string.Empty,
            Department = dto.Department?.Trim() ?? string.Empty,
            Position = dto.Position?.Trim() ?? string.Empty,
            Status = dto.Status,
            IsAccessAllOutlet = dto.IsAccessAllOutlet,
            WorkingLocationCode = dto.WorkingLocationCode?.Trim() ?? string.Empty,
            AccessGroupCode = dto.AccessGroupCode?.Trim() ?? string.Empty,
            CreateUser = "SYSTEM",
            CreateDate = DateTime.Now
        }).ToList();

        await _employeeRepository.AddRangeAsync(entities);
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("批量创建员工成功，数量: {Count}", entities.Count);

        return entities.Select(e => MapToDto(e)).ToList();
    }

    /// <summary>
    /// 实体转 DTO
    /// </summary>
    private static EmployeeDto MapToDto(SserpERPTxnEmployee entity)
    {
        return new EmployeeDto
        {
            EmployeeCode = entity.EmployeeCode,
            EmployeeNo = entity.EmployeeNo,
            Name = entity.Name,
            EnglishName = entity.EnglishName,
            Sex = entity.Sex,
            AGE = entity.AGE,
            BIRTHDATE = entity.BIRTHDATE,
            ID = entity.ID,
            Department = entity.Department,
            Position = entity.Position,
            Status = entity.Status,
            CreateUser = entity.CreateUser,
            CreateDate = entity.CreateDate,
            ModifyUser = entity.ModifyUser,
            ModifyDate = entity.ModifyDate,
            IsAccessAllOutlet = entity.IsAccessAllOutlet,
            WorkingLocationCode = entity.WorkingLocationCode,
            AccessGroupCode = entity.AccessGroupCode
        };
    }
}