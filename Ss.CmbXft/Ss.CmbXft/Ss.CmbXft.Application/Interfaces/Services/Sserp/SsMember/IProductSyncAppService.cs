using Ss.CmbXft.Application.Dtos.Sserp.Product;
using Ss.CmbXft.Common.Models;

namespace Ss.CmbXft.Application.Interfaces.Services.Sserp.SsMember;

/// <summary>
/// 商品同步应用服务接口
/// 负责从SSERP数据库读取商品数据，拼凑JSON后同步到昇菘会员系统
/// </summary>
public interface IProductSyncAppService
{
    /// <summary>
    /// 同步商品到会员系统（根据查询条件筛选商品后同步）
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="ct">取消令牌</param>
    /// <returns>同步结果</returns>
    Task<ApiResult> SyncProductsAsync(ProductSyncQueryDto query, CancellationToken ct = default);
}
