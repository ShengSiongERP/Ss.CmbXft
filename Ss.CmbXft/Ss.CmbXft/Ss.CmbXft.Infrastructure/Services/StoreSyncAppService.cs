using Microsoft.Extensions.Logging;
using Ss.CmbXft.Application.Dtos.Sspos;
using Ss.CmbXft.Common.Models;
using Ss.CmbXft.Domain.Entities.Sspos;
using Ss.CmbXft.Domain.Repositories;
using Ss.CmbXft.Infrastructure.ThirdParty.SsMember.Interfaces;
using Ss.CmbXft.Infrastructure.ThirdParty.SsMember.Model.StoreSync;

namespace Ss.CmbXft.Infrastructure.Services;

/// <summary>
/// 门店同步(pos数据库)
/// </summary>
public class StoreSyncAppService : IStoreSyncAppService
{
    private readonly ISsposRepository<PosLocation, string> _posLocationRepository;
    private readonly ISsMemberStoreService _storeService;
    private readonly ILogger<StoreSyncAppService> _logger;

    public StoreSyncAppService(
        ISsposRepository<PosLocation, string> posLocationRepository,
        ISsMemberStoreService storeService,
        ILogger<StoreSyncAppService> logger)
    {
        _posLocationRepository = posLocationRepository;
        _storeService = storeService;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task<ApiResult> SyncStoresAsync(StoreSyncQueryDto query, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(query);
        var storeInfos = await _posLocationRepository.GetListAsync(predicate: x => x.Active);

        if (storeInfos.Count == 0)
        {
            _logger.LogWarning("没有找到符合条件的门店，跳过同步");
            return ApiResult.Error(ApiResultEnum.Fail, "没有找到符合条件的门店");
        }

        var list = storeInfos.Select(x => new StoreInfo
        {
            StoreId = x.LocationCode,
            StoreName = x.LocationName,
            Address = x.Address1 ?? ""
        }).ToList();

#if DEBUG
        var storeInfo = new StoreInfo();
        storeInfo.StoreId = "TEST01";
        storeInfo.StoreName = "TEST";
        storeInfo.Address = "中国云南省昆明市西山区广福路488号爱琴海购物公园地下一层B1001号";
        list = new List<StoreInfo> { storeInfo };
#endif

        var response = await _storeService.SyncStoresAsync(list, ct);

        if (response.IsSuccess)
        {
            return ApiResult.Success($"成功同步 {storeInfos.Count} 个门店");
        }

        return ApiResult.Error(ApiResultEnum.ServerError, $"同步失败: {response.GetErrorMessage()}");
    }
}
