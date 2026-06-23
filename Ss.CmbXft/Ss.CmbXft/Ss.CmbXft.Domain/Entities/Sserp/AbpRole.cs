using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ss.CmbXft.Domain.Entities.Sserp;

/// <summary>
/// Abp角色表实体（连接外部SQL Server数据库，非CodeFirst）
/// </summary>
public class AbpRole
{
    /// <summary>
    /// 角色ID（主键）
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
    /// 角色名称
    /// </summary>
    [Column("Name")]
    [StringLength(256)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 规范化角色名称
    /// </summary>
    [Column("NormalizedName")]
    [StringLength(256)]
    public string NormalizedName { get; set; } = string.Empty;

    /// <summary>
    /// 是否为默认角色
    /// </summary>
    [Column("IsDefault")]
    public bool IsDefault { get; set; }

    /// <summary>
    /// 是否为静态角色
    /// </summary>
    [Column("IsStatic")]
    public bool IsStatic { get; set; }

    /// <summary>
    /// 是否为公共角色
    /// </summary>
    [Column("IsPublic")]
    public bool IsPublic { get; set; }

    /// <summary>
    /// 扩展属性（JSON格式）
    /// </summary>
    [Column("ExtraProperties")]
    public string? ExtraProperties { get; set; }

    /// <summary>
    /// 并发标记
    /// </summary>
    [Column("ConcurrencyStamp")]
    [StringLength(40)]
    public string? ConcurrencyStamp { get; set; }
}