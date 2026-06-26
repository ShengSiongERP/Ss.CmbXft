using Microsoft.AspNetCore.Mvc;
using Ss.CmbXft.Api.Infrastructure.Common;
using Ss.CmbXft.Common.Models;
using Ss.CmbXft.Sdk.Models.Staff;
using Ss.CmbXft.Sdk.Services;

namespace Ss.CmbXft.Api.Controllers;

/// <summary>
/// 员工管理控制器
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class StaffManageController : ApiControllerBase
{
    private readonly IStaffService _staffService;

    public StaffManageController(
        IStaffService staffService,
        ILogger<StaffManageController> logger) : base(logger)
    {
        _staffService = staffService;
    }

    /// <summary>
    /// 创建企业员工
    /// </summary>
    /// <param name="request">创建员工请求</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>创建结果</returns>
    [HttpPost("create")]
    public async Task<ApiResult<CreateStaffResponseBody>> CreateStaff(
        [FromBody] CreateStaffRequest request,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("收到创建企业员工请求 - 人数: {Count}", request.Count);

        var result = await _staffService.CreateStaffAsync(request, cancellationToken);

        return ApiResult<CreateStaffResponseBody>.Success(result.Body);
    }

    /// <summary>
    /// 删除企业员工
    /// </summary>
    /// <param name="request">删除员工请求</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>删除结果</returns>
    [HttpPost("delete")]
    public async Task<ApiResult<DeleteStaffResponseBody>> DeleteStaff(
        [FromBody] DeleteStaffRequest request,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("收到删除企业员工请求 - 人数: {Count}", request.StfSeqList.Count);

        var result = await _staffService.DeleteStaffAsync(request, cancellationToken);

        return ApiResult<DeleteStaffResponseBody>.Success(result.Body);
    }

    /// <summary>
    /// 查询员工数据字典
    /// </summary>
    /// <param name="request">数据字典查询请求</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>数据字典查询结果</returns>
    [HttpPost("data-dictionary")]
    public async Task<ApiResult<DataDictionaryQueryResponseBody>> QueryDataDictionary(
        [FromBody] DataDictionaryQueryRequest request,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("收到查询员工数据字典请求 - 查询类型数: {Count}", request.Count);

        var result = await _staffService.QueryDataDictionaryAsync(request, cancellationToken);

        return ApiResult<DataDictionaryQueryResponseBody>.Success(result.Body);
    }
}
