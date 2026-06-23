using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ss.CmbXft.Domain.Entities.Sserp;

/// <summary>
/// Abp用户表实体（连接外部SQL Server数据库，非CodeFirst）
/// </summary>
public class AbpUser
{
    /// <summary>
    /// 用户ID（主键）
    /// </summary>
    [Key]
    [Column("Id")]
    public Guid Id { get; set; }

    /// <summary>
    /// 租户ID（可空）
    /// </summary>
    [Column("TenantId")]
    public Guid? TenantId { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    [Column("UserName")]
    [StringLength(256)]
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// 规范化用户名
    /// </summary>
    [Column("NormalizedUserName")]
    [StringLength(256)]
    public string NormalizedUserName { get; set; } = string.Empty;

    /// <summary>
    /// 名字
    /// </summary>
    [Column("Name")]
    [StringLength(64)]
    public string? Name { get; set; }

    /// <summary>
    /// 姓氏
    /// </summary>
    [Column("Surname")]
    [StringLength(64)]
    public string? Surname { get; set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    [Column("Email")]
    [StringLength(256)]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// 规范化邮箱
    /// </summary>
    [Column("NormalizedEmail")]
    [StringLength(256)]
    public string NormalizedEmail { get; set; } = string.Empty;

    /// <summary>
    /// 邮箱是否已确认
    /// </summary>
    [Column("EmailConfirmed")]
    public bool EmailConfirmed { get; set; }

    /// <summary>
    /// 密码哈希
    /// </summary>
    [Column("PasswordHash")]
    [StringLength(256)]
    public string? PasswordHash { get; set; }

    /// <summary>
    /// 安全戳
    /// </summary>
    [Column("SecurityStamp")]
    [StringLength(256)]
    public string SecurityStamp { get; set; } = string.Empty;

    /// <summary>
    /// 是否为外部用户
    /// </summary>
    [Column("IsExternal")]
    public bool IsExternal { get; set; }

    /// <summary>
    /// 电话号码
    /// </summary>
    [Column("PhoneNumber")]
    [StringLength(16)]
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// 电话号码是否已确认
    /// </summary>
    [Column("PhoneNumberConfirmed")]
    public bool PhoneNumberConfirmed { get; set; }

    /// <summary>
    /// 是否启用双因素认证
    /// </summary>
    [Column("TwoFactorEnabled")]
    public bool TwoFactorEnabled { get; set; }

    /// <summary>
    /// 锁定结束时间
    /// </summary>
    [Column("LockoutEnd")]
    public DateTimeOffset? LockoutEnd { get; set; }

    /// <summary>
    /// 是否启用锁定
    /// </summary>
    [Column("LockoutEnabled")]
    public bool LockoutEnabled { get; set; }

    /// <summary>
    /// 访问失败次数
    /// </summary>
    [Column("AccessFailedCount")]
    public int AccessFailedCount { get; set; }

    /// <summary>
    /// 扩展属性（JSON格式）
    /// </summary>
    [Column("ExtraProperties")]
    public string? ExtraProperties { get; set; }

    /// <summary>
    /// 并发戳
    /// </summary>
    [Column("ConcurrencyStamp")]
    [StringLength(40)]
    public string? ConcurrencyStamp { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    [Column("CreationTime")]
    public DateTime CreationTime { get; set; }

    /// <summary>
    /// 创建人ID
    /// </summary>
    [Column("CreatorId")]
    public Guid? CreatorId { get; set; }

    /// <summary>
    /// 最后修改时间
    /// </summary>
    [Column("LastModificationTime")]
    public DateTime? LastModificationTime { get; set; }

    /// <summary>
    /// 最后修改人ID
    /// </summary>
    [Column("LastModifierId")]
    public Guid? LastModifierId { get; set; }

    /// <summary>
    /// 是否已删除
    /// </summary>
    [Column("IsDeleted")]
    public bool IsDeleted { get; set; }

    /// <summary>
    /// 删除人ID
    /// </summary>
    [Column("DeleterId")]
    public Guid? DeleterId { get; set; }

    /// <summary>
    /// 删除时间
    /// </summary>
    [Column("DeletionTime")]
    public DateTime? DeletionTime { get; set; }

    /// <summary>
    /// 分店ID
    /// </summary>
    [Column("BranchId")]
    public int? BranchId { get; set; }

    /// <summary>
    /// 是否首次登录
    /// </summary>
    [Column("IsFirstTimeLogin")]
    public bool IsFirstTimeLogin { get; set; }

    /// <summary>
    /// 是否激活
    /// </summary>
    [Column("IsActive")]
    public bool IsActive { get; set; }
}