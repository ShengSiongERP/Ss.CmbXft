using Newtonsoft.Json;

namespace Ss.CmbXft.Sdk.Models.Events;

/// <summary>
/// 薪福通事件推送请求模型
/// </summary>
public class XftEventPushRequest
{
    /// <summary>
    /// 事件ID
    /// </summary>
    [JsonProperty("eventId")]
    public string EventId { get; set; } = string.Empty;

    /// <summary>
    /// 事件变更信息（JSON字符串）
    /// </summary>
    [JsonProperty("eventRcdInf")]
    public string EventRcdInf { get; set; } = string.Empty;

    /// <summary>
    /// 企业号（平台类事件没有企业号）
    /// </summary>
    [JsonProperty("prjCod")]
    public string? PrjCod { get; set; }

    /// <summary>
    /// 变更时间
    /// </summary>
    [JsonProperty("eventTime")]
    public string EventTime { get; set; } = string.Empty;

    /// <summary>
    /// 事件唯一记录码
    /// </summary>
    [JsonProperty("eventCd")]
    public long EventCd { get; set; }

    /// <summary>
    /// 事件业务码
    /// </summary>
    [JsonProperty("businessKey")]
    public string BusinessKey { get; set; } = string.Empty;

    /// <summary>
    /// 应用ID
    /// </summary>
    [JsonProperty("appId")]
    public string AppId { get; set; } = string.Empty;

    /// <summary>
    /// 签名字段
    /// </summary>
    [JsonProperty("signature")]
    public string? Signature { get; set; }
}
