using Ss.CmbXft.Application.Dtos.Sserp;
using Ss.CmbXft.Common.Models;

namespace Ss.CmbXft.Application.Services;

/// <summary>
/// Abp角色应用服务接口
/// </summary>
public interface IAbpRoleAppService
{
    /// <summary>
    /// 分页查询角色列表
    /// </summary>
    Task<PageResult<AbpRoleDto>> GetPageAsync(AbpRoleQueryDto query);

    /// <summary>
    /// 获取所有角色（下拉框等场景使用）
    /// </summary>
    Task<List<AbpRoleDto>> GetListAsync();

    /// <summary>
    /// 根据ID获取角色详情
    /// </summary>
    Task<AbpRoleDto?> GetAsync(Guid id);
}