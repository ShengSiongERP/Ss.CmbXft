using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Ss.CmbXft.Common.Exceptions;
using Ss.CmbXft.Common.Models.Validation;

namespace Ss.CmbXft.Application.Services;

/// <summary>
/// 验证服务实现
/// </summary>
public class ValidationService : IValidationService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<ValidationService> _logger;

    public ValidationService(IServiceProvider serviceProvider, ILogger<ValidationService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task ValidateAsync<T>(T instance, CancellationToken cancellationToken = default) where T : class
    {
        var errors = await ValidateAndGetErrorsAsync(instance, cancellationToken);

        if (errors.Count > 0)
        {
            throw new Ss.CmbXft.Common.Exceptions.ValidationException("请求参数验证失败", errors);
        }
    }

    /// <inheritdoc />
    public async Task<List<ValidationErrorDetail>> ValidateAndGetErrorsAsync<T>(T instance, CancellationToken cancellationToken = default) where T : class
    {
        var errors = new List<ValidationErrorDetail>();

        if (instance == null)
        {
            errors.Add(new ValidationErrorDetail
            {
                PropertyName = "Request",
                ErrorMessage = "请求对象不能为null"
            });
            return errors;
        }

        try
        {
            var validator = _serviceProvider.GetService<IValidator<T>>();
            if (validator != null)
            {
                var validationResult = await validator.ValidateAsync(instance, cancellationToken);

                if (!validationResult.IsValid)
                {
                    foreach (var error in validationResult.Errors)
                    {
                        errors.Add(new ValidationErrorDetail
                        {
                            PropertyName = error.PropertyName,
                            ErrorMessage = error.ErrorMessage,
                            AttemptedValue = error.AttemptedValue,
                            ErrorCode = error.ErrorCode
                        });
                    }

                    _logger.LogWarning("验证失败: {Type} - {Errors}", typeof(T).Name, 
                        string.Join("; ", errors.Select(e => $"{e.PropertyName}: {e.ErrorMessage}")));
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "验证过程中发生异常: {Type}", typeof(T).Name);
            errors.Add(new ValidationErrorDetail
            {
                PropertyName = "Validation",
                ErrorMessage = "验证过程中发生异常"
            });
        }

        return errors;
    }

    /// <inheritdoc />
    public async Task<bool> IsValidAsync<T>(T instance, CancellationToken cancellationToken = default) where T : class
    {
        var errors = await ValidateAndGetErrorsAsync(instance, cancellationToken);
        return errors.Count == 0;
    }
}