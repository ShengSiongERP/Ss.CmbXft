using System.Collections.Generic;
using Newtonsoft.Json;

namespace Ss.CmbXft.Sdk.Models.Staff;

/// <summary>
/// 数据字典项
/// </summary>
public class DataDictionaryItem
{
    /// <summary>
    /// ID
    /// </summary>
    [JsonProperty("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// 归属ID
    /// </summary>
    [JsonProperty("belongingId")]
    public string? BelongingId { get; set; }

    /// <summary>
    /// 编码
    /// </summary>
    [JsonProperty("code")]
    public string? Code { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 排序号
    /// </summary>
    [JsonProperty("orderNumber")]
    public int? OrderNumber { get; set; }

    /// <summary>
    /// 上级ID
    /// </summary>
    [JsonProperty("upperId")]
    public string? UpperId { get; set; }

    /// <summary>
    /// 开启状态
    /// </summary>
    [JsonProperty("openStatus")]
    public bool? OpenStatus { get; set; }
}

/// <summary>
/// 数据字典查询结果项
/// </summary>
public class DataDictionaryQueryResultItem
{
    /// <summary>
    /// 查询类型
    /// </summary>
    [JsonProperty("queryType")]
    public string QueryType { get; set; } = string.Empty;

    /// <summary>
    /// 数据列表
    /// </summary>
    [JsonProperty("dataList")]
    public List<DataDictionaryItem> DataList { get; set; } = new List<DataDictionaryItem>();
}

/// <summary>
/// 员工数据字典查询响应体
/// </summary>
public class DataDictionaryQueryResponseBody : List<DataDictionaryQueryResultItem>
{
}
