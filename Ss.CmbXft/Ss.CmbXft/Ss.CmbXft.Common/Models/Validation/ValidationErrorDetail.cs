namespace Ss.CmbXft.Common.Models.Validation;

/// <summary>
/// 验证错误详情
/// </summary>
public sealed class ValidationErrorDetail
{
    /// <summary>
    /// 字段名称
    /// </summary>
    public string PropertyName { get; set; } = string.Empty;

    /// <summary>
    /// 错误消息
    /// </summary>
    public string ErrorMessage { get; set; } = string.Empty;

    /// <summary>
    /// 尝试的值
    /// </summary>
    public object? AttemptedValue { get; set; }

    /// <summary>
    /// 错误代码
    /// </summary>
    public string? ErrorCode { get; set; }
}
