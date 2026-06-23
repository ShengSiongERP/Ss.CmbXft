using System.Collections.Generic;
using Newtonsoft.Json;

namespace Ss.CmbXft.Sdk.Models.Staff;

/// <summary>
/// 员工数据字典查询请求项
/// </summary>
public class DataDictionaryQueryItem
{
    /// <summary>
    /// 查询类型
    /// </summary>
    [JsonProperty("queryType")]
    public string QueryType { get; set; } = string.Empty;

    /// <summary>
    /// 开启状态（可选）
    /// </summary>
    [JsonProperty("openStatus")]
    public bool? OpenStatus { get; set; }
}

/// <summary>
/// 员工数据字典查询请求
/// </summary>
public class DataDictionaryQueryRequest : List<DataDictionaryQueryItem>
{
}
