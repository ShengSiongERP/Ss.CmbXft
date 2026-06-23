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
using Ss.CmbXft.Sdk.Models.Organization;

namespace Ss.CmbXft.Sdk.Services;

/// <summary>
/// 组织服务实现
/// </summary>
public class OrganizationService : IOrganizationService
{
    private readonly IXftClient _client;
    private readonly ILogger<OrganizationService> _logger;
    private readonly XftOptions _options;

    /// <summary>
    /// 初始化 <see cref="OrganizationService"/> 类的新实例
    /// </summary>
    /// <param name="client">XFT客户端</param>
    /// <param name="options">配置选项</param>
    /// <param name="logger">日志记录器</param>
    public OrganizationService(IXftClient client, XftOptions options, ILogger<OrganizationService>? logger = null)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
        _options = options ?? throw new ArgumentNullException(nameof(options));
        _logger = logger ?? NullLogger<OrganizationService>.Instance;
    }

    /// <summary>
    /// 获取组织列表API路径
    /// </summary>
    private string GetOrganizationListApiPath()
    {
        return XftApiUrls.Organization.GetOrganizationList.Path(_options.Environment);
    }

    /// <summary>
    /// 分页获取组织列表
    /// </summary>
    public async Task<ApiResponse<OrganizationListResponseBody>> GetOrganizationListAsync(
        OrganizationListRequest request,
        CancellationToken cancellationToken = default)
    {
        // 验证请求参数
        var validationErrors = new List<string>();
        if (!request.Validate(out validationErrors))
        {
            throw new ArgumentException($"请求参数验证失败: {string.Join(", ", validationErrors)}");
        }

        _logger.LogDebug("开始分页获取组织列表: Page={Page}, PageSize={PageSize}",
            request.CurrentPage, request.PageSize);

        try
        {
            var result = await _client.PostEncryptedWithDecryptionAsync(
                GetOrganizationListApiPath(),
                request,
                null,
                cancellationToken);

            var response = JsonConvert.DeserializeObject<ApiResponse<OrganizationListResponseBody>>(result);
            if (response == null)
            {
                return new ApiResponse<OrganizationListResponseBody> { ReturnCode = "PARSE_ERROR", ErrorMsg = "响应解析失败" };
            }

            _logger.LogInformation("成功获取组织列表: TotalSize={TotalSize}, ReturnCode={ReturnCode}",
                response.Body?.TotalSize ?? 0, response.ReturnCode);

            return response;
        }
        catch (XftBusinessException ex)
        {
            _logger.LogError(ex, "获取组织列表业务异常: {Message}", ex.Message);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "获取组织列表异常");
            throw new XftBusinessException($"获取组织列表失败: {ex.Message}", ex);
        }
    }
}
