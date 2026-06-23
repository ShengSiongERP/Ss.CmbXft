using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Ss.CmbXft.Infrastructure.ThirdParty.SsMember;

/// <summary>
/// 昇菘会员系统签名工具类
/// </summary>
public static class SsMemberSignUtils
{
    /// <summary>
    /// 构建请求签名
    /// </summary>
    /// <param name="appKey">应用密钥</param>
    /// <param name="requestData">请求数据（不含 current 和 sign）</param>
    /// <param name="current">当前时间戳（10位）</param>
    /// <returns>签名（32位小写MD5）</returns>
    public static string BuildSign(string appKey, object requestData, long current)
    {
        // 将请求数据序列化为字典
        var dataDict = JsonSerializer.Serialize(requestData);
        var dict = JsonSerializer.Deserialize<Dictionary<string, object>>(dataDict);

        if (dict == null)
        {
            throw new ArgumentException("无法序列化请求数据");
        }

        return BuildSign(appKey, dict, current);
    }

    /// <summary>
    /// 构建请求签名（字典版本）
    /// </summary>
    /// <param name="appKey">应用密钥</param>
    /// <param name="requestData">请求数据字典（不含 current 和 sign）</param>
    /// <param name="current">当前时间戳（10位）</param>
    /// <returns>签名（32位小写MD5）</returns>
    public static string BuildSign(string appKey, Dictionary<string, object> requestData, long current)
    {
        // 排除 current 和 sign 字段，并按字典序排序
        var sortedKeys = requestData.Keys
            .Where(k => k != "current" && k != "sign")
            .OrderBy(k => k, StringComparer.Ordinal)
            .ToList();

        // 拼接字符串：key_a + key_b + ... + key_n + appKey + current
        var signString = string.Join("", sortedKeys) + appKey + current;

        // 计算MD5
        return ComputeMd5Hash(signString);
    }

    /// <summary>
    /// 计算MD5哈希值
    /// </summary>
    /// <param name="input">输入字符串</param>
    /// <returns>32位小写MD5哈希值</returns>
    public static string ComputeMd5Hash(string input)
    {
        using var md5 = MD5.Create();
        var inputBytes = Encoding.UTF8.GetBytes(input);
        var hashBytes = md5.ComputeHash(inputBytes);

        // 转换为32位小写十六进制字符串
        var sb = new StringBuilder();
        foreach (var b in hashBytes)
        {
            sb.Append(b.ToString("x2"));
        }

        return sb.ToString();
    }

    /// <summary>
    /// 获取当前10位时间戳
    /// </summary>
    /// <returns>10位Unix时间戳</returns>
    public static long GetCurrentTimestamp()
    {
        return DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    }

    /// <summary>
    /// 验证签名
    /// </summary>
    /// <param name="appKey">应用密钥</param>
    /// <param name="requestData">请求数据</param>
    /// <param name="receivedSign">接收到的签名</param>
    /// <returns>签名是否有效</returns>
    public static bool VerifySign(string appKey, object requestData, string receivedSign)
    {
        var dataDict = JsonSerializer.Serialize(requestData);
        var dict = JsonSerializer.Deserialize<Dictionary<string, object>>(dataDict);

        if (dict == null || !dict.TryGetValue("current", out var currentObj))
        {
            return false;
        }

        var current = Convert.ToInt64(currentObj);
        var calculatedSign = BuildSign(appKey, dict, current);

        return calculatedSign.Equals(receivedSign, StringComparison.OrdinalIgnoreCase);
    }
}
