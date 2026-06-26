using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ss.CmbXft.Api.Infrastructure.Common;
using Ss.CmbXft.Application.Dtos;
using Ss.CmbXft.Application.Services;
using Ss.CmbXft.Common.Models;

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
    public async Task<ApiResult<PageResult<OrgOrgDto>>> GetOrganizationList([FromBody] GetOrganizationListRequestDto input)
    {
        var result = await _organizationAppService.GetOrganizationListAsync(input);
        return ApiResult<PageResult<OrgOrgDto>>.Success(result);
    }
}
