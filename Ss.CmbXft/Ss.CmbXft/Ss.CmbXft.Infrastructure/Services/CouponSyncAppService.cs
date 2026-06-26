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
    private readonly ISsposRepository<PosControlLocation, string> _posControlLocationRepository;
    private readonly ISsposRepository<PosLocation, string> _posLocationRepository;
    private readonly ISsMemberCouponService _couponService;
    private readonly ILogger<CouponSyncAppService> _logger;

    public CouponSyncAppService(
        ISsposRepository<PosVoucher, string> posVoucherRepository,
        ISsposRepository<PosControlLocation, string> posControlLocationRepository,
        ISsposRepository<PosLocation, string> posLocationRepository,
        ISsMemberCouponService couponService,
        ILogger<CouponSyncAppService> logger)
    {
        _posVoucherRepository = posVoucherRepository;
        _posControlLocationRepository = posControlLocationRepository;
        _posLocationRepository = posLocationRepository;
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

        // 获取所有激活的控制门店数据
        var controlLocations = await _posControlLocationRepository.GetListAsync(predicate: x => x.Active == true);
        // 获取所有激活的门店数据（用于处理 ALL 的情况）
        var storeInfos = await _posLocationRepository.GetListAsync(predicate: x => x.Active);

        var list = couponInfos.Select(x =>
        {
            // 根据优惠券编码（VoucherCode = LinkedCode）查找关联的门店
            var relatedLocations = controlLocations
                .Where(cl => cl.LinkedCode == x.VoucherCode && cl.Active == true)
                .ToList();

            string storeId = string.Empty;

            if (relatedLocations.Any())
            {
                // 检查是否有 ALL 标记
                if (relatedLocations.Any(rl => rl.LocationCode == "ALL"))
                {
                    // 如果有 ALL，则使用所有激活门店的 LocationCode 拼接
                    storeId = string.Join(",", storeInfos.Select(s => s.LocationCode));
                    _logger.LogInformation($"优惠券 {x.VoucherCode} 关联所有门店，门店数量：{storeInfos.Count}");
                }
                else
                {
                    // 否则，使用该优惠券关联的所有 LocationCode 拼接
                    storeId = string.Join(",", relatedLocations
                        .Where(rl => !string.IsNullOrEmpty(rl.LocationCode))
                        .Select(rl => rl.LocationCode));
                    _logger.LogInformation($"优惠券 {x.VoucherCode} 关联指定门店：{storeId}");
                }
            }
            else
            {
                _logger.LogWarning($"优惠券 {x.VoucherCode} 没有找到关联的门店数据");
            }

            return new CouponSyncInfo
            {
                CouponType = "3", //1满减 2折扣 3代金劵
                Name = x.VoucherName,
                ImgSrc = x.VoucherImg != null && x.VoucherImg.Length > 0 ? "data:image/jpeg;base64," + Convert.ToBase64String(x.VoucherImg) : string.Empty,
                CodeId = x.VoucherCode,
                Money = x.VoucherAmount.ToString("F2"),
                Count = (int)(x.TotalQty ?? 0),
                Enough = x.MinPurchaseAmount?.ToString(),
                Deduct = "0",
                Discount = 0,
                StoreId = storeId,
                Remark = string.Empty,
                StartTime = x.ValidFrom.ToString("yyyy-MM-dd"),
                FinishTime = x.ValidTo.ToString("yyyy-MM-dd"),
                TimeMode = "1",
                ReceiveTime = string.Empty,
            };
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
            StoreId = "KM01,KM02,KM03,KM04,KM05,KM06",
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
