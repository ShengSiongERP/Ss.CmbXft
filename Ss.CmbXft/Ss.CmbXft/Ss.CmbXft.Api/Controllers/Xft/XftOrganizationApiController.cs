using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ss.CmbXft.Api.Infrastructure.Common;
using Ss.CmbXft.Application.Dtos;
using Ss.CmbXft.Application.Services;

namespace Ss.CmbXft.Api.Controllers;

/// <summary>
/// 组织管理控制器
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class XftOrganizationApiController : ApiControllerBase
{
    private readonly IOrganizationAppService _organizationAppService;

    public XftOrganizationApiController(
        IOrganizationAppService organizationAppService,
        ILogger<XftOrganizationApiController> logger) : base(logger)
    {
        _organizationAppService = organizationAppService;
    }

    /// <summary>
    /// 分页获取组织列表
    /// </summary>
    /// <param name="input">查询请求</param>
    /// <returns>组织列表分页结果</returns>
    [HttpPost("list")]
    public async Task<IActionResult> GetOrganizationList([FromBody] GetOrganizationListRequestDto input)
    {
        try
        {
            var result = await _organizationAppService.GetOrganizationListAsync(input);
            return Success(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "获取组织列表失败");
            return Failure($"获取组织列表失败: {ex.Message}");
        }
    }
}
