using Ss.CmbXft.Common.Models.Validation;

namespace Ss.CmbXft.Application.Services;

/// <summary>
/// 验证服务接口
/// </summary>
public interface IValidationService
{
    /// <summary>
    /// 验证对象
    /// </summary>
    /// <typeparam name="T">要验证的类型</typeparam>
    /// <param name="instance">要验证的实例</param>
    /// <param name="cancellationToken">取消令牌</param>
    Task ValidateAsync<T>(T instance, CancellationToken cancellationToken = default) where T : class;

    /// <summary>
    /// 验证对象并返回验证错误
    /// </summary>
    /// <typeparam name="T">要验证的类型</typeparam>
    /// <param name="instance">要验证的实例</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>验证错误列表，如果没有错误则返回空列表</returns>
    Task<List<ValidationErrorDetail>> ValidateAndGetErrorsAsync<T>(T instance, CancellationToken cancellationToken = default) where T : class;

    /// <summary>
    /// 验证对象并返回是否有效
    /// </summary>
    /// <typeparam name="T">要验证的类型</typeparam>
    /// <param name="instance">要验证的实例</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>如果验证通过返回true，否则返回false</returns>
    Task<bool> IsValidAsync<T>(T instance, CancellationToken cancellationToken = default) where T : class;
}