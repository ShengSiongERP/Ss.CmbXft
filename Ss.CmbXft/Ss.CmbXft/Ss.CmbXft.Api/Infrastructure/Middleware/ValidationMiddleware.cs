using System.Text.Json;
using Ss.CmbXft.Common.Exceptions;
using Ss.CmbXft.Common.Models;

namespace Ss.CmbXft.Api.Infrastructure.Middleware;

/// <summary>
/// 验证中间件
/// </summary>
public class ValidationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ValidationMiddleware> _logger;

    public ValidationMiddleware(RequestDelegate next, ILogger<ValidationMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException ex)
        {
            await HandleValidationExceptionAsync(context, ex);
        }
    }

    private async Task HandleValidationExceptionAsync(HttpContext context, ValidationException ex)
    {
        _logger.LogWarning("验证失败: {Errors}",
            string.Join("; ", ex.Errors.Select(e => $"{e.PropertyName}: {e.ErrorMessage}")));

        var firstMsg = ex.Errors.Count > 0
            ? $"{ex.Errors[0].PropertyName}: {ex.Errors[0].ErrorMessage}"
            : ex.Message;

        var apiResult = ApiResult.Error(ApiResultEnum.ValidateError, firstMsg, context.TraceIdentifier);
        if (ex.Errors.Count > 0)
        {
            apiResult.WithExtension("errors", ex.Errors);
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = StatusCodes.Status200OK;

        var json = JsonSerializer.Serialize(apiResult, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        });

        await context.Response.WriteAsync(json);
    }
}

/// <summary>
/// 验证中间件扩展方法
/// </summary>
public static class ValidationMiddlewareExtensions
{
    /// <summary>
    /// 使用验证中间件
    /// </summary>
    public static IApplicationBuilder UseValidationMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ValidationMiddleware>();
    }
}
