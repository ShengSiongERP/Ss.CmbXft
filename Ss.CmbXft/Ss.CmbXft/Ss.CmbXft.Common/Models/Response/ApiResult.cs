using System.Text.Json.Serialization;

namespace Ss.CmbXft.Common.Models;

/// <summary>
/// 统一返回体
/// </summary>
public interface IApiResult
{
    /// <summary>
    /// 响应状态码
    /// </summary>
    string Code { get; }

    /// <summary>
    /// 响应消息
    /// </summary>
    string Msg { get; }

    /// <summary>
    /// 响应时间戳（毫秒）
    /// </summary>
    long Timestamp { get; }

    /// <summary>
    /// 请求追踪ID
    /// </summary>
    string? TraceId { get; }
}

/// <inheritdoc />
public class ApiResult : IApiResult
{
    /// <summary>
    /// 响应状态码
    /// </summary>
    [JsonPropertyName("code")]
    public string Code { get; protected set; } = ApiResultEnum.Success.Code();

    /// <summary>
    /// 响应消息
    /// </summary>
    [JsonPropertyName("message")]
    public string Msg { get; protected set; } = ApiResultEnum.Success.Msg();

    /// <summary>
    /// 响应时间戳（毫秒）
    /// </summary>
    [JsonPropertyName("timestamp")]
    public long Timestamp { get; protected set; }

    /// <summary>
    /// 请求追踪ID
    /// </summary>
    [JsonPropertyName("traceId")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? TraceId { get; protected set; }

    /// <summary>
    /// 扩展数据（用于返回额外信息）
    /// </summary>
    [JsonPropertyName("extensions")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Dictionary<string, object?>? Extensions { get; protected set; }

    /// <summary>
    /// 私有构造函数
    /// </summary>
    protected ApiResult()
    {
        Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
    }

    /// <summary>
    /// 判断是否成功
    /// </summary>
    [JsonIgnore]
    public bool IsSuccess => Code == ApiResultEnum.Success.Code();

    /// <summary>
    /// 创建成功响应
    /// </summary>
    public static ApiResult Success(string? message = null, string? traceId = null)
    {
        return new ApiResult
        {
            Code = ApiResultEnum.Success.Code(),
            Msg = message ?? ApiResultEnum.Success.Msg(),
            TraceId = traceId,
        };
    }

    /// <summary>
    /// 创建失败响应
    /// </summary>
    public static ApiResult Error(ApiResultEnum code, string? message = null, string? traceId = null)
    {
        return new ApiResult
        {
            Code = code.Code(),
            Msg = message ?? code.Msg(),
            TraceId = traceId,
        };
    }

    /// <summary>
    /// 创建失败响应（自定义状态码）
    /// </summary>
    public static ApiResult Error(string code, string message, string? traceId = null)
    {
        return new ApiResult
        {
            Code = code,
            Msg = message,
            TraceId = traceId,
        };
    }

    /// <summary>
    /// 添加扩展数据
    /// </summary>
    /// <example>
    /// ApiResult.Success().WithExtension("serverTime", DateTime.UtcNow).WithExtension("version", "v1.0.0")
    /// </example>
    public ApiResult WithExtension(string key, object? value)
    {
        Extensions ??= new Dictionary<string, object?>();
        Extensions[key] = value;
        return this;
    }

    /// <summary>
    /// 设置追踪ID
    /// </summary>
    public ApiResult WithTraceId(string traceId)
    {
        TraceId = traceId;
        return this;
    }
}

/// <inheritdoc />
public class ApiResult<T> : ApiResult
{
    /// <summary>
    /// 响应数据
    /// </summary>
    [JsonPropertyName("data")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public T? Data { get; protected set; }

    /// <summary>
    /// 创建成功响应
    /// </summary>
    public static ApiResult<T> Success(T? data = default, string? message = null, string? traceId = null)
    {
        return new ApiResult<T>
        {
            Code = ApiResultEnum.Success.Code(),
            Msg = message ?? ApiResultEnum.Success.Msg(),
            Data = data,
            TraceId = traceId,
        };
    }

    /// <summary>
    /// 创建失败响应
    /// </summary>
    public static ApiResult<T> Error(ApiResultEnum code, string? message = null, T? data = default, string? traceId = null)
    {
        return new ApiResult<T>
        {
            Code = code.Code(),
            Msg = message ?? code.Msg(),
            Data = data,
            TraceId = traceId,
        };
    }

    /// <summary>
    /// 创建失败响应（自定义状态码）
    /// </summary>
    public static ApiResult<T> Fail(string code, string message, T? data = default, string? traceId = null)
    {
        return new ApiResult<T>
        {
            Code = code,
            Msg = message,
            Data = data,
            TraceId = traceId,
        };
    }
}
