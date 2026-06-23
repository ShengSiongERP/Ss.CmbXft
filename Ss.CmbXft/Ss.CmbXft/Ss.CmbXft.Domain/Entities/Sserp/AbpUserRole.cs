using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ss.CmbXft.Domain.Entities.Sserp;

/// <summary>
/// Abp用户角色关联表实体（连接外部SQL Server数据库，非CodeFirst）
/// </summary>
public class AbpUserRole
{
    /// <summary>
    /// 用户ID（主键）
    /// </summary>
    [Key]
    [Column("UserId")]
    public Guid UserId { get; set; }

    /// <summary>
    /// 角色ID（主键）
    /// </summary>
    [Key]
    [Column("RoleId")]
    public Guid RoleId { get; set; }

    /// <summary>
    /// 租户ID（可空）
    /// </summary>
    [Column("TenantId")]
    public Guid? TenantId { get; set; }
}