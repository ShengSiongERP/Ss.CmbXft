using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Ss.CmbXft.Sdk.Models.Role;

/// <summary>
/// 获取企业下所有角色请求
/// </summary>
public class RoleListRequest
{
    /// <summary>
    /// 当前页
    /// </summary>
    [JsonProperty("currentPage")]
    public long CurrentPage { get; set; } = 1;

    /// <summary>
    /// 每页大小
    /// </summary>
    [JsonProperty("pageSize")]
    public long PageSize { get; set; } = 20;

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

        return errors.Count == 0;
    }
}
