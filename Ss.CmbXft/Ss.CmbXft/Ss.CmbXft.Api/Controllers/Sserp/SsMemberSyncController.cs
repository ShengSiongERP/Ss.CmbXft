using Microsoft.AspNetCore.Mvc;
using Ss.CmbXft.Api.Infrastructure.Common;
using Ss.CmbXft.Application.Dtos.Sserp.Product;
using Ss.CmbXft.Application.Dtos.Sspos;
using Ss.CmbXft.Application.Interfaces.Services.Sserp.SsMember;
using Ss.CmbXft.Common.Models;
using Ss.CmbXft.Infrastructure.ThirdParty.SsMember.Interfaces;

namespace Ss.CmbXft.Api.Controllers.Sserp;

/// <summary>
/// 数据同步到昇菘会员系统
/// </summary>
[Route("api/sserp/ssmember_sync")]
public class SsMemberSyncController : ApiControllerBase
{
    private readonly IProductSyncAppService _productSyncService;
    private readonly ICouponSyncAppService _couponSyncAppService;
    private readonly IStoreSyncAppService _storeSyncAppService;

    public SsMemberSyncController(
        IProductSyncAppService productSyncService,
        ICouponSyncAppService couponSyncAppService,
        IStoreSyncAppService storeSyncAppService,
        ILogger<SsMemberSyncController> logger) : base(logger)
    {
        _productSyncService = productSyncService;
        _couponSyncAppService = couponSyncAppService;
        _storeSyncAppService = storeSyncAppService;
    }
    /// <summary>
    /// 同步门店
    /// </summary>
    [HttpPost("sync_store")]
    public async Task<ApiResult> SyncStore()
    {
        var result = await _storeSyncAppService.SyncStoresAsync(new StoreSyncQueryDto());
        return result;
    }

    /// <summary>
    /// 同步代金券
    /// </summary>
    [HttpPost("sync_voucher")]
    public async Task<ApiResult> SyncVoucher()
    {
        var result = await _couponSyncAppService.SyncCouponsAsync(new CouponSyncQueryDto());
        return result;
    }

    /// <summary>
    /// 同步商品
    /// </summary>
    [HttpPost("sync_goods")]
    public async Task<ApiResult> SyncGoods([FromBody] ProductSyncQueryDto query)
    {
        var result = await _productSyncService.SyncProductsAsync(query);
        return result;
    }
}
