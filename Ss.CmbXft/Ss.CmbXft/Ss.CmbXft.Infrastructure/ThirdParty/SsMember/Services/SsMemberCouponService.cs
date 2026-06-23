using Microsoft.Extensions.Logging;
using Ss.CmbXft.Domain.ThirdParty.SsMember;
using Ss.CmbXft.Infrastructure.ThirdParty;
using Ss.CmbXft.Infrastructure.ThirdParty.SsMember.Model.CouponSync;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ss.CmbXft.Infrastructure.ThirdParty.SsMember.Interfaces;

namespace Ss.CmbXft.Infrastructure.ThirdParty.SsMember.Services;

/// <summary>
/// 昇菘会员系统优惠券同步服务实现
/// </summary>
public class SsMemberCouponService : ISsMemberCouponService
{
    private readonly ISsMemberClient _client;
    private readonly ILogger<SsMemberCouponService> _logger;

    public SsMemberCouponService(
        ISsMemberClient client,
        ILogger<SsMemberCouponService> logger)
    {
        _client = client;
        _logger = logger;
    }

    /// <summary>
    /// 获取优惠券 需要formdata ,code_sign不知道是什么
    /// </summary>
    public async Task<SsMemberResponse> GetCouponsAsync(CancellationToken ct = default)
    {
        //var request = new GetCouponRequest
        //{
        //    CouponList = coupons
        //};
        var response = await _client.PostAsync<GetCouponRequest, SsMemberResponse>(
            "/api/erp/coupon", null, ct);

        if (response.IsSuccess)
        {
            //_logger.LogInformation("成功同步 {Count} 个优惠券到会员系统", coupons.Count);
        }
        else
        {
            _logger.LogError("同步优惠券到会员系统失败: {Message}", response.GetErrorMessage());
        }

        return response;
    }

    /// <summary>
    /// 同步优惠券信息到会员系统
    /// </summary>
    public async Task<SsMemberResponse> SyncCouponsAsync(List<CouponSyncInfo> coupons, CancellationToken ct = default)
    {
        if (coupons == null || !coupons.Any())
        {
            _logger.LogWarning("优惠券列表为空，跳过同步");
            return new SsMemberResponse
            {
                Code = 400,
                Status = "fail",
                Message = "优惠券列表不能为空"
            };
        }

        _logger.LogInformation("开始同步 {Count} 个优惠券到会员系统", coupons.Count);

        var request = new CouponSyncRequest
        {
            CouponList = coupons
        };

        var response = await _client.PostAsync<CouponSyncRequest, SsMemberResponse>(
            "/api/erp/coupon_sync", request, ct);

        if (response.IsSuccess)
        {
            _logger.LogInformation("成功同步 {Count} 个优惠券到会员系统", coupons.Count);
        }
        else
        {
            _logger.LogError("同步优惠券到会员系统失败: {Message}", response.GetErrorMessage());
        }

        return response;
    }
}
