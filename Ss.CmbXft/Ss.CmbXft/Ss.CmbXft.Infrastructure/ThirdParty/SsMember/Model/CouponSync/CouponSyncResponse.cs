using Newtonsoft.Json;
using Ss.CmbXft.Domain.ThirdParty.SsMember;

namespace Ss.CmbXft.Infrastructure.ThirdParty.SsMember.Model.CouponSync;

/// <summary>
/// 代金券同步响应
/// </summary>
public class CouponSyncResponse : SsMemberResponse
{
    /// <summary>
    /// 响应数据（同步成功时为空对象{}）
    /// </summary>
    [JsonProperty("data")]
    public object? Data { get; set; }
}
