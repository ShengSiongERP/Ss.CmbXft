using System;
using System.Collections.Generic;
using System.Text;

namespace Ss.CmbXft.Sdk.Utils;

/// <summary>
/// 签名信息
/// </summary>
public class SignInfo
{
    /// <summary>
    /// 时间戳（秒）
    /// </summary>
    public string Timestamp { get; set; } = string.Empty;

    /// <summary>
    /// 完整URL
    /// </summary>
    public string Url { get; set; } = string.Empty;

    /// <summary>
    /// 路径
    /// </summary>
    public string Path { get; set; } = string.Empty;

    /// <summary>
    /// 初始化 <see cref="SignInfo"/> 类的新实例
    /// </summary>
    public SignInfo()
    {
    }

    /// <summary>
    /// 初始化 <see cref="SignInfo"/> 类的新实例
    /// </summary>
    /// <param name="timestamp">时间戳</param>
    /// <param name="url">完整URL</param>
    /// <param name="path">路径</param>
    public SignInfo(string timestamp, string url, string path)
    {
        Timestamp = timestamp;
        Url = url;
        Path = path;
    }
}

/// <summary>
/// 签名工具类
/// </summary>
public static class SignUtils
{
    private const string ParamFieldCscAppUid = "CSCAPPUID=";
    private const string ParamFieldCscPrjCod = "&CSCPRJCOD=";
    private const string ParamFieldCscUsrUid = "&CSCUSRUID=";
    private const string ParamFieldCscReqTim = "&CSCREQTIM=";
    private const string ParamFieldCscUsrNbr = "&CSCUSRNBR=";

    /// <summary>
    /// 构建签名信息
    /// </summary>
    /// <param name="appId">应用ID</param>
    /// <param name="companyId">企业ID</param>
    /// <param name="usrUid">用户号</param>
    /// <param name="usrNbr">平台用户号</param>
    /// <param name="apiPath">API路径</param>
    /// <param name="queryParams">查询参数</param>
    /// <returns>签名信息</returns>
    public static SignInfo BuildSignInfo(
        string appId,
        string companyId,
        string usrUid,
        string usrNbr,
        string apiPath,
        Dictionary<string, string>? queryParams = null)
    {
        long currentTimeMillis = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        string timestamp = (currentTimeMillis / 1000).ToString();

        // 构建查询参数
        var sb = new StringBuilder();
        sb.Append($"{ParamFieldCscAppUid}{appId}");
        sb.Append($"{ParamFieldCscPrjCod}{companyId}");
        sb.Append($"{ParamFieldCscUsrUid}{usrUid}");
        sb.Append($"{ParamFieldCscReqTim}{currentTimeMillis}");
        sb.Append($"{ParamFieldCscUsrNbr}{usrNbr}");

        if (queryParams != null)
        {
            foreach (var kvp in queryParams)
            {
                sb.Append($"&{kvp.Key}={kvp.Value}");
            }
        }

        string fullUrl;
        string path;    

        // 完整URL
        fullUrl = $"{apiPath}?{sb}";
        string subUrl = fullUrl.Replace("http://", "").Replace("https://", "");
        int pathIndex = subUrl.IndexOf("/");
        path = subUrl.Substring(pathIndex);

        return new SignInfo(timestamp, fullUrl, path);
    }

    /// <summary>
    /// 构建POST请求的签名字符串
    /// </summary>
    /// <param name="path">请求路径</param>
    /// <param name="requestBody">请求体</param>
    /// <param name="timestamp">时间戳</param>
    /// <returns>签名字符串</returns>
    public static string BuildPostSignString(string path, string requestBody, string timestamp)
    {
        return $"POST {path}\nx-alb-digest: {requestBody}\nx-alb-timestamp: {timestamp}";
    }

    /// <summary>
    /// 构建GET请求的签名字符串
    /// </summary>
    /// <param name="path">请求路径</param>
    /// <param name="timestamp">时间戳</param>
    /// <returns>签名字符串</returns>
    public static string BuildGetSignString(string path, string timestamp)
    {
        return $"GET {path}\nx-alb-timestamp: {timestamp}";
    }

    /// <summary>
    /// 计算SM3摘要
    /// </summary>
    /// <param name="data">待计算的数据</param>
    /// <returns>十六进制摘要值</returns>
    public static string Sm3Digest(string data)
    {
        byte[] srcByte = Encoding.UTF8.GetBytes(data);
        Org.BouncyCastle.Crypto.Digests.SM3Digest sm3Digest = new Org.BouncyCastle.Crypto.Digests.SM3Digest();
        sm3Digest.BlockUpdate(srcByte, 0, srcByte.Length);
        byte[] ret = new byte[sm3Digest.GetDigestSize()];
        sm3Digest.DoFinal(ret, 0);
        return Org.BouncyCastle.Utilities.Encoders.Hex.ToHexString(ret);
    }
}
