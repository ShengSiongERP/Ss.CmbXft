using System.Collections.Generic;
using Newtonsoft.Json;

namespace Ss.CmbXft.Sdk.Models.Role;

/// <summary>
/// 获取企业下所有角色响应Body
/// </summary>
public class RoleListResponseBody
{
    /// <summary>
    /// 当前页
    /// </summary>
    [JsonProperty("currentPage")]
    public long? CurrentPage { get; set; }

    /// <summary>
    /// 每页大小
    /// </summary>
    [JsonProperty("pageSize")]
    public long? PageSize { get; set; }

    /// <summary>
    /// 总条数
    /// </summary>
    [JsonProperty("totalSize")]
    public long? TotalSize { get; set; }

    /// <summary>
    /// 数据记录
    /// </summary>
    [JsonProperty("records")]
    public List<RoleInfo> Records { get; set; } = new();
}

/// <summary>
/// 角色信息
/// </summary>
public class RoleInfo
{
    /// <summary>
    /// 角色编号
    /// </summary>
    [JsonProperty("roleCode")]
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// 角色名称
    /// </summary>
    [JsonProperty("roleName")]
    public string Name { get; set; } = string.Empty;
}
