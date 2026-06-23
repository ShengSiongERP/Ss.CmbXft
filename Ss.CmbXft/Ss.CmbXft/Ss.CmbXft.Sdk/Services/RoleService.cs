using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;
using Ss.CmbXft.Sdk.Client;
using Ss.CmbXft.Sdk.Configuration;
using Ss.CmbXft.Sdk.Exceptions;
using Ss.CmbXft.Sdk.Models;
using Ss.CmbXft.Sdk.Models.Role;

namespace Ss.CmbXft.Sdk.Services;

/// <summary>
/// 角色服务实现
/// </summary>
public class RoleService : IRoleService
{
    private readonly IXftClient _client;
    private readonly ILogger<RoleService> _logger;
    private readonly XftOptions _options;

    /// <summary>
    /// 初始化 <see cref="RoleService"/> 类的新实例
    /// </summary>
    /// <param name="client">XFT客户端</param>
    /// <param name="options">配置选项</param>
    /// <param name="logger">日志记录器</param>
    public RoleService(IXftClient client, XftOptions options, ILogger<RoleService>? logger = null)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
        _options = options ?? throw new ArgumentNullException(nameof(options));
        _logger = logger ?? NullLogger<RoleService>.Instance;
    }

    /// <summary>
    /// 获取角色列表API路径
    /// </summary>
    private string GetRoleListApiPath()
    {
        return XftApiUrls.Auth.RoleList.Path(_options.Environment);
    }

    /// <summary>
    /// 分页获取企业下所有角色
    /// </summary>
    public async Task<ApiResponse<RoleListResponseBody>> GetRoleListAsync(
        RoleListRequest request,
        CancellationToken cancellationToken = default)
    {
        // 验证请求参数
        var validationErrors = new List<string>();
        if (!request.Validate(out validationErrors))
        {
            throw new ArgumentException($"请求参数验证失败: {string.Join(", ", validationErrors)}");
        }

        _logger.LogDebug("开始分页获取企业下所有角色: Page={Page}, PageSize={PageSize}",
            request.CurrentPage, request.PageSize);

        try
        {
            var result = await _client.PostEncryptedWithDecryptionAsync(
                GetRoleListApiPath(),
                request,
                null,
                cancellationToken);

            var response = JsonConvert.DeserializeObject<ApiResponse<RoleListResponseBody>>(result);
            if (response == null)
            {
                return new ApiResponse<RoleListResponseBody> { ReturnCode = "PARSE_ERROR", ErrorMsg = "响应解析失败" };
            }

            _logger.LogInformation("成功获取企业角色列表: TotalSize={TotalSize}, ReturnCode={ReturnCode}",
                response.Body?.TotalSize ?? 0, response.ReturnCode);

            return response;
        }
        catch (XftBusinessException ex)
        {
            _logger.LogError(ex, "获取企业角色列表业务异常: {Message}", ex.Message);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "获取企业角色列表异常");
            throw new XftBusinessException($"获取企业角色列表失败: {ex.Message}", ex);
        }
    }
}
