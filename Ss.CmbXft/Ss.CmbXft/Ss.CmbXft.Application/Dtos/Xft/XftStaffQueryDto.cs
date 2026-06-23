using Ss.CmbXft.Common.Models;

namespace Ss.CmbXft.Application.Dtos.Xft;

/// <summary>
/// 员工查询请求DTO
/// </summary>
public class XftStaffQueryDto : PagedRequestBase
{
    /// <summary>
    /// 员工序号
    /// </summary>
    public string? StaffSeq { get; set; }

    /// <summary>
    /// 员工姓名
    /// </summary>
    public string? StfName { get; set; }

    /// <summary>
    /// 手机号
    /// </summary>
    public string? MobileNumber { get; set; }

    /// <summary>
    /// 员工类型
    /// </summary>
    public string? StfType { get; set; }

    /// <summary>
    /// 员工状态
    /// </summary>
    public string? StfStatus { get; set; }

    /// <summary>
    /// 企业号
    /// </summary>
    public string? EnterpriseId { get; set; }
}