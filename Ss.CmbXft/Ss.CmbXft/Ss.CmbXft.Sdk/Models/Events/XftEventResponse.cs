using Newtonsoft.Json;

namespace Ss.CmbXft.Sdk.Models.Events;

/// <summary>
/// 薪福通事件响应模型
/// </summary>
public class XftEventResponse
{
    /// <summary>
    /// 返回码：200表示成功
    /// </summary>
    [JsonProperty("rtnCod")]
    public string RtnCod { get; set; } = string.Empty;

    /// <summary>
    /// 错误消息
    /// </summary>
    [JsonProperty("errMsg")]
    public string ErrMsg { get; set; } = string.Empty;

    /// <summary>
    /// 创建成功响应
    /// </summary>
    public static XftEventResponse Success()
    {
        return new XftEventResponse
        {
            RtnCod = "200",
            ErrMsg = string.Empty
        };
    }

    /// <summary>
    /// 创建失败响应
    /// </summary>
    public static XftEventResponse Fail(string errorMessage)
    {
        return new XftEventResponse
        {
            RtnCod = "500",
            ErrMsg = errorMessage
        };
    }
}
