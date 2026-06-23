using System.Collections.Generic;
using Newtonsoft.Json;

namespace Ss.CmbXft.Sdk.Models.Position;

/// <summary>
/// 分页查询职位响应Body
/// </summary>
public class PositionQueryResponseBody
{
    /// <summary>
    /// 当前页数，起始值1
    /// </summary>
    [JsonProperty("currentPage")]
    public long? CurrentPage { get; set; }

    /// <summary>
    /// 每页大小
    /// </summary>
    [JsonProperty("pageSize")]
    public long? PageSize { get; set; }

    /// <summary>
    /// 数据总条数
    /// </summary>
    [JsonProperty("totalSize")]
    public long? TotalSize { get; set; }

    /// <summary>
    /// 查询数据列表
    /// </summary>
    [JsonProperty("records")]
    public List<PositionInfo> Records { get; set; } = new();
}

/// <summary>
/// 职位信息
/// </summary>
public class PositionInfo
{
    /// <summary>
    /// 职位流水号，最大长度10
    /// </summary>
    [JsonProperty("sequenceNumber")]
    public string SequenceNumber { get; set; } = string.Empty;

    /// <summary>
    /// 职位编号，最大长度8
    /// </summary>
    [JsonProperty("codeNumber")]
    public string CodeNumber { get; set; } = string.Empty;

    /// <summary>
    /// 职位名称，最大长度150
    /// </summary>
    [JsonProperty("jobName")]
    public string JobName { get; set; } = string.Empty;

    /// <summary>
    /// 备注，最大长度600
    /// </summary>
    [JsonProperty("remark")]
    public string? Remark { get; set; }

    /// <summary>
    /// 顺序号
    /// </summary>
    [JsonProperty("orderNumber")]
    public string? OrderNumber { get; set; }
}
