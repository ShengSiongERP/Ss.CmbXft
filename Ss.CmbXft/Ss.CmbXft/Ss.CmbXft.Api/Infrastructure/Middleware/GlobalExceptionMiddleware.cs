using Microsoft.EntityFrameworkCore;
using Ss.CmbXft.Common.Exceptions;
using Ss.CmbXft.Common.Models;
using Ss.CmbXft.Sdk.Exceptions;
using System.Net;
using System.Text.Json;

namespace Ss.CmbXft.Api.Infrastructure.Middleware;

/// <summary>
/// 全局异常处理中间件
/// </summary>
public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;
    private readonly IHostEnvironment _env;

    public GlobalExceptionMiddleware(
        RequestDelegate next,
        ILogger<GlobalExceptionMiddleware> logger,
        IHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var traceId = context.TraceIdentifier;
        var path = context.Request.Path + context.Request.QueryString;
        var method = context.Request.Method;

        // 默认响应
        var statusCode = (int)HttpStatusCode.InternalServerError;
        ApiResultEnum code = ApiResultEnum.ServerError;
        var message = "服务器繁忙，请稍后重试";
        string strType = string.Empty;
        string logType = "LogWarning";
        Dictionary<string, object?>? extensions = null;

        switch (ex)
        {
            case BusinessException bizEx://业务异常
                statusCode = StatusCodes.Status200OK;
                code = bizEx.Code.ToApiResultEnum();
                message = bizEx.Message;
                strType = "【业务异常】";
                break;

            case ArgumentNullException argnEx://参数空异常
                statusCode = StatusCodes.Status200OK;
                code = ApiResultEnum.ValidateError;
                message = argnEx.Message ?? ApiResultEnum.ValidateError.GetMsg();
                strType = "【参数空异常】";
                break;
            case ArgumentException argEx://参数异常
                statusCode = StatusCodes.Status200OK;
                code = ApiResultEnum.ValidateError;
                message = argEx.Message ?? ApiResultEnum.ValidateError.GetMsg();
                strType = "【参数异常】";
                break;

            case ValidationException valEx://验证异常
                statusCode = StatusCodes.Status200OK;
                code = ApiResultEnum.ValidateError;
                message = valEx.Message ?? ApiResultEnum.ValidateError.GetMsg();
                strType = "【验证异常】";
                if (valEx.Errors.Count > 0)
                {
                    extensions = new Dictionary<string, object?>
                    {
                        ["errors"] = valEx.Errors,
                        //["errors1"] = string.Join("; ", valEx.Errors.Select(e => $"{e.PropertyName}: {e.ErrorMessage}"))
                    };
                }
                break;
            case UnauthorizedAccessException://未授权
                statusCode = StatusCodes.Status200OK;
                code = ApiResultEnum.Unauthorized;
                message = ex.Message ?? ApiResultEnum.Unauthorized.GetMsg();
                strType = "【未授权】";
                break;
            case XftBusinessException xftEx://薪福通业务异常
                statusCode = StatusCodes.Status200OK;
                code = ApiResultEnum.BusinessError;
                message = xftEx.ErrorMessage ?? xftEx.Message;
                strType = "【薪福通业务异常】";
                break;
            case DbUpdateException dbEx://EF Core 数据库异常
                statusCode = StatusCodes.Status200OK;
                code = ApiResultEnum.DbError;
                message = "数据操作异常，请检查数据后重试";
                extensions = new Dictionary<string, object?>
                {
                    ["message"] = $"{dbEx.InnerException?.Message ?? dbEx.Message}"
                };
                strType = "【数据库异常】";
                logType = "LogError";
                break;
            default:
                code = ApiResultEnum.Fail;
                strType = "【系统异常】";
                logType = "LogError";
                break;
        }
        if (logType == "LogWarning")
            _logger.LogWarning(ex, "{strType} TraceId:{TraceId} {Method} {Path} {ErrorType}",
                strType, traceId, path, method, ex.GetType().FullName);
        else if (logType == "LogError")
            _logger.LogError(ex, "{strType} TraceId:{TraceId} | Path:{Path} | Method:{Method}",
            strType, traceId, path, method);
        // 构建统一响应
        var apiResult = ApiResult.Error(code, message, traceId);
        if (extensions != null)
        {
            foreach (var kv in extensions)
            {
                apiResult.WithExtension(kv.Key, kv.Value);
            }
        }

        // 4. 开发环境返回堆栈
        if (_env.IsDevelopment())
        {
            apiResult.WithExtension("devError", $"{ex.GetType().FullName}: {ex.Message}");
            apiResult.WithExtension("devStackTrace", ex.StackTrace);
        }
        // 返回 JSON
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        var json = JsonSerializer.Serialize(apiResult, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
            //WriteIndented = _env.IsDevelopment()
        });

        await context.Response.WriteAsync(json);
    }
}

/// <summary>
/// 全局异常中间件扩展方法
/// </summary>
public static class GlobalExceptionMiddlewareExtensions
{
    /// <summary>
    /// 使用全局异常处理中间件（建议放在管道最前面）
    /// </summary>
    public static IApplicationBuilder UseGlobalExceptionMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<GlobalExceptionMiddleware>();
    }
}