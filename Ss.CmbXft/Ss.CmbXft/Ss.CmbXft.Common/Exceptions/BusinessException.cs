using Ss.CmbXft.Common.Models;

namespace Ss.CmbXft.Common.Exceptions;

/// <summary>
/// 业务异常
/// </summary>
public sealed class BusinessException : Exception
{
    public string Code { get; } = ApiResultEnum.Fail.Code();

    public BusinessException(string message, Exception? innerException = null)
        : base(message, innerException) { }

    public BusinessException(string code, string message, Exception? innerException = null)
        : base(message, innerException)
    {
        Code = code;
    }

    public BusinessException(ApiResultEnum resultEnum, string? message = null, Exception? innerException = null)
        : base(message ?? resultEnum.Msg(), innerException)
    {
        Code = resultEnum.Code();
    }
}
