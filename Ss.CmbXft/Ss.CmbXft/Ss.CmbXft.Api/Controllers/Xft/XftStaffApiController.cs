using Microsoft.AspNetCore.Mvc;
using Ss.CmbXft.Api.Infrastructure.Common;
using Ss.CmbXft.Application.Services;
using Ss.CmbXft.Common.Models;
using Ss.CmbXft.Sdk.Models.Staff;
using Ss.CmbXft.Sdk.Services;

namespace Ss.CmbXft.Api.Controllers;

/// <summary>
/// 员工控制器
/// </summary>
public class XftStaffApiController : ApiControllerBase
{
    private readonly IStaffService _staffService;
    private readonly IXftErpSyncService _xftStaffSyncService;

    public XftStaffApiController(
        IStaffService staffService,
        IXftErpSyncService xftStaffSyncService,
        ILogger<XftStaffApiController> logger) : base(logger)
    {
        _staffService = staffService;
        _xftStaffSyncService = xftStaffSyncService;
    }

    #region 获取员工信息

    /// <summary>
    /// 分页查询员工信息
    /// </summary>
    /// <param name="request">查询请求</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>员工信息</returns>
    [HttpPost("api_list")]
    public async Task<ApiResult<StaffQueryResponseBody>> QueryStaffInfo([FromBody] StaffQueryRequest request,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("收到分页查询员工信息请求 - Page: {Page}, PageSize: {PageSize}",
            request.CurrentPage, request.PageSize);

        var result = await _staffService.QueryStaffAsync(request, cancellationToken);

        return ApiResult<StaffQueryResponseBody>.Success(result.Body);
    }

    #endregion

    #region 员工数据字典通用查询
    [HttpPost("api_dict")]
    public async Task<ApiResult<DataDictionaryQueryResponseBody>> QueryDataDictionary([FromBody] DataDictionaryQueryRequest request)
    {
        var result = await _staffService.QueryDataDictionaryAsync(request);

        return ApiResult<DataDictionaryQueryResponseBody>.Success(result.Body);
    }
    #endregion

    #region 数据库同步

    /// <summary>
    /// 从薪福通同步员工数据到数据库
    /// </summary>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>同步的员工数量</returns>
    [HttpPost("sync-to-both")]
    public async Task<ApiResult<int>> SyncToBothDatabases(
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("开始从薪福通同步员工数据到双数据库");

        var count = await _xftStaffSyncService.SyncFromXftAsync(cancellationToken);

        _logger.LogInformation("从薪福通同步员工数据到数据库完成，共同步 {Count} 条记录", count);

        return ApiResult<int>.Success(count, $"同步成功，共同步 {count} 条员工数据到数据库");
    }

    #endregion
}
