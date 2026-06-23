using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Ss.CmbXft.Common.Models;

namespace Ss.CmbXft.Api.Infrastructure.Middleware;

/// <summary>
/// 全局拦截：参数为空、dto=null、模型验证失败
/// </summary>
public class GlobalValidationFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            // 收集所有详尽错误
            var errorList = new List<object>();

            foreach (var kvp in context.ModelState)
            {
                if (kvp.Value?.Errors == null || !kvp.Value.Errors.Any())
                    continue;

                var field = string.IsNullOrEmpty(kvp.Key) ? "Request" : kvp.Key;

                foreach (var error in kvp.Value.Errors)
                {
                    var msg = string.IsNullOrWhiteSpace(error.ErrorMessage) ? "参数验证失败" : error.ErrorMessage;
                    errorList.Add(new { field, message = msg });
                }
            }

            // 提示信息
            string firstMsg = errorList.Any() ? (errorList.First() as dynamic)!.message : "参数格式不正确";

            // TraceId
            string? traceId = context.HttpContext.TraceIdentifier;

            var result = ApiResult.Error(ApiResultEnum.ValidateError, firstMsg, traceId).WithExtension("errors", errorList);

            context.Result = new JsonResult(result)
            {
                StatusCode = 200
            };
        }
    }

    public void OnActionExecuted(ActionExecutedContext context) { }
}