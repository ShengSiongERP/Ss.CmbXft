using Newtonsoft.Json;

namespace Ss.CmbXft.Infrastructure.ThirdParty.SsMember.Model.CouponSync;

/// <summary>
/// 优惠券信息（用于同步到会员系统）
/// </summary>
public class CouponSyncInfo
{
    /// <summary>
    /// ERP优惠券ID（重复ID视为更新，必填）
    /// </summary>
    [JsonProperty("code_id")]
    public string CodeId { get; set; } = string.Empty;

    /// <summary>
    /// 优惠券数量（必填）
    /// </summary>
    [JsonProperty("count")]
    public int Count { get; set; }

    /// <summary>
    /// 满多少（可选）
    /// </summary>
    [JsonProperty("enough")]
    public string? Enough { get; set; }

    /// <summary>
    /// 减多少（可选）
    /// </summary>
    [JsonProperty("deduct")]
    public string? Deduct { get; set; }

    /// <summary>
    /// 折扣（可选，如85表示8.5折）
    /// </summary>
    [JsonProperty("discount")]
    public int? Discount { get; set; }

    /// <summary>
    /// 可用门店，多个英文逗号隔开（可选）
    /// </summary>
    [JsonProperty("store_id")]
    public string? StoreId { get; set; }

    /// <summary>
    /// 代金多少（可选）
    /// </summary>
    [JsonProperty("money")]
    public string? Money { get; set; }

    /// <summary>
    /// 优惠券说明（可选）
    /// </summary>
    [JsonProperty("remark")]
    public string? Remark { get; set; }

    /// <summary>
    /// 有效期开始时间 yyyy-mm-dd（必填）
    /// </summary>
    [JsonProperty("start_time")]
    public string StartTime { get; set; } = string.Empty;

    /// <summary>
    /// 有效期结束时间 yyyy-mm-dd（必填）
    /// </summary>
    [JsonProperty("finish_time")]
    public string FinishTime { get; set; } = string.Empty;

    /// <summary>
    /// 有效期方式：1是开始结束 2领取几天内后过期 3两个维度控制（必填）
    /// </summary>
    [JsonProperty("time_mode")]
    public string TimeMode { get; set; } = "1";

    /// <summary>
    /// 领取几天内后过期（必填）
    /// </summary>
    [JsonProperty("receive_time")]
    public string ReceiveTime { get; set; } = string.Empty;

    /// <summary>
    /// 优惠券类型 1满减 2折扣 3代金劵（可选）
    /// </summary>
    [JsonProperty("coupon_type")]
    public string? CouponType { get; set; }

    /// <summary>
    /// 券名称（必填）
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 券图片（必填）
    /// </summary>
    [JsonProperty("img_src")]
    public string ImgSrc { get; set; } = string.Empty;
}

/// <summary>
/// 代金券同步请求
/// 接口URL: https://member.shengsiongcn.com/api/erp/coupon_sync
/// </summary>
public class CouponSyncRequest
{
    /// <summary>
    /// 当前时间戳（10位）
    /// </summary>
    [JsonProperty("current")]
    public long Current { get; set; }

    /// <summary>
    /// 签名
    /// </summary>
    [JsonProperty("sign")]
    public string Sign { get; set; } = string.Empty;

    /// <summary>
    /// 优惠券列表
    /// </summary>
    [JsonProperty("coupon_list")]
    public List<CouponSyncInfo> CouponList { get; set; } = new();
}

/// <summary>
/// 代金券查询
/// 接口URL: https://member.shengsiongcn.com/api/erp/coupon
/// </summary>
public class GetCouponRequest
{
    /// <summary>
    /// 当前时间戳（10位）
    /// </summary>
    [JsonProperty("current")]
    public long Current { get; set; }

    /// <summary>
    /// 签名
    /// </summary>
    [JsonProperty("sign")]
    public string Sign { get; set; } = string.Empty;
}
