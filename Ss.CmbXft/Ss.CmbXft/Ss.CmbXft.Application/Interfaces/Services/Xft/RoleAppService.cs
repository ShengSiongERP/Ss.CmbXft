using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Ss.CmbXft.Application.Dtos;
using Ss.CmbXft.Common.Models;
using Ss.CmbXft.Sdk.Models;
using Ss.CmbXft.Sdk.Models.Role;
using Ss.CmbXft.Sdk.Services;

namespace Ss.CmbXft.Application.Services;

/// <summary>
/// 角色应用服务实现
/// </summary>
public class RoleAppService : IRoleAppService
{
    private readonly IRoleService _roleService;
    private readonly ILogger<RoleAppService> _logger;

    /// <summary>
    /// 初始化 <see cref="RoleAppService"/> 类的新实例
    /// </summary>
    /// <param name="roleService">角色服务</param>
    /// <param name="logger">日志记录器</param>
    public RoleAppService(
        IRoleService roleService,
        ILogger<RoleAppService> logger)
    {
        _roleService = roleService ?? throw new ArgumentNullException(nameof(roleService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// 分页获取企业下所有角色
    /// </summary>
    /// <param name="input">查询请求</param>
    /// <returns>角色列表分页结果</returns>
    public async Task<PageResult<RoleDto>> GetRoleListAsync(GetRoleListRequestDto input)
    {
        if (input == null)
        {
            throw new ArgumentNullException(nameof(input));
        }

        _logger.LogInformation("开始获取企业角色列表: Page={Page}, PageSize={PageSize}",
            input.CurrentPage, input.PageSize);

        try
        {
            // 构建SDK请求
            var request = new RoleListRequest
            {
                CurrentPage = input.CurrentPage,
                PageSize = input.PageSize
            };

            // 调用SDK服务
            var response = await _roleService.GetRoleListAsync(request);

            // 检查响应
            if (response.ReturnCode != "SUC0000")
            {
                _logger.LogError("获取企业角色列表失败: ReturnCode={ReturnCode}, ErrorMsg={ErrorMsg}",
                    response.ReturnCode, response.ErrorMsg);
                throw new Exception($"获取企业角色列表失败: {response.ErrorMsg ?? response.ReturnCode}");
            }

            if (response.Body == null)
            {
                _logger.LogWarning("获取企业角色列表响应Body为空");
                return new PageResult<RoleDto>(0, (int)input.CurrentPage, (int)input.PageSize, new List<RoleDto>());
            }

            // 转换为DTO
            var roleDtos = response.Body.Records?.Select(r => new RoleDto
            {
                Code = r.Code,
                Name = r.Name
            }).ToList() ?? new List<RoleDto>();

            _logger.LogInformation("成功获取企业角色列表: TotalSize={TotalSize}, Count={Count}",
                response.Body.TotalSize, roleDtos.Count);

            return new PageResult<RoleDto>(
                response.Body.TotalSize ?? 0,
                (int)input.CurrentPage,
                (int)input.PageSize,
                roleDtos
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "获取企业角色列表异常");
            throw;
        }
    }
}
