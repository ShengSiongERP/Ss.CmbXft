using Ss.CmbXft.Domain.ThirdParty.SsMember;
using Ss.CmbXft.Infrastructure.ThirdParty.SsMember.Model.GoodsSync;

namespace Ss.CmbXft.Infrastructure.ThirdParty.SsMember.Interfaces;

/// <summary>
/// 昇菘会员系统商品同步服务接口
/// </summary>
public interface ISsMemberGoodsService
{
    /// <summary>
    /// 同步商品信息到会员系统
    /// </summary>
    /// <param name="products">商品列表</param>
    /// <param name="ct">取消令牌</param>
    /// <returns>同步结果</returns>
    Task<SsMemberResponse> SyncGoodsAsync(List<GoodsInfo> products, CancellationToken ct = default);
}