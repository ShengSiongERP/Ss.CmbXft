using Ss.CmbXft.Common.Models.Validation;

namespace Ss.CmbXft.Common.Exceptions;

/// <summary>
/// 验证异常
/// </summary>
public sealed class ValidationException : Exception
{
    /// <summary>
    /// 验证错误列表
    /// </summary>
    public List<ValidationErrorDetail> Errors { get; } = [];

    /// <summary>
    /// 构造函数
    /// </summary>
    public ValidationException(string? message = null, List<ValidationErrorDetail>? errors = null, Exception? innerException = null)
        : base(message ?? "请求参数验证失败", innerException)
    {
        Errors = errors ?? [];
    }

    /// <summary>
    /// 从错误列表构造
    /// </summary>
    public ValidationException(List<ValidationErrorDetail> errors)
        : base("请求参数验证失败")
    {
        Errors = errors;
    }

    /// <summary>
    /// 从单条错误构造
    /// </summary>
    public ValidationException(ValidationErrorDetail error)
        : base("请求参数验证失败")
    {
        Errors = [error];
    }
}
