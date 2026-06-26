using Ss.CmbXft.Common.Models.Request;
using System.ComponentModel.DataAnnotations;

namespace Ss.CmbXft.Application.Dtos.Sserp;

/// <summary>
/// Abp用户角色关联响应 DTO
/// </summary>
public class AbpUserRoleDto
{
    /// <summary>
    /// 用户ID
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// 角色ID
    /// </summary>
    public Guid RoleId { get; set; }

    /// <summary>
    /// 租户ID
    /// </summary>
    public Guid? TenantId { get; set; }
}

/// <summary>
/// Abp用户角色关联保存 DTO
/// </summary>
public class AbpUserRoleSaveDto
{
    /// <summary>
    /// 用户ID
    /// </summary>
    [Required(ErrorMessage = "用户ID不能为空")]
    public Guid UserId { get; set; }

    /// <summary>
    /// 角色ID
    /// </summary>
    [Required(ErrorMessage = "角色ID不能为空")]
    public Guid RoleId { get; set; }

    /// <summary>
    /// 租户ID
    /// </summary>
    public Guid? TenantId { get; set; }
}

/// <summary>
/// Abp用户角色关联查询 DTO
/// </summary>
public class AbpUserRoleQueryDto : PageRequestBase
{
    /// <summary>
    /// 用户ID筛选
    /// </summary>
    public Guid? UserId { get; set; }

    /// <summary>
    /// 角色ID筛选
    /// </summary>
    public Guid? RoleId { get; set; }

    /// <summary>
    /// 租户ID筛选
    /// </summary>
    public Guid? TenantId { get; set; }
}

/// <summary>
/// 批量分配用户角色 DTO
/// </summary>
public class AbpUserRoleBatchAssignDto
{
    /// <summary>
    /// 用户ID
    /// </summary>
    [Required(ErrorMessage = "用户ID不能为空")]
    public Guid UserId { get; set; }

    /// <summary>
    /// 角色ID列表
    /// </summary>
    [Required(ErrorMessage = "角色ID列表不能为空")]
    public List<Guid> RoleIds { get; set; } = new();
}