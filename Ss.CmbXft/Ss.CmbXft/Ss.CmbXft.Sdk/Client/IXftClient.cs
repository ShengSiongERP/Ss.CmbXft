using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ss.CmbXft.Sdk.Models;

namespace Ss.CmbXft.Sdk.Client;

/// <summary>
/// 薪福通API客户端接口
/// </summary>
public interface IXftClient : IDisposable
{
    /// <summary>
    /// 发送POST请求
    /// </summary>
    /// <typeparam name="TRequest">请求类型</typeparam>
    /// <typeparam name="TResponse">响应类型</typeparam>
    /// <param name="apiPath">API路径</param>
    /// <param name="request">请求数据</param>
    /// <param name="queryParams">查询参数</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>API响应</returns>
    Task<ApiResponse<TResponse>> PostAsync<TRequest, TResponse>(
        string apiPath,
        TRequest request,
        Dictionary<string, string>? queryParams = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 发送GET请求
    /// </summary>
    /// <typeparam name="TResponse">响应类型</typeparam>
    /// <param name="apiPath">API路径</param>
    /// <param name="queryParams">查询参数</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>API响应</returns>
    Task<ApiResponse<TResponse>> GetAsync<TResponse>(
        string apiPath,
        Dictionary<string, string>? queryParams = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 发送加密POST请求（SM4ECB加密）
    /// </summary>
    /// <typeparam name="TRequest">请求类型</typeparam>
    /// <typeparam name="TResponse">响应类型</typeparam>
    /// <param name="apiPath">API路径</param>
    /// <param name="request">请求数据</param>
    /// <param name="queryParams">查询参数</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>API响应</returns>
    Task<ApiResponse<TResponse>> PostEncryptedAsync<TRequest, TResponse>(
        string apiPath,
        TRequest request,
        Dictionary<string, string>? queryParams = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 发送加密POST请求（SM4ECB加密，返回解密后的响应）
    /// </summary>
    /// <typeparam name="TRequest">请求类型</typeparam>
    /// <param name="apiPath">API路径</param>
    /// <param name="request">请求数据</param>
    /// <param name="queryParams">查询参数</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>解密后的响应字符串</returns>
    Task<string> PostEncryptedWithDecryptionAsync<TRequest>(
        string apiPath,
        TRequest request,
        Dictionary<string, string>? queryParams = null,
        CancellationToken cancellationToken = default);
}
