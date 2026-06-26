using Ss.CmbXft.Common.Models.Request;

namespace Ss.CmbXft.Application.Dtos.Sspos;

/// <summary>
/// 优惠券同步查询条件 DTO
/// </summary>
public class CouponSyncQueryDto : PageRequestBase
{
    /// <summary>
    /// 优惠券编码（精确匹配，多个用逗号分隔）
    /// </summary>
    public string? VoucherCode { get; set; }

    /// <summary>
    /// 优惠券名称（模糊搜索）
    /// </summary>
    public string? VoucherName { get; set; }

    /// <summary>
    /// 优惠券类型
    /// </summary>
    public string? VoucherType { get; set; }

    /// <summary>
    /// 是否只同步激活状态的优惠券（默认只同步激活的优惠券）
    /// </summary>
    public bool? Active { get; set; } = true;

    /// <summary>
    /// 门店编码筛选（仅同步指定门店的优惠券）
    /// </summary>
    public string? LocationCode { get; set; }
}
