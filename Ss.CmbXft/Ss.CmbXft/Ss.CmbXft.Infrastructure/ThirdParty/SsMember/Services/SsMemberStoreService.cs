using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Ss.CmbXft.Domain.ThirdParty.SsMember;
using Ss.CmbXft.Infrastructure.ThirdParty;
using Ss.CmbXft.Infrastructure.ThirdParty.SsMember.Model.StoreSync;
using Ss.CmbXft.Infrastructure.ThirdParty.SsMember.Interfaces;

namespace Ss.CmbXft.Infrastructure.ThirdParty.SsMember.Services;

/// <summary>
/// 昇菘会员系统门店同步服务实现
/// </summary>
public class SsMemberStoreService : ISsMemberStoreService
{
    private readonly ISsMemberClient _client;
    private readonly ILogger<SsMemberStoreService> _logger;

    public SsMemberStoreService(
        ISsMemberClient client,
        ILogger<SsMemberStoreService> logger)
    {
        _client = client;
        _logger = logger;
    }

    /// <summary>
    /// 同步门店信息到会员系统
    /// </summary>
    public async Task<SsMemberResponse> SyncStoresAsync(List<StoreInfo> stores, CancellationToken ct = default)
    {
        if (stores == null || !stores.Any())
        {
            _logger.LogWarning("门店列表为空，跳过同步");
            return new SsMemberResponse
            {
                Code = 400,
                Status = "fail",
                Message = "门店列表不能为空"
            };
        }
        _logger.LogInformation("开始同步 {Count} 个门店到会员系统", stores.Count);

        var request = new StoreSyncRequest
        {
            Store = stores
        };
        var response = await _client.PostAsync<StoreSyncRequest, SsMemberResponse>(
            "/api/erp/store_sync", request, ct);
        if (response.IsSuccess)
        {
            _logger.LogInformation("成功同步 {Count} 个门店到会员系统", stores.Count);
        }
        else
        {
            _logger.LogError("同步门店到会员系统失败: {Message}", response.GetErrorMessage());
        }
        return response;
    }
}
