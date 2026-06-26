using Newtonsoft.Json;

namespace Ss.CmbXft.Infrastructure.ThirdParty.SsMember.Model.StoreSync;

/// <summary>
/// 门店信息（用于同步到会员系统）
/// </summary>
public class StoreInfo
{
    /// <summary>
    /// 门店唯一标识（必填）
    /// </summary>
    [JsonProperty("store_id")]
    public string StoreId { get; set; } = string.Empty;

    /// <summary>
    /// 门店名称（必填）
    /// </summary>
    [JsonProperty("store_name")]
    public string StoreName { get; set; } = string.Empty;

    /// <summary>
    /// 门店地址（必填）
    /// </summary>
    [JsonProperty("address")]
    public string Address { get; set; } = string.Empty;

    ///// <summary>
    ///// 门店信息（可选扩展数据 暂时用不到）
    ///// </summary>
    //[JsonProperty("data")]
    //public object? Data { get; set; }
}

/// <summary>
/// 门店信息同步请求
/// 接口URL: https://member.shengsiongcn.com/api/erp/store_sync
/// </summary>
public class StoreSyncRequest
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
    /// 门店列表
    /// </summary>
    [JsonProperty("store")]
    public List<StoreInfo> Store { get; set; } = new();
}
