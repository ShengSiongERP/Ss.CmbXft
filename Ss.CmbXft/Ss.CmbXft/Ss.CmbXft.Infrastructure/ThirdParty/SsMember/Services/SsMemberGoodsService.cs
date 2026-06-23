using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Ss.CmbXft.Domain.ThirdParty.SsMember;
using Ss.CmbXft.Infrastructure.ThirdParty;
using Ss.CmbXft.Infrastructure.ThirdParty.SsMember.Model.GoodsSync;
using Ss.CmbXft.Infrastructure.ThirdParty.SsMember.Model.UserInfo;
using Ss.CmbXft.Infrastructure.ThirdParty.SsMember.Interfaces;

namespace Ss.CmbXft.Infrastructure.ThirdParty.SsMember.Services;

/// <summary>
/// 昇菘会员系统商品同步服务实现
/// </summary>
public class SsMemberGoodsService : ISsMemberGoodsService
{
    private readonly ISsMemberClient _client;
    private readonly ILogger<SsMemberGoodsService> _logger;

    public SsMemberGoodsService(
        ISsMemberClient client,
        ILogger<SsMemberGoodsService> logger)
    {
        _client = client;
        _logger = logger;
    }

    /// <summary>
    /// 同步商品信息到会员系统
    /// </summary>
    public async Task<SsMemberResponse> SyncGoodsAsync(List<GoodsInfo> products, CancellationToken ct = default)
    {
        if (products == null || !products.Any())
        {
            _logger.LogWarning("商品列表为空，跳过同步");
            return new SsMemberResponse
            {
                Code = 400,
                Status = "fail",
                Message = "商品列表不能为空"
            };
        }

        _logger.LogInformation("开始同步 {Count} 个商品到会员系统", products.Count);

        var request = new GoodsSyncRequest
        {
            Products = products
        };

        var response = await _client.PostAsync<GoodsSyncRequest, SsMemberResponse>(
            "/api/erp/goods_sync", request, ct);

        if (response.IsSuccess)
        {
            _logger.LogInformation("成功同步 {Count} 个商品到会员系统", products.Count);
        }
        else
        {
            _logger.LogError("同步商品到会员系统失败: {Message}", response.GetErrorMessage());
        }

        return response;
    }
}
