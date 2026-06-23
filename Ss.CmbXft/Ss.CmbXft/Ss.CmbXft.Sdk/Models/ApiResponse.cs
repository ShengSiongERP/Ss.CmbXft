using Newtonsoft.Json;

namespace Ss.CmbXft.Sdk.Models;

/// <summary>
/// API响应基类
/// </summary>
/// <typeparam name="T">响应数据类型</typeparam>
public class ApiResponse<T>
{
    /// <summary>
    /// 返回业务码，SUC0000表示接口正常返回
    /// </summary>
    [JsonProperty("returnCode")]
    public string ReturnCode { get; set; } = string.Empty;

    /// <summary>
    /// 错误信息，null表示无错误信息
    /// </summary>
    [JsonProperty("errorMsg")]
    public string? ErrorMsg { get; set; }

    /// <summary>
    /// 响应数据
    /// </summary>
    [JsonProperty("body")]
    public T? Body { get; set; }

    /// <summary>
    /// 错误详情信息
    /// </summary>
    public object? ErrorInfo { get; set; }

    /// <summary>
    /// 判断请求是否成功
    /// </summary>
    public bool IsSuccess => ReturnCode == "SUC0000";

    /// <summary>
    /// 获取错误信息（如果失败）
    /// </summary>
    /// <returns>错误信息字符串</returns>
    public string GetErrorMessage()
    {
        if (IsSuccess)
            return string.Empty;

        return ErrorMsg ?? $"未知错误 (ReturnCode: {ReturnCode})";
    }
}

/// <summary>
/// 无数据的API响应
/// </summary>
public class ApiResponse : ApiResponse<object>
{
}
