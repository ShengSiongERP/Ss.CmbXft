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
/// Abp用户角色关联应用服务实现
/// </summary>
public class AbpUserRoleAppService : IAbpUserRoleAppService
{
    private readonly ISserpRepository<AbpUserRole, (Guid UserId, Guid RoleId)> _userRoleRepository;
    private readonly ISserpUnitOfWork _unitOfWork;
    private readonly ILogger<AbpUserRoleAppService> _logger;

    public AbpUserRoleAppService(
        ISserpRepository<AbpUserRole, (Guid UserId, Guid RoleId)> userRoleRepository,
        ISserpUnitOfWork unitOfWork,
        ILogger<AbpUserRoleAppService> logger)
    {
        _userRoleRepository = userRoleRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task<PageResult<AbpUserRoleDto>> GetPageAsync(AbpUserRoleQueryDto query)
    {
        ArgumentNullException.ThrowIfNull(query);

        var result = await _userRoleRepository.GetPagedListAsync(
            predicate: ur =>
                (!query.UserId.HasValue || ur.UserId == query.UserId.Value) &&
                (!query.RoleId.HasValue || ur.RoleId == query.RoleId.Value) &&
                (!query.TenantId.HasValue || ur.TenantId == query.TenantId.Value),
            sorting: "UserId ASC, RoleId ASC",
            pageIndex: query.PageIndex,
            pageSize: query.PageSize
        );

        var items = result.Items.Select(ur => MapToDto(ur)).ToList();
        return PageResult<AbpUserRoleDto>.Create(result.TotalCount, result.PageIndex, result.PageSize, items);
    }

    /// <inheritdoc />
    public async Task<List<AbpUserRoleDto>> GetListAsync()
    {
        var userRoles = await _userRoleRepository.GetListAsync(orderBy: null);
        return userRoles.Select(ur => MapToDto(ur)).ToList();
    }

    /// <inheritdoc />
    public async Task<AbpUserRoleDto?> GetAsync(Guid userId, Guid roleId)
    {
        var entity = await _userRoleRepository.FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleId);
        if (entity == null) return null;
        return MapToDto(entity);
    }

    /// <inheritdoc />
    public async Task<List<Guid>> GetRoleIdsByUserIdAsync(Guid userId)
    {
        var userRoles = await _userRoleRepository.GetListAsync(predicate: ur => ur.UserId == userId, orderBy: null);
        return userRoles.Select(ur => ur.RoleId).ToList();
    }

    /// <inheritdoc />
    public async Task<List<Guid>> GetUserIdsByRoleIdAsync(Guid roleId)
    {
        var userRoles = await _userRoleRepository.GetListAsync(predicate: ur => ur.RoleId == roleId, orderBy: null);
        return userRoles.Select(ur => ur.UserId).ToList();
    }

    /// <inheritdoc />
    public async Task<AbpUserRoleDto> CreateAsync(AbpUserRoleSaveDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        // 检查是否已存在
        var exists = await _userRoleRepository.FirstOrDefaultAsync(ur => ur.UserId == dto.UserId && ur.RoleId == dto.RoleId);
        if (exists != null)
        {
            throw new InvalidOperationException($"用户ID '{dto.UserId}' 和角色ID '{dto.RoleId}' 的关联已存在");
        }

        var entity = new AbpUserRole
        {
            UserId = dto.UserId,
            RoleId = dto.RoleId,
            TenantId = dto.TenantId
        };

        await _userRoleRepository.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("创建用户角色关联成功: UserId={UserId}, RoleId={RoleId}", entity.UserId, entity.RoleId);

        return MapToDto(entity);
    }

    /// <inheritdoc />
    public async Task BatchAssignAsync(AbpUserRoleBatchAssignDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        // 先删除该用户的所有角色关联
        var existingRoles = await _userRoleRepository.GetListAsync(predicate: ur => ur.UserId == dto.UserId, orderBy: null);
        if (existingRoles.Any())
        {
            _userRoleRepository.DeleteRange(existingRoles);
        }

        // 添加新的角色关联
        var newRoles = dto.RoleIds.Select(roleId => new AbpUserRole
        {
            UserId = dto.UserId,
            RoleId = roleId,
            TenantId = null
        }).ToList();

        if (newRoles.Any())
        {
            await _userRoleRepository.AddRangeAsync(newRoles);
        }

        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("批量分配用户角色成功: UserId={UserId}, RoleCount={RoleCount}", dto.UserId, dto.RoleIds.Count);
    }

    /// <inheritdoc />
    public async Task DeleteAsync(Guid userId, Guid roleId)
    {
        var entity = await _userRoleRepository.FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleId);
        if (entity == null)
        {
            throw new InvalidOperationException($"用户ID '{userId}' 和角色ID '{roleId}' 的关联不存在");
        }

        _userRoleRepository.Delete(entity);
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("删除用户角色关联成功: UserId={UserId}, RoleId={RoleId}", userId, roleId);
    }

    /// <inheritdoc />
    public async Task DeleteBatchAsync(List<(Guid UserId, Guid RoleId)> ids)
    {
        ArgumentNullException.ThrowIfNull(ids);

        var entities = new List<AbpUserRole>();
        foreach (var (userId, roleId) in ids)
        {
            var entity = await _userRoleRepository.FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleId);
            if (entity != null)
            {
                entities.Add(entity);
            }
        }

        if (entities.Any())
        {
            _userRoleRepository.DeleteRange(entities);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("批量删除用户角色关联成功: Count={Count}", entities.Count);
        }
    }

    /// <inheritdoc />
    public async Task DeleteByUserIdAsync(Guid userId)
    {
        var entities = await _userRoleRepository.GetListAsync(predicate: ur => ur.UserId == userId, orderBy: null);
        if (entities.Any())
        {
            _userRoleRepository.DeleteRange(entities);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("删除用户的所有角色关联成功: UserId={UserId}, Count={Count}", userId, entities.Count);
        }
    }

    /// <inheritdoc />
    public async Task DeleteByRoleIdAsync(Guid roleId)
    {
        var entities = await _userRoleRepository.GetListAsync(predicate: ur => ur.RoleId == roleId, orderBy: null);
        if (entities.Any())
        {
            _userRoleRepository.DeleteRange(entities);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("删除角色的所有用户关联成功: RoleId={RoleId}, Count={Count}", roleId, entities.Count);
        }
    }

    /// <summary>
    /// 映射实体到 DTO
    /// </summary>
    private static AbpUserRoleDto MapToDto(AbpUserRole entity)
    {
        return new AbpUserRoleDto
        {
            UserId = entity.UserId,
            RoleId = entity.RoleId,
            TenantId = entity.TenantId
        };
    }
}