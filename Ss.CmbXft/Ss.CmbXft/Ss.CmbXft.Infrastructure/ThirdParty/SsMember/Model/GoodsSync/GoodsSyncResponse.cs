using Newtonsoft.Json;
using Ss.CmbXft.Domain.ThirdParty.SsMember;

namespace Ss.CmbXft.Infrastructure.ThirdParty.SsMember.Model.GoodsSync;

/// <summary>
/// 商品信息同步响应
/// </summary>
public class GoodsSyncResponse : SsMemberResponse
{
    /// <summary>
    /// 响应数据（同步成功时为空对象{}）
    /// </summary>
    [JsonProperty("data")]
    public object? Data { get; set; }
}
