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
using Ss.CmbXft.Sdk.Models.Post;

namespace Ss.CmbXft.Sdk.Services;

/// <summary>
/// 岗位服务实现
/// </summary>
public class PostService : IPostService
{
    private readonly IXftClient _client;
    private readonly ILogger<PostService> _logger;
    private readonly XftOptions _options;

    /// <summary>
    /// 初始化 <see cref="PostService"/> 类的新实例
    /// </summary>
    /// <param name="client">XFT客户端</param>
    /// <param name="options">配置选项</param>
    /// <param name="logger">日志记录器</param>
    public PostService(IXftClient client, XftOptions options, ILogger<PostService>? logger = null)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
        _options = options ?? throw new ArgumentNullException(nameof(options));
        _logger = logger ?? NullLogger<PostService>.Instance;
    }

    /// <summary>
    /// 获取岗位查询API路径
    /// </summary>
    private string GetPostQueryApiPath()
    {
        return XftApiUrls.Post.QueryPage.Path(_options.Environment);
    }

    /// <summary>
    /// 分页查询岗位
    /// </summary>
    public async Task<ApiResponse<PostQueryResponseBody>> QueryPostAsync(
        PostQueryRequest request,
        CancellationToken cancellationToken = default)
    {
        // 验证请求参数
        var validationErrors = new List<string>();
        if (!request.Validate(out validationErrors))
        {
            throw new ArgumentException($"请求参数验证失败: {string.Join(", ", validationErrors)}");
        }

        _logger.LogDebug("开始分页查询岗位: Page={Page}, PageSize={PageSize}",
            request.CurrentPage, request.PageSize);

        try
        {
            var result = await _client.PostEncryptedWithDecryptionAsync(
                GetPostQueryApiPath(),
                request,
                null,
                cancellationToken);

            var response = JsonConvert.DeserializeObject<ApiResponse<PostQueryResponseBody>>(result);
            if (response == null)
            {
                return new ApiResponse<PostQueryResponseBody> { ReturnCode = "PARSE_ERROR", ErrorMsg = "响应解析失败" };
            }

            _logger.LogInformation("成功查询岗位列表: TotalSize={TotalSize}, ReturnCode={ReturnCode}",
                response.Body?.TotalSize ?? 0, response.ReturnCode);

            return response;
        }
        catch (XftBusinessException ex)
        {
            _logger.LogError(ex, "查询岗位列表业务异常: {Message}", ex.Message);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询岗位列表异常");
            throw new XftBusinessException($"查询岗位列表失败: {ex.Message}", ex);
        }
    }
}
