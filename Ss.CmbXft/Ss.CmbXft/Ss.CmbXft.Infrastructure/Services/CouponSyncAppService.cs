using Microsoft.Extensions.Logging;
using Ss.CmbXft.Application.Dtos.Sspos;
using Ss.CmbXft.Common.Models;
using Ss.CmbXft.Domain.Entities.Sspos;
using Ss.CmbXft.Domain.Repositories;
using Ss.CmbXft.Infrastructure.ThirdParty.SsMember.Interfaces;
using Ss.CmbXft.Infrastructure.ThirdParty.SsMember.Model.CouponSync;

namespace Ss.CmbXft.Infrastructure.Services;

/// <summary>
/// 优惠券同步(pos数据库)
/// </summary>
public class CouponSyncAppService : ICouponSyncAppService
{
    private readonly ISsposRepository<PosVoucher, string> _posVoucherRepository;
    private readonly ISsMemberCouponService _couponService;
    private readonly ILogger<CouponSyncAppService> _logger;

    public CouponSyncAppService(
        ISsposRepository<PosVoucher, string> posVoucherRepository,
        ISsMemberCouponService couponService,
        ILogger<CouponSyncAppService> logger)
    {
        _posVoucherRepository = posVoucherRepository;
        _couponService = couponService;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task<ApiResult> SyncCouponsAsync(CouponSyncQueryDto query, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(query);
        var couponInfos = await _posVoucherRepository.GetListAsync(predicate: x => x.Active);

        if (couponInfos.Count == 0)
        {
            _logger.LogWarning("没有找到符合条件的优惠券，跳过同步");
            return ApiResult.Error(ApiResultEnum.Fail, "没有找到符合条件的优惠券");
        }

        var list = couponInfos.Select(x => new CouponSyncInfo
        {
            CouponType = "3", //1满减 2折扣 3代金劵
            Name = x.VoucherName,
            ImgSrc = x.VoucherImg != null && x.VoucherImg.Length > 0 ? "data:image/jpeg;base64," + Convert.ToBase64String(x.VoucherImg) : string.Empty,
            CodeId = x.VoucherCode,
            Money = x.VoucherAmount.ToString("F2"),
            Count = 0,
            Enough = x.MinPurchaseAmount?.ToString(),
            Deduct = "0",
            Discount = 0,
            StoreId = null,
            Remark = string.Empty,
            StartTime = x.ValidFrom.ToString("yyyy-MM-dd"),
            FinishTime = x.ValidTo.ToString("yyyy-MM-dd"),
            TimeMode = "1",
            ReceiveTime = string.Empty,
        }).ToList();

#if DEBUG
        var storeInfo = new CouponSyncInfo()
        {
            CouponType = "3", //1满减 2折扣 3代金劵
            Name = "测试优惠券1",
            ImgSrc = string.Empty,
            CodeId = "TESTCODE",
            Money = "1.00",
            Count = 1,
            Enough = "100000.00",
            Deduct = "0",
            Discount = 0,
            Remark = "这是一个测试优惠券1",
            StartTime = DateTime.Now.ToString("yyyy-MM-dd"),
            FinishTime = DateTime.Now.AddDays(2).ToString("yyyy-MM-dd"),
            TimeMode = "1",
            ReceiveTime = string.Empty,
        };
        list = new List<CouponSyncInfo> { storeInfo };
#endif

        var response = await _couponService.SyncCouponsAsync(list, ct);

        if (response.IsSuccess)
        {
            return ApiResult.Success($"成功同步 {couponInfos.Count} 个优惠券");
        }

        return ApiResult.Error(ApiResultEnum.ServerError, $"同步失败: {response.GetErrorMessage()}");
    }
}
