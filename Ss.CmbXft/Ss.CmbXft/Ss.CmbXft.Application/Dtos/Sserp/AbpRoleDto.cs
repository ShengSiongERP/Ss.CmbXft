using Ss.CmbXft.Common.Models;
using System.ComponentModel.DataAnnotations;

namespace Ss.CmbXft.Application.Dtos.Sserp;

/// <summary>
/// Abp角色响应 DTO
/// </summary>
public class AbpRoleDto
{
    public Guid Id { get; set; }
    public Guid? TenantId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string NormalizedName { get; set; } = string.Empty;
    public bool IsDefault { get; set; }
    public bool IsStatic { get; set; }
    public bool IsPublic { get; set; }
    public string? ExtraProperties { get; set; }
    public string? ConcurrencyStamp { get; set; }
}

/// <summary>
/// Abp角色查询DTO
/// </summary>
public class AbpRoleQueryDto : PagedRequestBase
{
    /// <summary>
    /// 角色名称
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// 规范化角色名称
    /// </summary>
    public string? NormalizedName { get; set; }

    /// <summary>
    /// 是否为默认角色
    /// </summary>
    public bool? IsDefault { get; set; }

    /// <summary>
    /// 是否为静态角色
    /// </summary>
    public bool? IsStatic { get; set; }

    /// <summary>
    /// 是否为公共角色
    /// </summary>
    public bool? IsPublic { get; set; }
}