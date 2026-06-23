using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ss.CmbXft.Domain.Entities.Sserp;

/// <summary>
/// 员工信息表实体（连接外部SQL Server数据库，非CodeFirst）
/// </summary>
public class SserpERPTxnEmployee
{
    /// <summary>
    /// 员工编码（主键）
    /// </summary>
    [Key]
    [Column("EmployeeCode")]
    [StringLength(50)]
    public string EmployeeCode { get; set; } = string.Empty;

    /// <summary>
    /// 员工工号
    /// </summary>
    [Column("EmployeeNo")]
    [StringLength(50)]
    public string EmployeeNo { get; set; } = string.Empty;

    /// <summary>
    /// 姓名
    /// </summary>
    [Column("Name")]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 英文名
    /// </summary>
    [Column("EnglishName")]
    [StringLength(100)]
    public string EnglishName { get; set; } = string.Empty;

    /// <summary>
    /// 性别 1=男 2=女
    /// </summary>
    [Column("Sex")]
    public short Sex { get; set; }

    /// <summary>
    /// 年龄（可空）
    /// </summary>
    [Column("AGE")]
    public int? AGE { get; set; }

    /// <summary>
    /// 出生日期（可空）
    /// </summary>
    [Column("BIRTHDATE")]
    public DateTime? BIRTHDATE { get; set; }

    /// <summary>
    /// 证件号码
    /// </summary>
    [Column("ID")]
    [StringLength(50)]
    public string ID { get; set; } = string.Empty;

    /// <summary>
    /// 部门（可空）
    /// </summary>
    [Column("Department")]
    [StringLength(100)]
    public string? Department { get; set; }

    /// <summary>
    /// 职位（可空）
    /// </summary>
    [Column("Position")]
    [StringLength(100)]
    public string? Position { get; set; }

    /// <summary>
    /// 状态（1正常0无效）
    /// </summary>
    [Column("Status")]
    public short? Status { get; set; }

    /// <summary>
    /// 创建人（可空）
    /// </summary>
    [Column("CreateUser")]
    [StringLength(50)]
    public string? CreateUser { get; set; } = "SYSTEM";

    /// <summary>
    /// 创建时间（可空）
    /// </summary>
    [Column("CreateDate")]
    public DateTime? CreateDate { get; set; }

    /// <summary>
    /// 修改人（可空）
    /// </summary>
    [Column("ModifyUser")]
    [StringLength(50)]
    public string? ModifyUser { get; set; } = string.Empty;

    /// <summary>
    /// 修改时间（可空）
    /// </summary>
    [Column("ModifyDate")]
    public DateTime? ModifyDate { get; set; }

    /// <summary>
    /// 是否可访问所有门店（可空）
    /// </summary>
    [Column("IsAccessAllOutlet")]
    public bool? IsAccessAllOutlet { get; set; }

    /// <summary>
    /// 工作地点编码（可空）
    /// </summary>
    [Column("WorkingLocationCode")]
    [StringLength(50)]
    public string? WorkingLocationCode { get; set; } = string.Empty;

    /// <summary>
    /// 权限组编码（可空）
    /// </summary>
    [Column("AccessGroupCode")]
    [StringLength(50)]
    public string? AccessGroupCode { get; set; } = string.Empty;
}