using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ss.CmbXft.Api.Infrastructure.Common;
using Ss.CmbXft.Application.Services;
using Ss.CmbXft.Sdk.Models.Events;
using System.Text.Json;

namespace Ss.CmbXft.Api.Controllers;

/// <summary>
/// 薪福通事件推送控制器
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class XftEventController : ApiControllerBase
{
    private readonly IXftEventService _xftEventService;

    public XftEventController(
        IXftEventService xftEventService,
        ILogger<XftEventController> logger) : base(logger)
    {
        _xftEventService = xftEventService;
    }

    /// <summary>
    /// 接收薪福通事件推送
    /// </summary>
    /// <param name="request">事件推送请求</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>薪福通事件响应</returns>
    [HttpPost]
    public async Task<ActionResult<XftEventResponse>> ReceiveEvent(
        [FromBody] JsonElement request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("收到薪福通事件推送请求");

            var requestJson = request.GetRawText();
            _logger.LogDebug("事件原始数据: {RequestJson}", requestJson);

            var eventRequest = JsonSerializer.Deserialize<XftEventPushRequest>(requestJson);
            if (eventRequest == null)
            {
                _logger.LogError("事件推送请求解析失败");
                return Ok(XftEventResponse.Fail("请求解析失败"));
            }

            _logger.LogInformation("处理事件 - EventId: {EventId}, BusinessKey: {BusinessKey}",
                eventRequest!.EventId, eventRequest.BusinessKey);

            var processResult = await _xftEventService.ProcessEventAsync(eventRequest);

            if (processResult)
            {
                _logger.LogInformation("事件处理成功 - EventId: {EventId}", eventRequest.EventId);
                return Ok(XftEventResponse.Success());
            }
            else
            {
                _logger.LogWarning("事件处理失败 - EventId: {EventId}", eventRequest.EventId);
                return Ok(XftEventResponse.Fail("事件处理失败"));
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "处理薪福通事件推送时发生异常");
            return Ok(XftEventResponse.Fail($"处理异常: {ex.Message}"));
        }
    }

    /// <summary>
    /// 健康检查端点（用于薪福通连接测试）
    /// </summary>
    /// <returns>薪福通事件响应</returns>
    [HttpGet]
    [HttpHead]
    public ActionResult<XftEventResponse> HealthCheck()
    {
        _logger.LogInformation("收到薪福通连接测试请求");
        return Ok(XftEventResponse.Success());
    }
}
