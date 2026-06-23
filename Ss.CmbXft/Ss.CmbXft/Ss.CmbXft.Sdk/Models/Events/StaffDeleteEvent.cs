using Newtonsoft.Json;

namespace Ss.CmbXft.Sdk.Models.Events;

/// <summary>
/// 删除员工事件模型
/// </summary>
public class StaffDeleteEvent
{
    /// <summary>
    /// 企业号
    /// </summary>
    [JsonProperty("PRJCOD")]
    public string? PrjCod { get; set; }

    /// <summary>
    /// 员工序号
    /// </summary>
    [JsonProperty("STFSEQ")]
    public string? StfSeq { get; set; }

    /// <summary>
    /// 操作时间
    /// </summary>
    [JsonProperty("OPRTSP")]
    public string? OprtSp { get; set; }

    /// <summary>
    /// 操作用户
    /// </summary>
    [JsonProperty("OPRUSR")]
    public string? OprUsr { get; set; }
}
