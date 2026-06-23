using Newtonsoft.Json;
using Ss.CmbXft.Domain.ThirdParty.SsMember;
using Ss.CmbXft.Infrastructure.ThirdParty.SsMember.Model.CouponSync;

namespace Ss.CmbXft.Infrastructure.ThirdParty.SsMember.Model.UserInfo;

/// <summary>
/// 获取用户信息响应
/// </summary>
public class GetUserInfoResponse : SsMemberResponse<SsUserInfoData>
{
    /// <summary>
    /// 响应数据（用户信息）
    /// </summary>
    [JsonProperty("data")]
    public new SsUserInfoData? Data { get; set; }
}

/// <summary>
/// 用户信息数据
/// </summary>
public class SsUserInfoData
{
    /// <summary>
    /// 会员余额
    /// </summary>
    [JsonProperty("money")]
    public string Money { get; set; } = string.Empty;

    /// <summary>
    /// 会员积分
    /// </summary>
    [JsonProperty("integral")]
    public string Integral { get; set; } = string.Empty;

    /// <summary>
    /// 优惠券列表
    /// </summary>
    [JsonProperty("coupon_list")]
    public List<SsUserInfoCouponInfo>? CouponList { get; set; }

    /// <summary>
    /// 客户唯一ID
    /// </summary>
    [JsonProperty("UID")]
    public string Uid { get; set; } = string.Empty;
}

/// <summary>
/// 优惠券信息
/// </summary>
public class SsUserInfoCouponInfo
{
    /// <summary>
    /// 优惠券ID
    /// </summary>
    [JsonProperty("code__id")]
    public string CodeId { get; set; } = string.Empty;

    /// <summary>
    /// 数量
    /// </summary>
    [JsonProperty("count")]
    public string Count { get; set; } = string.Empty;
}