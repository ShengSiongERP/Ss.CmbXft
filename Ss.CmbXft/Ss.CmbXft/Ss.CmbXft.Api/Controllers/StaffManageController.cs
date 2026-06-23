using Microsoft.AspNetCore.Mvc;
using Ss.CmbXft.Api.Infrastructure.Common;
using Ss.CmbXft.Sdk.Exceptions;
using Ss.CmbXft.Sdk.Models;
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
    public async Task<ActionResult<ApiResponse<CreateStaffResponseBody>>> CreateStaff(
        [FromBody] CreateStaffRequest request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("收到创建企业员工请求 - 人数: {Count}", request.Count);

            var result = await _staffService.CreateStaffAsync(request, cancellationToken);

            return Ok(result);
        }
        catch (XftBusinessException ex)
        {
            _logger.LogError(ex, "薪福通业务异常");
            return BadRequest(new { error = ex.ErrorCode ?? "BIZ_ERROR", message = ex.ErrorMessage ?? ex.Message });
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, "参数异常");
            return BadRequest(new { error = "INVALID_ARGUMENT", message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "未知异常");
            return StatusCode(500, new { error = "INTERNAL_ERROR", message = "服务器内部错误" });
        }
    }

    /// <summary>
    /// 删除企业员工
    /// </summary>
    /// <param name="request">删除员工请求</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>删除结果</returns>
    [HttpPost("delete")]
    public async Task<ActionResult<ApiResponse<DeleteStaffResponseBody>>> DeleteStaff(
        [FromBody] DeleteStaffRequest request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("收到删除企业员工请求 - 人数: {Count}", request.StfSeqList.Count);

            var result = await _staffService.DeleteStaffAsync(request, cancellationToken);

            return Ok(result);
        }
        catch (XftBusinessException ex)
        {
            _logger.LogError(ex, "薪福通业务异常");
            return BadRequest(new { error = ex.ErrorCode ?? "BIZ_ERROR", message = ex.ErrorMessage ?? ex.Message });
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, "参数异常");
            return BadRequest(new { error = "INVALID_ARGUMENT", message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "未知异常");
            return StatusCode(500, new { error = "INTERNAL_ERROR", message = "服务器内部错误" });
        }
    }

    /// <summary>
    /// 查询员工数据字典
    /// </summary>
    /// <param name="request">数据字典查询请求</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>数据字典查询结果</returns>
    [HttpPost("data-dictionary")]
    public async Task<ActionResult<ApiResponse<DataDictionaryQueryResponseBody>>> QueryDataDictionary(
        [FromBody] DataDictionaryQueryRequest request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("收到查询员工数据字典请求 - 查询类型数: {Count}", request.Count);

            var result = await _staffService.QueryDataDictionaryAsync(request, cancellationToken);

            return Ok(result);
        }
        catch (XftBusinessException ex)
        {
            _logger.LogError(ex, "薪福通业务异常");
            return BadRequest(new { error = ex.ErrorCode ?? "BIZ_ERROR", message = ex.ErrorMessage ?? ex.Message });
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, "参数异常");
            return BadRequest(new { error = "INVALID_ARGUMENT", message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "未知异常");
            return StatusCode(500, new { error = "INTERNAL_ERROR", message = "服务器内部错误" });
        }
    }
}
