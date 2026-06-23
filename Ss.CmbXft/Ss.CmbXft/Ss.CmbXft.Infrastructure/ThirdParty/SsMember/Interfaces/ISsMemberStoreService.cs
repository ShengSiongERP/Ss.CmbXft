using Ss.CmbXft.Domain.ThirdParty.SsMember;
using Ss.CmbXft.Infrastructure.ThirdParty.SsMember.Model.StoreSync;

namespace Ss.CmbXft.Infrastructure.ThirdParty.SsMember.Interfaces;

/// <summary>
/// 昇菘会员系统门店同步服务接口
/// </summary>
public interface ISsMemberStoreService
{
    /// <summary>
    /// 同步门店信息到会员系统
    /// </summary>
    /// <param name="stores">门店列表</param>
    /// <param name="ct">取消令牌</param>
    /// <returns>同步结果</returns>
    Task<SsMemberResponse> SyncStoresAsync(List<StoreInfo> stores, CancellationToken ct = default);
}
