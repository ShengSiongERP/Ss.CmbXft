using System;

namespace Ss.CmbXft.Sdk.Exceptions;

/// <summary>
/// 薪福通业务异常
/// </summary>
public class XftBusinessException : Exception
{
    /// <summary>
    /// 错误码
    /// </summary>
    public string? ErrorCode { get; set; }

    /// <summary>
    /// 错误信息
    /// </summary>
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// 原始响应内容
    /// </summary>
    public string? RawResponse { get; set; }

    /// <summary>
    /// 初始化 <see cref="XftBusinessException"/> 类的新实例
    /// </summary>
    public XftBusinessException()
    {
    }

    /// <summary>
    /// 初始化 <see cref="XftBusinessException"/> 类的新实例
    /// </summary>
    /// <param name="message">错误消息</param>
    public XftBusinessException(string message) : base(message)
    {
    }

    /// <summary>
    /// 初始化 <see cref="XftBusinessException"/> 类的新实例
    /// </summary>
    /// <param name="message">错误消息</param>
    /// <param name="innerException">内部异常</param>
    public XftBusinessException(string message, Exception innerException) : base(message, innerException)
    {
    }

    /// <summary>
    /// 初始化 <see cref="XftBusinessException"/> 类的新实例
    /// </summary>
    /// <param name="errorCode">错误码</param>
    /// <param name="errorMessage">错误信息</param>
    /// <param name="rawResponse">原始响应内容</param>
    public XftBusinessException(string errorCode, string errorMessage, string? rawResponse = null)
        : base($"[{errorCode}] {errorMessage}")
    {
        ErrorCode = errorCode;
        ErrorMessage = errorMessage;
        RawResponse = rawResponse;
    }

    /// <summary>
    /// 返回异常的字符串表示形式
    /// </summary>
    /// <returns>异常的字符串表示</returns>
    public override string ToString()
    {
        if (string.IsNullOrEmpty(ErrorCode))
            return base.ToString();

        return $"[{ErrorCode}] {ErrorMessage}{(RawResponse != null ? $"\nRawResponse: {RawResponse}" : "")}";
    }
}