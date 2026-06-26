namespace Ss.CmbXft.Common.Models.Request;

/// <summary>
/// 基础请求接口
/// </summary>
public interface IBaseRequest
{
    /// <summary>
    /// 请求追踪ID
    /// </summary>
    string? TraceId { get; set; }

    /// <summary>
    /// 请求时间戳（毫秒）
    /// </summary>
    long? Timestamp { get; set; }

    /// <summary>
    /// 客户端IP地址
    /// </summary>
    string? ClientIp { get; set; }

    /// <summary>
    /// 用户代理
    /// </summary>
    string? UserAgent { get; set; }
}

/// <summary>
/// 基础请求抽象类
/// </summary>
public abstract class BaseRequest : IBaseRequest
{
    /// <summary>
    /// 请求追踪ID
    /// </summary>
    public string? TraceId { get; set; }

    /// <summary>
    /// 请求时间戳（毫秒）
    /// </summary>
    public long? Timestamp { get; set; }

    /// <summary>
    /// 客户端IP地址
    /// </summary>
    public string? ClientIp { get; set; }

    /// <summary>
    /// 用户代理
    /// </summary>
    public string? UserAgent { get; set; }
}
