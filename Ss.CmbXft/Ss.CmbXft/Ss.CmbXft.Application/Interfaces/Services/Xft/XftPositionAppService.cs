using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Ss.CmbXft.Application.Dtos;
using Ss.CmbXft.Common.Models;
using Ss.CmbXft.Sdk.Models.Position;
using Ss.CmbXft.Sdk.Services;

namespace Ss.CmbXft.Application.Services;

/// <summary>
/// 职位应用服务实现（薪福通SDK服务）
/// </summary>
public class XftPositionAppService : IXftPositionAppService
{
    private readonly IPositionService _positionService;
    private readonly ILogger<XftPositionAppService> _logger;

    /// <summary>
    /// 初始化 <see cref="XftPositionAppService"/> 类的新实例
    /// </summary>
    /// <param name="positionService">职位服务</param>
    /// <param name="logger">日志记录器</param>
    public XftPositionAppService(
        IPositionService positionService,
        ILogger<XftPositionAppService> logger)
    {
        _positionService = positionService ?? throw new ArgumentNullException(nameof(positionService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// 分页查询职位
    /// </summary>
    public async Task<PageResult<PositionDto>> GetPositionListAsync(GetPositionListRequestDto input)
    {
        if (input == null)
        {
            throw new ArgumentNullException(nameof(input));
        }

        _logger.LogInformation("开始查询职位列表: Page={Page}, PageSize={PageSize}",
            input.CurrentPage, input.PageSize);

        try
        {
            // 构建SDK请求
            var request = new PositionQueryRequest
            {
                SequenceNumbers = input.SequenceNumbers,
                JobName = input.JobName,
                CodeNumber = input.CodeNumber,
                CurrentPage = input.CurrentPage,
                PageSize = input.PageSize
            };

            // 调用SDK服务
            var response = await _positionService.QueryPositionAsync(request);

            // 检查响应
            if (response.ReturnCode != "SUC0000")
            {
                _logger.LogError("查询职位列表失败: ReturnCode={ReturnCode}, ErrorMsg={ErrorMsg}",
                    response.ReturnCode, response.ErrorMsg);
                throw new Exception($"查询职位列表失败: {response.ErrorMsg ?? response.ReturnCode}");
            }

            if (response.Body == null)
            {
                _logger.LogWarning("查询职位列表响应Body为空");
                return new PageResult<PositionDto>(0, (int)input.CurrentPage, (int)input.PageSize, new List<PositionDto>());
            }

            // 转换为DTO
            var positionDtos = response.Body.Records?.Select(r => new PositionDto
            {
                SequenceNumber = r.SequenceNumber,
                CodeNumber = r.CodeNumber,
                JobName = r.JobName,
                Remark = r.Remark,
                OrderNumber = r.OrderNumber
            }).ToList() ?? new List<PositionDto>();

            _logger.LogInformation("成功查询职位列表: TotalSize={TotalSize}, Count={Count}",
                response.Body.TotalSize, positionDtos.Count);

            return new PageResult<PositionDto>(
                response.Body.TotalSize ?? 0,
                (int)input.CurrentPage,
                (int)input.PageSize,
                positionDtos
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询职位列表异常");
            throw;
        }
    }
}