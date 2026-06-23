using System.Collections.Generic;
using Newtonsoft.Json;

namespace Ss.CmbXft.Sdk.Models.Post;

/// <summary>
/// 分页查询岗位响应Body
/// </summary>
public class PostQueryResponseBody
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
    public List<PostInfo> Records { get; set; } = new();
}

/// <summary>
/// 岗位信息
/// </summary>
public class PostInfo
{
    /// <summary>
    /// 岗位流水号，最大长度10
    /// </summary>
    [JsonProperty("sequenceNumber")]
    public string SequenceNumber { get; set; } = string.Empty;

    /// <summary>
    /// 岗位编号，最大长度8
    /// </summary>
    [JsonProperty("codeNumber")]
    public string CodeNumber { get; set; } = string.Empty;

    /// <summary>
    /// 岗位名称，最大长度150
    /// </summary>
    [JsonProperty("positionName")]
    public string PositionName { get; set; } = string.Empty;

    /// <summary>
    /// 所属组织机构列表
    /// </summary>
    [JsonProperty("organizations")]
    public List<PostOrganization>? Organizations { get; set; }

    /// <summary>
    /// 备注，最大长度600
    /// </summary>
    [JsonProperty("remark")]
    public string? Remark { get; set; }

    /// <summary>
    /// 顺序号，页面查询展示的优先级权重参数，数值越小，排序优先级越高，默认为9999
    /// </summary>
    [JsonProperty("orderNumber")]
    public string? OrderNumber { get; set; }
}

/// <summary>
/// 岗位所属组织机构
/// </summary>
public class PostOrganization
{
    /// <summary>
    /// 组织ID，最大长度4
    /// </summary>
    [JsonProperty("organizationId")]
    public string OrganizationId { get; set; } = string.Empty;

    /// <summary>
    /// 组织名称，最大长度100
    /// </summary>
    [JsonProperty("organizationName")]
    public string OrganizationName { get; set; } = string.Empty;

    /// <summary>
    /// 组织名称路径
    /// </summary>
    [JsonProperty("organizationNamePath")]
    public string? OrganizationNamePath { get; set; }
}
