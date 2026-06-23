using System;
using System.Collections.Generic;

namespace Ss.CmbXft.Infrastructure.ThirdParty.SsMember;

/// <summary>
/// 昇菘超市会员系统API配置选项
/// </summary>
public class SsMemberOptions
{
    /// <summary>
    /// 应用标识
    /// </summary>
    public string AppId { get; set; } = string.Empty;

    /// <summary>
    /// 应用密钥（用于签名）
    /// </summary>
    public string AppKey { get; set; } = string.Empty;

    /// <summary>
    /// 基础URL（例如：https://member.shengsiongcn.com）
    /// </summary>
    public string BaseUrl { get; set; } = string.Empty;

    /// <summary>
    /// 请求超时时间（秒），默认30秒
    /// </summary>
    public int TimeoutSeconds { get; set; } = 30;

    /// <summary>
    /// 是否启用日志记录，默认true
    /// </summary>
    public bool EnableLogging { get; set; } = true;

    /// <summary>
    /// 验证配置是否有效
    /// </summary>
    /// <param name="errors">输出错误信息列表</param>
    /// <returns>配置是否有效</returns>
    public bool Validate(out List<string> errors)
    {
        errors = new List<string>();

        if (string.IsNullOrWhiteSpace(AppId))
            errors.Add("AppId 不能为空");

        if (string.IsNullOrWhiteSpace(AppKey))
            errors.Add("AppKey 不能为空");

        if (string.IsNullOrWhiteSpace(BaseUrl))
            errors.Add("BaseUrl 不能为空");

        if (!Uri.TryCreate(BaseUrl, UriKind.Absolute, out _))
            errors.Add("BaseUrl 不是有效的URL格式");

        if (TimeoutSeconds <= 0)
            errors.Add("TimeoutSeconds 必须大于0");

        return errors.Count == 0;
    }

    /// <summary>
    /// 获取API完整URL
    /// </summary>
    /// <param name="path">API路径</param>
    /// <returns>完整URL</returns>
    public string GetApiUrl(string path)
    {
        var baseUrl = BaseUrl.EndsWith("/") ? BaseUrl.TrimEnd('/') : BaseUrl;
        var apiPath = path.StartsWith("/") ? path : $"/{path}";
        return $"{baseUrl}{apiPath}";
    }
}
