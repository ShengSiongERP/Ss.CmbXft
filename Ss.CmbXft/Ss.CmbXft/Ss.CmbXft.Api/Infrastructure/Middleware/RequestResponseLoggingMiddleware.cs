using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Ss.CmbXft.Api.Infrastructure.Middleware;

/// <summary>
/// 请求/响应日志记录
/// </summary>
public class RequestResponseLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestResponseLoggingMiddleware> _logger;

    public RequestResponseLoggingMiddleware(RequestDelegate next, ILogger<RequestResponseLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Log Request
        context.Request.EnableBuffering();
        var requestBody = await ReadRequestBody(context.Request);
        _logger.LogInformation("HTTP Request: {Method} {Path} {QueryString} Body: {Body}", 
            context.Request.Method, context.Request.Path, context.Request.QueryString, requestBody);

        // Copy original response stream
        var originalBodyStream = context.Response.Body;
        using var responseBody = new MemoryStream();
        context.Response.Body = responseBody;

        try
        {
            await _next(context);

            // Log Response
            var responseBodyText = await ReadResponseBody(context.Response);
            _logger.LogInformation("HTTP Response: {StatusCode} Body: {Body}", 
                context.Response.StatusCode, responseBodyText);

            // Copy back to original stream
            await responseBody.CopyToAsync(originalBodyStream);
        }
        finally
        {
            context.Response.Body = originalBodyStream;
        }
    }

    private async Task<string> ReadRequestBody(HttpRequest request)
    {
        request.Body.Position = 0;
        using var reader = new StreamReader(request.Body, Encoding.UTF8, true, 1024, true);
        var body = await reader.ReadToEndAsync();
        request.Body.Position = 0;
        return body;
    }

    private async Task<string> ReadResponseBody(HttpResponse response)
    {
        response.Body.Seek(0, SeekOrigin.Begin);
        var body = await new StreamReader(response.Body).ReadToEndAsync();
        response.Body.Seek(0, SeekOrigin.Begin);
        return body;
    }
}
