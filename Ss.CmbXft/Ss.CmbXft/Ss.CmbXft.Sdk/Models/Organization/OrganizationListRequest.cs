using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Ss.CmbXft.Sdk.Models.Organization;

/// <summary>
/// 获取组织列表请求
/// </summary>
public class OrganizationListRequest
{
    /// <summary>
    /// 组织编码集合，最大查询个数为2000
    /// </summary>
    [JsonProperty("codes")]
    public List<string>? Codes { get; set; }

    /// <summary>
    /// 组织id集合，最大查询个数为2000
    /// </summary>
    [JsonProperty("ids")]
    public List<string>? Ids { get; set; }

    /// <summary>
    /// 搜索关键词
    /// </summary>
    [JsonProperty("keyword")]
    public string? Keyword { get; set; }

    /// <summary>
    /// 父组织id
    /// </summary>
    [JsonProperty("parentId")]
    public string? ParentId { get; set; }

    /// <summary>
    /// 生效状态集合，active-正常，delete-删除，stopped-停用
    /// 不填时默认查询所有状态，包括已删除
    /// </summary>
    [JsonProperty("status")]
    public List<string>? Status { get; set; }

    /// <summary>
    /// 当前页，起始页为1；默认值为1，可不传值但不能传空
    /// </summary>
    [JsonProperty("currentPage")]
    public long CurrentPage { get; set; } = 1;

    /// <summary>
    /// 页大小，最大2000
    /// </summary>
    [JsonProperty("pageSize")]
    public long PageSize { get; set; } = 10;

    /// <summary>
    /// 扩展查询选项
    /// leader-组织负责人、approver-审批主管、extData-自定义字段、namePath-组织名称全路径、allDisplayOrderNumber-填充所有顺序号、leaf-查询组织是否为叶节点
    /// 不传值时默认查询组织负责人,审批主管,组织名称全路径，传值时按传值进行选项查询
    /// </summary>
    [JsonProperty("extOptions")]
    public List<string>? ExtOptions { get; set; }

    /// <summary>
    /// 验证请求参数
    /// </summary>
    /// <param name="errors">错误信息列表</param>
    /// <returns>验证是否通过</returns>
    public bool Validate(out List<string> errors)
    {
        errors = new List<string>();

        if (CurrentPage < 1)
        {
            errors.Add("当前页必须大于0");
        }

        if (PageSize < 1 || PageSize > 2000)
        {
            errors.Add("每页大小必须在1-2000之间");
        }

        if (Codes != null && Codes.Count > 2000)
        {
            errors.Add("组织编码集合最大查询个数为2000");
        }

        if (Ids != null && Ids.Count > 2000)
        {
            errors.Add("组织ID集合最大查询个数为2000");
        }

        return errors.Count == 0;
    }
}
