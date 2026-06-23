using Ss.CmbXft.Common.Models;
using System.ComponentModel.DataAnnotations;

namespace Ss.CmbXft.Application.Dtos.Sserp;

/// <summary>
/// 员工信息响应 DTO
/// </summary>
public class EmployeeDto
{
    /// <summary>
    /// 员工编码（主键）
    /// </summary>
    public string EmployeeCode { get; set; } = string.Empty;

    /// <summary>
    /// 员工工号
    /// </summary>
    public string EmployeeNo { get; set; } = string.Empty;

    /// <summary>
    /// 姓名
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 英文名
    /// </summary>
    public string EnglishName { get; set; } = string.Empty;

    /// <summary>
    /// 性别 0=女 1=男
    /// </summary>
    public short Sex { get; set; }

    /// <summary>
    /// 年龄
    /// </summary>
    public int? AGE { get; set; }

    /// <summary>
    /// 出生日期
    /// </summary>
    public DateTime? BIRTHDATE { get; set; }

    /// <summary>
    /// 证件号码
    /// </summary>
    public string ID { get; set; } = string.Empty;

    /// <summary>
    /// 部门
    /// </summary>
    public string Department { get; set; } = string.Empty;

    /// <summary>
    /// 职位
    /// </summary>
    public string Position { get; set; } = string.Empty;

    /// <summary>
    /// 状态
    /// </summary>
    public short? Status { get; set; }

    /// <summary>
    /// 创建人
    /// </summary>
    public string CreateUser { get; set; } = string.Empty;

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime? CreateDate { get; set; }

    /// <summary>
    /// 修改人
    /// </summary>
    public string ModifyUser { get; set; } = string.Empty;

    /// <summary>
    /// 修改时间
    /// </summary>
    public DateTime? ModifyDate { get; set; }

    /// <summary>
    /// 是否可访问所有门店
    /// </summary>
    public bool? IsAccessAllOutlet { get; set; }

    /// <summary>
    /// 工作地点编码
    /// </summary>
    public string WorkingLocationCode { get; set; } = string.Empty;

    /// <summary>
    /// 权限组编码
    /// </summary>
    public string AccessGroupCode { get; set; } = string.Empty;
}

/// <summary>
/// 员工信息保存 DTO（创建/更新共用）
/// </summary>
public class EmployeeSaveDto
{
    /// <summary>
    /// 员工编码（主键，创建时必填，更新时不可改）
    /// </summary>
    [StringLength(50)]
    public string EmployeeCode { get; set; } = string.Empty;

    /// <summary>
    /// 员工工号
    /// </summary>
    [StringLength(50)]
    public string EmployeeNo { get; set; } = string.Empty;

    /// <summary>
    /// 姓名
    /// </summary>
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 英文名
    /// </summary>
    [StringLength(100)]
    public string EnglishName { get; set; } = string.Empty;

    /// <summary>
    /// 性别 0=女 1=男
    /// </summary>
    public short Sex { get; set; }

    /// <summary>
    /// 年龄
    /// </summary>
    public int? AGE { get; set; }

    /// <summary>
    /// 出生日期
    /// </summary>
    public DateTime? BIRTHDATE { get; set; }

    /// <summary>
    /// 证件号码
    /// </summary>
    [StringLength(50)]
    public string ID { get; set; } = string.Empty;

    /// <summary>
    /// 部门
    /// </summary>
    [StringLength(100)]
    public string Department { get; set; } = string.Empty;

    /// <summary>
    /// 职位
    /// </summary>
    [StringLength(100)]
    public string Position { get; set; } = string.Empty;

    /// <summary>
    /// 状态
    /// </summary>
    public short? Status { get; set; }

    /// <summary>
    /// 是否可访问所有门店
    /// </summary>
    public bool? IsAccessAllOutlet { get; set; }

    /// <summary>
    /// 工作地点编码
    /// </summary>
    [StringLength(50)]
    public string WorkingLocationCode { get; set; } = string.Empty;

    /// <summary>
    /// 权限组编码
    /// </summary>
    [StringLength(50)]
    public string AccessGroupCode { get; set; } = string.Empty;
}

/// <summary>
/// 员工信息查询 DTO
/// </summary>
public class EmployeeQueryDto : PagedRequestBase
{
    /// <summary>
    /// 员工编码
    /// </summary>
    public string? EmployeeCode { get; set; }

    /// <summary>
    /// 员工工号
    /// </summary>
    public string? EmployeeNo { get; set; }

    /// <summary>
    /// 姓名（模糊搜索）
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// 部门
    /// </summary>
    public string? Department { get; set; }

    /// <summary>
    /// 职位
    /// </summary>
    public string? Position { get; set; }

    /// <summary>
    /// 状态筛选
    /// </summary>
    public short? Status { get; set; }
}