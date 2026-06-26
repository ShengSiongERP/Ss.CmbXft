using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ss.CmbXft.Application.Dtos;
using Ss.CmbXft.Application.Services;
using Ss.CmbXft.Api.Infrastructure.Common;
using Ss.CmbXft.Common.Models;

namespace Ss.CmbXft.Api.Controllers;

/// <summary>
/// 角色管理控制器
/// </summary>
public class XftRoleApiController : ApiControllerBase
{
    private readonly IRoleAppService _roleAppService;

    public XftRoleApiController(IRoleAppService roleAppService, ILogger<XftRoleApiController> logger) : base(logger)
    {
        _roleAppService = roleAppService;
    }

    /// <summary>
    /// 分页获取企业下所有角色
    /// </summary>
    /// <param name="input">查询请求</param>
    /// <returns>角色列表分页结果</returns>
    [HttpPost("list")]
    public async Task<ApiResult<PageResult<RoleDto>>> GetRoleList([FromBody] GetRoleListRequestDto input)
    {
        var result = await _roleAppService.GetRoleListAsync(input);
        return ApiResult<PageResult<RoleDto>>.Success(result);
    }
}
