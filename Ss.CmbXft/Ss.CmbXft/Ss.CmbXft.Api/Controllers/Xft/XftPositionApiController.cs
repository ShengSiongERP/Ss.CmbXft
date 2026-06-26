using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ss.CmbXft.Api.Infrastructure.Common;
using Ss.CmbXft.Application.Dtos;
using Ss.CmbXft.Application.Services;
using Ss.CmbXft.Common.Models;

namespace Ss.CmbXft.Api.Controllers;

/// <summary>
/// 职位管理控制器
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class XftPositionApiController : ApiControllerBase
{
    private readonly IXftPositionAppService _xftPositionAppService;

    public XftPositionApiController(
        IXftPositionAppService xftPositionAppService,
        ILogger<XftPositionApiController> logger) : base(logger)
    {
        _xftPositionAppService = xftPositionAppService;
    }

    /// <summary>
    /// 分页查询职位列表
    /// </summary>
    /// <param name="input">查询请求</param>
    /// <returns>职位列表分页结果</returns>
    [HttpPost("list")]
    public async Task<ApiResult<PageResult<PositionDto>>> GetPositionList([FromBody] GetPositionListRequestDto input)
    {
        var result = await _xftPositionAppService.GetPositionListAsync(input);
        return ApiResult<PageResult<PositionDto>>.Success(result);
    }
}
