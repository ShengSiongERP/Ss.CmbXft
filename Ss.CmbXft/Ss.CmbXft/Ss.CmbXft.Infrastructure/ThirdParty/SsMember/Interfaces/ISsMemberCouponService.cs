using Ss.CmbXft.Domain.ThirdParty.SsMember;
using Ss.CmbXft.Infrastructure.ThirdParty.SsMember.Model.CouponSync;

namespace Ss.CmbXft.Infrastructure.ThirdParty.SsMember.Interfaces;

/// <summary>
/// 昇菘会员系统优惠券同步服务接口
/// </summary>
public interface ISsMemberCouponService
{
    /// <summary>
    /// 获取优惠券
    /// </summary>
    Task<SsMemberResponse> GetCouponsAsync(CancellationToken ct = default);
    /// <summary>
    /// 同步优惠券信息到会员系统
    /// </summary>
    /// <param name="coupons">优惠券列表</param>
    /// <param name="ct">取消令牌</param>
    /// <returns>同步结果</returns>
    Task<SsMemberResponse> SyncCouponsAsync(List<CouponSyncInfo> coupons, CancellationToken ct = default);
}
