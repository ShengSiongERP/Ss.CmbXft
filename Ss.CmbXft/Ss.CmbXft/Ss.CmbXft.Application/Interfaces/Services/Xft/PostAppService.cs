using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Ss.CmbXft.Application.Dtos;
using Ss.CmbXft.Common.Models;
using Ss.CmbXft.Sdk.Models.Post;
using Ss.CmbXft.Sdk.Services;

namespace Ss.CmbXft.Application.Services;

/// <summary>
/// 岗位应用服务实现
/// </summary>
public class PostAppService : IPostAppService
{
    private readonly IPostService _postService;
    private readonly ILogger<PostAppService> _logger;

    /// <summary>
    /// 初始化 <see cref="PostAppService"/> 类的新实例
    /// </summary>
    /// <param name="postService">岗位服务</param>
    /// <param name="logger">日志记录器</param>
    public PostAppService(
        IPostService postService,
        ILogger<PostAppService> logger)
    {
        _postService = postService ?? throw new ArgumentNullException(nameof(postService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// 分页查询岗位
    /// </summary>
    public async Task<PageResult<PostDto>> GetPostListAsync(GetPostListRequestDto input)
    {
        if (input == null)
        {
            throw new ArgumentNullException(nameof(input));
        }

        _logger.LogInformation("开始查询岗位列表: Page={Page}, PageSize={PageSize}",
            input.CurrentPage, input.PageSize);

        try
        {
            // 构建SDK请求
            var request = new PostQueryRequest
            {
                SequenceNumbers = input.SequenceNumbers,
                PositionName = input.PositionName,
                OrganizationIds = input.OrganizationIds,
                CodeNumber = input.CodeNumber,
                CurrentPage = input.CurrentPage,
                PageSize = input.PageSize
            };

            // 调用SDK服务
            var response = await _postService.QueryPostAsync(request);

            // 检查响应
            if (response.ReturnCode != "SUC0000")
            {
                _logger.LogError("查询岗位列表失败: ReturnCode={ReturnCode}, ErrorMsg={ErrorMsg}",
                    response.ReturnCode, response.ErrorMsg);
                throw new Exception($"查询岗位列表失败: {response.ErrorMsg ?? response.ReturnCode}");
            }

            if (response.Body == null)
            {
                _logger.LogWarning("查询岗位列表响应Body为空");
                return new PageResult<PostDto>(0, (int)input.CurrentPage, (int)input.PageSize, new List<PostDto>());
            }

            // 转换为DTO
            var postDtos = response.Body.Records?.Select(r => new PostDto
            {
                SequenceNumber = r.SequenceNumber,
                CodeNumber = r.CodeNumber,
                PositionName = r.PositionName,
                Organizations = r.Organizations?.Select(o => new PostOrganizationDto
                {
                    OrganizationId = o.OrganizationId,
                    OrganizationName = o.OrganizationName,
                    OrganizationNamePath = o.OrganizationNamePath
                }).ToList(),
                Remark = r.Remark,
                OrderNumber = r.OrderNumber
            }).ToList() ?? new List<PostDto>();

            _logger.LogInformation("成功查询岗位列表: TotalSize={TotalSize}, Count={Count}",
                response.Body.TotalSize, postDtos.Count);

            return new PageResult<PostDto>(
                response.Body.TotalSize ?? 0,
                (int)input.CurrentPage,
                (int)input.PageSize,
                postDtos
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询岗位列表异常");
            throw;
        }
    }
}
