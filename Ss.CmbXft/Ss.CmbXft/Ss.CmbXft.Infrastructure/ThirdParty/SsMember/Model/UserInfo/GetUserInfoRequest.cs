using Newtonsoft.Json;

namespace Ss.CmbXft.Infrastructure.ThirdParty.SsMember.Model.UserInfo;

/// <summary>
/// 获取用户信息请求
/// 接口URL: https://member.shengsiongcn.com/api/erp/user
/// </summary>
public class GetUserInfoRequest
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
    /// 用户标识（动态二维码，必填）
    /// </summary>
    [JsonProperty("user_sign")]
    public string UserSign { get; set; } = string.Empty;
}
