using Ss.CmbXft.Application.Dtos.Sspos;
using Ss.CmbXft.Common.Models;

namespace Ss.CmbXft.Infrastructure.ThirdParty.SsMember.Interfaces;

/// <summary>
/// 优惠券同步应用服务接口
/// 负责从POS数据库读取优惠券数据，拼凑JSON后同步到昇菘会员系统
/// </summary>
public interface ICouponSyncAppService
{
    /// <summary>
    /// 同步优惠券到会员系统（根据查询条件筛选优惠券后同步）
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="ct">取消令牌</param>
    /// <returns>同步结果</returns>
    Task<ApiResult> SyncCouponsAsync(CouponSyncQueryDto query, CancellationToken ct = default);
}
