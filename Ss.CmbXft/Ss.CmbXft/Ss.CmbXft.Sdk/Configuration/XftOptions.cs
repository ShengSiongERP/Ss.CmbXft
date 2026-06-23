using System;
using System.Collections.Generic;

namespace Ss.CmbXft.Sdk.Configuration;

/// <summary>
/// 薪福通API配置选项
/// </summary>
public class XftOptions
{
    /// <summary>
    /// 企业号/租户号
    /// </summary>
    public string CompanyId { get; set; } = string.Empty;

    /// <summary>
    /// 开放平台AppId
    /// </summary>
    public string AppId { get; set; } = string.Empty;

    /// <summary>
    /// 授权密钥（至少32位）
    /// </summary>
    public string AuthoritySecret { get; set; } = string.Empty;

    /// <summary>
    /// 基础URL（例如：https://api.cmbchina.com 或 https://api.cmburl.cn:8065）
    /// </summary>
    public string BaseUrl { get; set; } = string.Empty;

    /// <summary>
    /// 环境类型（0=测试环境，1=生产环境）
    /// </summary>
    public XftEnvironment Environment { get; set; } = XftEnvironment.Test;

    /// <summary>
    /// 请求超时时间（秒），默认30秒
    /// </summary>
    public int TimeoutSeconds { get; set; } = 30;

    /// <summary>
    /// 是否启用日志记录，默认true
    /// </summary>
    public bool EnableLogging { get; set; } = true;

    /// <summary>
    /// 用户号（可选，默认AUTO0001）
    /// </summary>
    public string UsrUid { get; set; } = "AUTO0001";

    /// <summary>
    /// 平台用户号（可选，默认A0001）
    /// </summary>
    public string UsrNbr { get; set; } = "A0001";

    /// <summary>
    /// 验证配置是否有效
    /// </summary>
    /// <param name="errors">输出错误信息列表</param>
    /// <returns>配置是否有效</returns>
    public bool Validate(out List<string> errors)
    {
        errors = new List<string>();

        if (string.IsNullOrWhiteSpace(CompanyId))
            errors.Add("CompanyId 不能为空");

        if (string.IsNullOrWhiteSpace(AppId))
            errors.Add("AppId 不能为空");

        if (string.IsNullOrWhiteSpace(AuthoritySecret))
            errors.Add("AuthoritySecret 不能为空");

        if (AuthoritySecret.Length < 32)
            errors.Add("AuthoritySecret 长度不能少于32位");

        if (string.IsNullOrWhiteSpace(BaseUrl))
            errors.Add("BaseUrl 不能为空");

        if (!Uri.TryCreate(BaseUrl, UriKind.Absolute, out _))
            errors.Add("BaseUrl 不是有效的URL格式");

        if (TimeoutSeconds <= 0)
            errors.Add("TimeoutSeconds 必须大于0");

        return errors.Count == 0;
    }

    /// <summary>
    /// 获取加密密钥（AuthoritySecret的前32位）
    /// </summary>
    /// <returns>加密密钥</returns>
    public string GetEncryptionKey()
    {
        return AuthoritySecret.Length >= 32
            ? AuthoritySecret.Substring(0, 32)
            : AuthoritySecret.PadRight(32, '0');
    }
}

/// <summary>
/// 环境枚举
/// </summary>
public enum XftEnvironment
{
    ///// <summary>
    ///// 开发环境
    ///// </summary>
    //Development = 0,
    /// <summary>
    /// 测试环境
    /// </summary>
    Test = 1,
    ///// <summary>
    ///// 预发环境
    ///// </summary>
    //Staging = 2,
    /// <summary>
    /// 生产环境
    /// </summary>
    Production = 100
}