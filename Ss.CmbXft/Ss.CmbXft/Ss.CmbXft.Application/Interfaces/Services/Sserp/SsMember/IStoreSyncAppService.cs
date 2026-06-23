using Ss.CmbXft.Application.Dtos.Sspos;
using Ss.CmbXft.Common.Models;

namespace Ss.CmbXft.Infrastructure.ThirdParty.SsMember.Interfaces;

/// <summary>
/// 门店同步应用服务接口
/// 负责从POS数据库读取门店数据，拼凑JSON后同步到昇菘会员系统
/// </summary>
public interface IStoreSyncAppService
{
    /// <summary>
    /// 同步门店到会员系统（根据查询条件筛选门店后同步）
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="ct">取消令牌</param>
    /// <returns>同步结果</returns>
    Task<ApiResult> SyncStoresAsync(StoreSyncQueryDto query, CancellationToken ct = default);
}
