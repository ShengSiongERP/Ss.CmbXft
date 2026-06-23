using Microsoft.AspNetCore.Mvc;
using Ss.CmbXft.Api.Infrastructure.Common;
using Ss.CmbXft.Application.Dtos;
using Ss.CmbXft.Application.Dtos.Xft;
using Ss.CmbXft.Application.Services;
using Ss.CmbXft.Application.Validators;
using Ss.CmbXft.Common.Models;

namespace Ss.CmbXft.Api.Controllers;

/// <summary>
/// 员工管理
/// </summary>
[Route("api/xft/staff")]
public class XftStaffController : ApiControllerBase
{
    private readonly IXftStaffAppService _xftStaffAppService;
    private readonly IValidationService _validationService;

    public XftStaffController(
        IXftStaffAppService xftStaffAppService,
        IValidationService validationService,
        ILogger<XftStaffController> logger) : base(logger)
    {
        _xftStaffAppService = xftStaffAppService;
        _validationService = validationService;
    }

    /// <summary>
    /// 分页查询员工列表
    /// </summary>
    [HttpPost("page")]
    public async Task<ApiResult<PageResult<XftStaffDto>>> Page([FromBody] XftStaffQueryDto query)
    {
        await _validationService.ValidateAsync(query);
        var result = await _xftStaffAppService.GetPageAsync(query);
        return ApiResult<PageResult<XftStaffDto>>.Success(result);
    }

    /// <summary>
    /// 获取所有员工（下拉框等场景使用）
    /// </summary>
    [HttpGet("list")]
    public async Task<ApiResult<List<XftStaffDto>>> List()
    {
        var result = await _xftStaffAppService.GetListAsync();
        return ApiResult<List<XftStaffDto>>.Success(result);
    }

    /// <summary>
    /// 根据ID获取员工详情
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ApiResult<XftStaffDto>> Get(long id)
    {
        var result = await _xftStaffAppService.GetAsync(id);
        if (result == null)
        {
            return ApiResult<XftStaffDto>.Error(ApiResultEnum.ValidateError, "员工不存在");
        }
        return ApiResult<XftStaffDto>.Success(result);
    }

    /// <summary>
    /// 创建员工
    /// </summary>
    [HttpPost]
    public async Task<ApiResult<XftStaffDto>> Create([FromBody] XftStaffSaveDto dto)
    {
        await _validationService.ValidateAsync(dto);
        var result = await _xftStaffAppService.CreateAsync(dto);
        return ApiResult<XftStaffDto>.Success(result);
    }

    /// <summary>
    /// 更新员工
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ApiResult<XftStaffDto>> Update(long id, [FromBody] XftStaffSaveDto dto)
    {
        await _validationService.ValidateAsync(dto);
        var result = await _xftStaffAppService.UpdateAsync(id, dto);
        return ApiResult<XftStaffDto>.Success(result);
    }

    /// <summary>
    /// 删除员工（软删除）
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ApiResult> Delete(long id)
    {
        await _xftStaffAppService.DeleteAsync(id);
        return ApiResult.Success();
    }

    /// <summary>
    /// 批量删除员工（软删除）
    /// </summary>
    [HttpDelete("batch")]
    public async Task<ApiResult> DeleteBatchAsync([FromBody] List<long> ids)
    {
        if (ids == null || ids.Count == 0)
        {
            return ApiResult.Error(ApiResultEnum.ValidateError, "请选择要删除的员工");
        }
        if (ids.Count > 100)
        {
            return ApiResult.Error(ApiResultEnum.ValidateError, "一次最多删除100个员工");
        }
        await _xftStaffAppService.DeleteBatchAsync(ids);
        return ApiResult.Success();
    }
}
