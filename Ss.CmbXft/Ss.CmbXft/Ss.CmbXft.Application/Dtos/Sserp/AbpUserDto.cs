using Ss.CmbXft.Common.Models;
using System.ComponentModel.DataAnnotations;

namespace Ss.CmbXft.Application.Dtos.Sserp;

/// <summary>
/// Abp用户响应 DTO
/// </summary>
public class AbpUserDto
{
    /// <summary>
    /// 用户ID（主键）
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// 租户ID
    /// </summary>
    public Guid? TenantId { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// 规范化用户名
    /// </summary>
    public string NormalizedUserName { get; set; } = string.Empty;

    /// <summary>
    /// 名字
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// 姓氏
    /// </summary>
    public string? Surname { get; set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// 规范化邮箱
    /// </summary>
    public string NormalizedEmail { get; set; } = string.Empty;

    /// <summary>
    /// 邮箱是否已确认
    /// </summary>
    public bool EmailConfirmed { get; set; }

    /// <summary>
    /// 密码哈希
    /// </summary>
    public string? PasswordHash { get; set; }

    /// <summary>
    /// 安全戳
    /// </summary>
    public string SecurityStamp { get; set; } = string.Empty;

    /// <summary>
    /// 是否为外部用户
    /// </summary>
    public bool IsExternal { get; set; }

    /// <summary>
    /// 电话号码
    /// </summary>
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// 电话号码是否已确认
    /// </summary>
    public bool PhoneNumberConfirmed { get; set; }

    /// <summary>
    /// 是否启用双因素认证
    /// </summary>
    public bool TwoFactorEnabled { get; set; }

    /// <summary>
    /// 锁定结束时间
    /// </summary>
    public DateTimeOffset? LockoutEnd { get; set; }

    /// <summary>
    /// 是否启用锁定
    /// </summary>
    public bool LockoutEnabled { get; set; }

    /// <summary>
    /// 访问失败次数
    /// </summary>
    public int AccessFailedCount { get; set; }

    /// <summary>
    /// 扩展属性（JSON格式）
    /// </summary>
    public string? ExtraProperties { get; set; }

    /// <summary>
    /// 并发戳
    /// </summary>
    public string? ConcurrencyStamp { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreationTime { get; set; }

    /// <summary>
    /// 创建人ID
    /// </summary>
    public Guid? CreatorId { get; set; }

    /// <summary>
    /// 最后修改时间
    /// </summary>
    public DateTime? LastModificationTime { get; set; }

    /// <summary>
    /// 最后修改人ID
    /// </summary>
    public Guid? LastModifierId { get; set; }

    /// <summary>
    /// 是否已删除
    /// </summary>
    public bool IsDeleted { get; set; }

    /// <summary>
    /// 删除人ID
    /// </summary>
    public Guid? DeleterId { get; set; }

    /// <summary>
    /// 删除时间
    /// </summary>
    public DateTime? DeletionTime { get; set; }

    /// <summary>
    /// 分店ID
    /// </summary>
    public int? BranchId { get; set; }

    /// <summary>
    /// 是否首次登录
    /// </summary>
    public bool IsFirstTimeLogin { get; set; }

    /// <summary>
    /// 是否激活
    /// </summary>
    public bool IsActive { get; set; }
}

/// <summary>
/// Abp用户保存 DTO（创建/更新共用）
/// </summary>
public class AbpUserSaveDto
{
    /// <summary>
    /// 用户ID（主键，创建时必填，更新时不可改）
    /// </summary>
    [Required]
    public Guid Id { get; set; }

    /// <summary>
    /// 租户ID
    /// </summary>
    public Guid? TenantId { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    [Required]
    [StringLength(256)]
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// 规范化用户名
    /// </summary>
    [Required]
    [StringLength(256)]
    public string NormalizedUserName { get; set; } = string.Empty;

    /// <summary>
    /// 名字
    /// </summary>
    [StringLength(64)]
    public string? Name { get; set; }

    /// <summary>
    /// 姓氏
    /// </summary>
    [StringLength(64)]
    public string? Surname { get; set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    [Required]
    [StringLength(256)]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// 规范化邮箱
    /// </summary>
    [Required]
    [StringLength(256)]
    public string NormalizedEmail { get; set; } = string.Empty;

    /// <summary>
    /// 邮箱是否已确认
    /// </summary>
    public bool EmailConfirmed { get; set; }

    /// <summary>
    /// 密码哈希
    /// </summary>
    [StringLength(256)]
    public string? PasswordHash { get; set; }

    /// <summary>
    /// 安全戳
    /// </summary>
    [Required]
    [StringLength(256)]
    public string SecurityStamp { get; set; } = string.Empty;

    /// <summary>
    /// 是否为外部用户
    /// </summary>
    public bool IsExternal { get; set; }

    /// <summary>
    /// 电话号码
    /// </summary>
    [StringLength(16)]
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// 电话号码是否已确认
    /// </summary>
    public bool PhoneNumberConfirmed { get; set; }

    /// <summary>
    /// 是否启用双因素认证
    /// </summary>
    public bool TwoFactorEnabled { get; set; }

    /// <summary>
    /// 锁定结束时间
    /// </summary>
    public DateTimeOffset? LockoutEnd { get; set; }

    /// <summary>
    /// 是否启用锁定
    /// </summary>
    public bool LockoutEnabled { get; set; }

    /// <summary>
    /// 访问失败次数
    /// </summary>
    public int AccessFailedCount { get; set; }

    /// <summary>
    /// 扩展属性（JSON格式）
    /// </summary>
    public string? ExtraProperties { get; set; }

    /// <summary>
    /// 并发戳
    /// </summary>
    [StringLength(40)]
    public string? ConcurrencyStamp { get; set; }

    /// <summary>
    /// 分店ID
    /// </summary>
    public int? BranchId { get; set; }

    /// <summary>
    /// 是否首次登录
    /// </summary>
    public bool IsFirstTimeLogin { get; set; }

    /// <summary>
    /// 是否激活
    /// </summary>
    public bool IsActive { get; set; }
}

/// <summary>
/// Abp用户查询 DTO
/// </summary>
public class AbpUserQueryDto : PagedRequestBase
{
    /// <summary>
    /// 用户名（模糊搜索）
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    /// 邮箱（模糊搜索）
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// 名字（模糊搜索）
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// 姓氏（模糊搜索）
    /// </summary>
    public string? Surname { get; set; }

    /// <summary>
    /// 电话号码（模糊搜索）
    /// </summary>
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// 是否激活筛选
    /// </summary>
    public bool? IsActive { get; set; }

    /// <summary>
    /// 分店ID筛选
    /// </summary>
    public int? BranchId { get; set; }

    /// <summary>
    /// 是否已删除筛选
    /// </summary>
    public bool? IsDeleted { get; set; }
}