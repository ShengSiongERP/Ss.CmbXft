using Ss.CmbXft.Application.Dtos.Sserp;
using Ss.CmbXft.Common.Models;

namespace Ss.CmbXft.Application.Services;

/// <summary>
/// Abp用户角色关联应用服务接口
/// </summary>
public interface IAbpUserRoleAppService
{
    /// <summary>
    /// 分页查询用户角色关联列表
    /// </summary>
    Task<PageResult<AbpUserRoleDto>> GetPageAsync(AbpUserRoleQueryDto query);

    /// <summary>
    /// 获取所有用户角色关联列表
    /// </summary>
    Task<List<AbpUserRoleDto>> GetListAsync();

    /// <summary>
    /// 根据用户ID和角色ID获取详情
    /// </summary>
    Task<AbpUserRoleDto?> GetAsync(Guid userId, Guid roleId);

    /// <summary>
    /// 根据用户ID获取该用户的角色ID列表
    /// </summary>
    Task<List<Guid>> GetRoleIdsByUserIdAsync(Guid userId);

    /// <summary>
    /// 根据角色ID获取拥有该角色的用户ID列表
    /// </summary>
    Task<List<Guid>> GetUserIdsByRoleIdAsync(Guid roleId);

    /// <summary>
    /// 创建用户角色关联
    /// </summary>
    Task<AbpUserRoleDto> CreateAsync(AbpUserRoleSaveDto dto);

    /// <summary>
    /// 批量为用户分配角色
    /// </summary>
    Task BatchAssignAsync(AbpUserRoleBatchAssignDto dto);

    /// <summary>
    /// 删除用户角色关联
    /// </summary>
    Task DeleteAsync(Guid userId, Guid roleId);

    /// <summary>
    /// 批量删除用户角色关联
    /// </summary>
    Task DeleteBatchAsync(List<(Guid UserId, Guid RoleId)> ids);

    /// <summary>
    /// 删除用户的所有角色关联
    /// </summary>
    Task DeleteByUserIdAsync(Guid userId);

    /// <summary>
    /// 删除角色的所有用户关联
    /// </summary>
    Task DeleteByRoleIdAsync(Guid roleId);
}