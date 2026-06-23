using System;
using Ss.CmbXft.Domain.Base;

namespace Ss.CmbXft.Domain.Entities;

/// <summary>
/// 示例数据实体：员工本地扩展信息
/// </summary>
public class XftStaff : AllEntityBase<long>
{
    /// <summary>
    /// 企业号
    /// </summary>
    public string? EnterpriseId { get; set; }
    /// <summary>
    /// 员工序号
    /// </summary>
    public string? StaffSeq { get; set; }
    /// <summary>
    /// 员工类型
    /// </summary>
    public string? StfType { get; set; }
    /// <summary>
    /// 员工状态
    /// </summary>
    public string? StfStatus { get; set; }
    /// <summary>
    /// 员工姓名
    /// </summary>
    public string? StfName { get; set; }
    /// <summary>
    /// 手机号
    /// </summary>
    public string? MobileNumber { get; set; }
    
    /// <summary>
    /// 全量员工信息JSON
    /// </summary>
    public string StaffJson { get; set; } = string.Empty;
}
