using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ss.CmbXft.Api.Infrastructure.Common;
using Ss.CmbXft.Application.Dtos;
using Ss.CmbXft.Application.Services;

namespace Ss.CmbXft.Api.Controllers;

/// <summary>
/// 岗位管理控制器
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class XftPostApiController : ApiControllerBase
{
    private readonly IPostAppService _postAppService;

    public XftPostApiController(
        IPostAppService postAppService,
        ILogger<XftPostApiController> logger) : base(logger)
    {
        _postAppService = postAppService;
    }

    /// <summary>
    /// 分页查询岗位列表
    /// </summary>
    /// <param name="input">查询请求</param>
    /// <returns>岗位列表分页结果</returns>
    [HttpPost("list")]
    public async Task<IActionResult> GetPostList([FromBody] GetPostListRequestDto input)
    {
        try
        {
            var result = await _postAppService.GetPostListAsync(input);
            return Success(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询岗位列表失败");
            return Failure($"查询岗位列表失败: {ex.Message}");
        }
    }
}
