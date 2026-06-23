using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ss.CmbXft.Sdk.Models;
using Ss.CmbXft.Sdk.Models.Staff;

namespace Ss.CmbXft.Sdk.Services;

/// <summary>
/// 员工服务接口
/// </summary>
public interface IStaffService
{
    /// <summary>
    /// 分页查询员工信息
    /// </summary>
    /// <param name="request">查询请求</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>员工信息查询响应</returns>
    Task<ApiResponse<StaffQueryResponseBody>> QueryStaffAsync(
        StaffQueryRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 创建企业员工
    /// </summary>
    /// <param name="request">创建员工请求</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>创建员工响应</returns>
    Task<ApiResponse<CreateStaffResponseBody>> CreateStaffAsync(
        CreateStaffRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 删除企业员工
    /// </summary>
    /// <param name="request">删除员工请求</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>删除员工响应</returns>
    Task<ApiResponse<DeleteStaffResponseBody>> DeleteStaffAsync(
        DeleteStaffRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 查询员工数据字典
    /// </summary>
    /// <param name="request">数据字典查询请求</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>数据字典查询响应</returns>
    Task<ApiResponse<DataDictionaryQueryResponseBody>> QueryDataDictionaryAsync(
        DataDictionaryQueryRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 编辑企业员工
    /// </summary>
    /// <param name="request">编辑员工请求</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>编辑员工响应</returns>
    Task<ApiResponse<EditStaffResponseBody>> EditStaffAsync(
        EditStaffRequest request,
        CancellationToken cancellationToken = default);
}
