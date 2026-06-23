using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Ss.CmbXft.Sdk.Models.Post;

/// <summary>
/// 分页查询岗位请求
/// </summary>
public class PostQueryRequest
{
    /// <summary>
    /// 流水号列表，每项最大长度为10，最大1000个
    /// </summary>
    [JsonProperty("sequenceNumbers")]
    public List<string>? SequenceNumbers { get; set; }

    /// <summary>
    /// 岗位名称，最大长度150，支持模糊查询
    /// </summary>
    [JsonProperty("positionName")]
    public string? PositionName { get; set; }

    /// <summary>
    /// 所属组织机构ID，每项最大长度为4，最大1000个
    /// </summary>
    [JsonProperty("organizationIds")]
    public List<string>? OrganizationIds { get; set; }

    /// <summary>
    /// 岗位编号，最大长度8，支持模糊查询
    /// </summary>
    [JsonProperty("codeNumber")]
    public string? CodeNumber { get; set; }

    /// <summary>
    /// 当前页数，起始值1，必填
    /// </summary>
    [JsonProperty("currentPage")]
    public long CurrentPage { get; set; } = 1;

    /// <summary>
    /// 每页大小，默认值10，最大支持1000，必填
    /// </summary>
    [JsonProperty("pageSize")]
    public long PageSize { get; set; } = 10;

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

        if (PageSize < 1 || PageSize > 1000)
        {
            errors.Add("每页大小必须在1-1000之间");
        }

        if (SequenceNumbers != null && SequenceNumbers.Count > 1000)
        {
            errors.Add("流水号列表数量不能超过1000");
        }

        if (OrganizationIds != null && OrganizationIds.Count > 1000)
        {
            errors.Add("所属组织机构ID数量不能超过1000");
        }

        if (!string.IsNullOrEmpty(PositionName) && PositionName.Length > 150)
        {
            errors.Add("岗位名称长度不能超过150");
        }

        if (!string.IsNullOrEmpty(CodeNumber) && CodeNumber.Length > 8)
        {
            errors.Add("岗位编号长度不能超过8");
        }

        return errors.Count == 0;
    }
}
