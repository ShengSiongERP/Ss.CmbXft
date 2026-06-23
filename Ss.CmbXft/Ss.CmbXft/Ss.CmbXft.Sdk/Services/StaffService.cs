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
using Ss.CmbXft.Sdk.Models.Staff;

namespace Ss.CmbXft.Sdk.Services;

/// <summary>
/// 员工服务实现
/// </summary>
public class StaffService : IStaffService
{
    private readonly IXftClient _client;
    private readonly ILogger<StaffService> _logger;
    private readonly XftOptions _options;

    /// <summary>
    /// 初始化 <see cref="StaffService"/> 类的新实例
    /// </summary>
    /// <param name="client">XFT客户端</param>
    /// <param name="options">配置选项</param>
    /// <param name="logger">日志记录器</param>
    public StaffService(IXftClient client, XftOptions options, ILogger<StaffService>? logger = null)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
        _options = options ?? throw new ArgumentNullException(nameof(options));
        _logger = logger ?? NullLogger<StaffService>.Instance;
    }

    /// <summary>
    /// 获取员工查询API路径
    /// </summary>
    private string GetStaffQueryApiPath()
    {
        return XftApiUrls.Staff.Query.Path(_options.Environment);
    }

    /// <summary>
    /// 获取创建员工API路径
    /// </summary>
    private string GetCreateStaffApiPath()
    {
        return XftApiUrls.Staff.Create.Path(_options.Environment);
    }

    /// <summary>
    /// 获取删除员工API路径
    /// </summary>
    private string GetDeleteStaffApiPath()
    {
        return XftApiUrls.Staff.Delete.Path(_options.Environment);
    }

    /// <summary>
    /// 获取数据字典查询API路径
    /// </summary>
    private string GetDataDictionaryApiPath()
    {
        return XftApiUrls.Staff.DataDictionary.Path(_options.Environment);
    }

    /// <summary>
    /// 获取编辑员工API路径
    /// </summary>
    private string GetEditStaffApiPath()
    {
        return XftApiUrls.Staff.Update.Path(_options.Environment);
    }

    /// <summary>
    /// 分页查询员工信息
    /// </summary>
    public async Task<ApiResponse<StaffQueryResponseBody>> QueryStaffAsync(
        StaffQueryRequest request,
        CancellationToken cancellationToken = default)
    {
        // 验证请求参数
        var validationErrors = new List<string>();
        if (!request.Validate(out validationErrors))
        {
            throw new ArgumentException($"请求参数验证失败: {string.Join(", ", validationErrors)}");
        }

        _logger.LogDebug("开始分页查询员工信息: Page={Page}, PageSize={PageSize}",
            request.CurrentPage, request.PageSize);

        try
        {
            var result = await _client.PostEncryptedWithDecryptionAsync(
                GetStaffQueryApiPath(),
                request,
                null,
                cancellationToken);

            var response = JsonConvert.DeserializeObject<ApiResponse<StaffQueryResponseBody>>(result);
            if (response == null)
            {
                return new ApiResponse<StaffQueryResponseBody> { ReturnCode = "PARSE_ERROR", ErrorMsg = "响应解析失败" };
            }

            if (!response.IsSuccess)
            {
                _logger.LogWarning("分页查询员工信息失败: {Code} - {Message}", response.ReturnCode, response.ErrorMsg);
            }
            else
            {
                _logger.LogDebug("分页查询员工信息成功: 当前页={CurrentPage}, 每页={PageSize}, 总数={TotalSize}",
                    response.Body?.CurrentPage,
                    response.Body?.PageSize,
                    response.Body?.TotalSize);
            }

            return response;
        }

        catch (Exception ex)
        {
            _logger.LogError(ex, "分页查询员工信息时发生异常");
            throw;
        }
    }

    /// <summary>
    /// 创建企业员工
    /// </summary>
    public async Task<ApiResponse<CreateStaffResponseBody>> CreateStaffAsync(
        CreateStaffRequest request,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("开始创建企业员工，共 {Count} 人", request.Count);

        try
        {
            var result = await _client.PostEncryptedWithDecryptionAsync(
                GetCreateStaffApiPath(),
                request,
                null,
                cancellationToken);

            var response = JsonConvert.DeserializeObject<ApiResponse<CreateStaffResponseBody>>(result);
            if (response == null)
            {
                return new ApiResponse<CreateStaffResponseBody> { ReturnCode = "PARSE_ERROR", ErrorMsg = "响应解析失败" };
            }

            if (!response.IsSuccess)
            {
                _logger.LogWarning("创建企业员工失败: {Code} - {Message}", response.ReturnCode, response.ErrorMsg);
            }
            else
            {
                _logger.LogInformation("创建企业员工请求成功，响应数量: {Count}", response.Body?.Count ?? 0);
            }

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "创建企业员工时发生异常");
            throw;
        }
    }

    /// <summary>
    /// 删除企业员工
    /// </summary>
    public async Task<ApiResponse<DeleteStaffResponseBody>> DeleteStaffAsync(
        DeleteStaffRequest request,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("开始删除企业员工，共 {Count} 人", request.StfSeqList.Count);

        try
        {
            var result = await _client.PostEncryptedWithDecryptionAsync(
                GetDeleteStaffApiPath(),
                request,
                null,
                cancellationToken);

            var response = JsonConvert.DeserializeObject<ApiResponse<DeleteStaffResponseBody>>(result);
            if (response == null)
            {
                return new ApiResponse<DeleteStaffResponseBody> { ReturnCode = "PARSE_ERROR", ErrorMsg = "响应解析失败" };
            }

            if (!response.IsSuccess)
            {
                _logger.LogWarning("删除企业员工失败: {Code} - {Message}", response.ReturnCode, response.ErrorMsg);
            }
            else
            {
                _logger.LogInformation("删除企业员工请求成功，响应数量: {Count}", response.Body?.Count ?? 0);
            }

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "删除企业员工时发生异常");
            throw;
        }
    }

    /// <summary>
    /// 查询员工数据字典
    /// </summary>
    public async Task<ApiResponse<DataDictionaryQueryResponseBody>> QueryDataDictionaryAsync(
        DataDictionaryQueryRequest request,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("开始查询员工数据字典，查询类型数: {Count}", request.Count);

        try
        {
            var result = await _client.PostEncryptedWithDecryptionAsync(
                GetDataDictionaryApiPath(),
                request,
                null,
                cancellationToken);

            var response = JsonConvert.DeserializeObject<ApiResponse<DataDictionaryQueryResponseBody>>(result);
            if (response == null)
            {
                return new ApiResponse<DataDictionaryQueryResponseBody> { ReturnCode = "PARSE_ERROR", ErrorMsg = "响应解析失败" };
            }

            if (!response.IsSuccess)
            {
                _logger.LogWarning("查询员工数据字典失败: {Code} - {Message}", response.ReturnCode, response.ErrorMsg);
            }
            else
            {
                _logger.LogInformation("查询员工数据字典成功，结果类型数: {Count}", response.Body?.Count ?? 0);
            }

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询员工数据字典时发生异常");
            throw;
        }
    }

    /// <summary>
    /// 编辑企业员工
    /// </summary>
    public async Task<ApiResponse<EditStaffResponseBody>> EditStaffAsync(
        EditStaffRequest request,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("开始编辑企业员工，共 {Count} 人", request.Count);

        try
        {
            var result = await _client.PostEncryptedWithDecryptionAsync(
                GetEditStaffApiPath(),
                request,
                null,
                cancellationToken);

            var response = JsonConvert.DeserializeObject<ApiResponse<EditStaffResponseBody>>(result);
            if (response == null)
            {
                return new ApiResponse<EditStaffResponseBody> { ReturnCode = "PARSE_ERROR", ErrorMsg = "响应解析失败" };
            }

            if (!response.IsSuccess)
            {
                _logger.LogWarning("编辑企业员工失败: {Code} - {Message}", response.ReturnCode, response.ErrorMsg);
            }
            else
            {
                _logger.LogInformation("编辑企业员工请求成功，响应数量: {Count}", response.Body?.Count ?? 0);
            }

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "编辑企业员工时发生异常");
            throw;
        }
    }
}
