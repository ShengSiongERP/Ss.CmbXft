using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Ss.CmbXft.Domain.ThirdParty.SsMember;
using Ss.CmbXft.Infrastructure.ThirdParty.SsMember;

namespace Ss.CmbXft.Infrastructure.ThirdParty.SsMember;

/// <summary>
/// 昇菘会员系统HTTP客户端接口
/// </summary>
public interface ISsMemberClient
{
    /// <summary>
    /// 发送POST请求
    /// </summary>
    /// <typeparam name="TRequest">请求类型</typeparam>
    /// <typeparam name="TResponse">响应类型</typeparam>
    /// <param name="apiPath">API路径</param>
    /// <param name="request">请求数据</param>
    /// <param name="ct">取消令牌</param>
    /// <returns>响应数据</returns>
    Task<TResponse> PostAsync<TRequest, TResponse>(string apiPath, TRequest request, CancellationToken ct = default)
        where TResponse : SsMemberResponse, new();

    /// <summary>
    /// 发送GET请求
    /// </summary>
    /// <typeparam name="TResponse">响应类型</typeparam>
    /// <param name="apiPath">API路径</param>
    /// <param name="queryParams">查询参数</param>
    /// <param name="ct">取消令牌</param>
    /// <returns>响应数据</returns>
    Task<TResponse> GetAsync<TResponse>(string apiPath, Dictionary<string, object>? queryParams = null, CancellationToken ct = default)
        where TResponse : SsMemberResponse, new();
}

/// <summary>
/// 昇菘会员系统HTTP客户端实现
/// </summary>
public class SsMemberClient : ISsMemberClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<SsMemberClient> _logger;
    private readonly SsMemberOptions _options;

    public SsMemberClient(
        HttpClient httpClient,
        ILogger<SsMemberClient> logger,
        IOptions<SsMemberOptions> options)
    {
        _httpClient = httpClient;
        _logger = logger;
        _options = options.Value;

        // 设置超时时间
        _httpClient.Timeout = TimeSpan.FromSeconds(_options.TimeoutSeconds);
    }

    public async Task<TResponse> PostAsync<TRequest, TResponse>(string apiPath, TRequest request, CancellationToken ct = default)
        where TResponse : SsMemberResponse, new()
    {
        var url = _options.GetApiUrl(apiPath);

        // 转换请求数据为字典以便签名
        var requestJson = JsonConvert.SerializeObject(request);
        var requestData = JsonConvert.DeserializeObject<Dictionary<string, object>>(requestJson)
            ?? new Dictionary<string, object>();

        // 生成签名
        var current = SsMemberSignUtils.GetCurrentTimestamp();
        requestData["current"] = current;
        requestData["sign"] = SsMemberSignUtils.BuildSign(_options.AppKey, requestData, current);

        // 序列化最终请求数据
        var finalRequestJson = JsonConvert.SerializeObject(requestData);

        if (_options.EnableLogging)
        {
            _logger.LogInformation("SsMember API Request: {Url}, Body: {Body}", url, finalRequestJson);
        }

        using var httpRequest = new HttpRequestMessage(HttpMethod.Post, url);
        httpRequest.Content = new StringContent(finalRequestJson, Encoding.UTF8, "application/json");
        httpRequest.Headers.Add("Accept", "application/json");

        var response = await _httpClient.SendAsync(httpRequest, ct);
        var responseContent = await response.Content.ReadAsStringAsync(ct);

        if (_options.EnableLogging)
        {
            _logger.LogInformation("SsMember API Response: {StatusCode}, Body: {Body}",
                response.StatusCode, responseContent);
        }

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError("SsMember API Failed: {StatusCode}, {Content}",
                response.StatusCode, responseContent);
            throw new HttpRequestException(
                $"SsMember API Error: {response.StatusCode}, Content: {responseContent}");
        }

        var result = JsonConvert.DeserializeObject<TResponse>(responseContent)
            ?? new TResponse { Code = (int)response.StatusCode, Message = "Failed to parse response" };

        return result;
    }

    public async Task<TResponse> GetAsync<TResponse>(string apiPath, Dictionary<string, object>? queryParams = null, CancellationToken ct = default)
        where TResponse : SsMemberResponse, new()
    {
        var url = _options.GetApiUrl(apiPath);
        var current = SsMemberSignUtils.GetCurrentTimestamp();

        // 构建查询参数
        var allParams = new Dictionary<string, object>(queryParams ?? new Dictionary<string, object>());
        allParams["current"] = current;
        allParams["sign"] = SsMemberSignUtils.BuildSign(_options.AppKey, allParams, current);

        // 构建查询字符串
        var queryString = string.Join("&", allParams
            .OrderBy(kvp => kvp.Key, StringComparer.Ordinal)
            .Select(kvp => $"{Uri.EscapeDataString(kvp.Key)}={Uri.EscapeDataString(kvp.Value?.ToString() ?? "")}"));

        var fullUrl = $"{url}?{queryString}";

        if (_options.EnableLogging)
        {
            _logger.LogInformation("SsMember API Request: {Url}", fullUrl);
        }

        using var httpRequest = new HttpRequestMessage(HttpMethod.Get, fullUrl);
        httpRequest.Headers.Add("Accept", "application/json");

        var response = await _httpClient.SendAsync(httpRequest, ct);
        var responseContent = await response.Content.ReadAsStringAsync(ct);

        if (_options.EnableLogging)
        {
            _logger.LogInformation("SsMember API Response: {StatusCode}, Body: {Body}",
                response.StatusCode, responseContent);
        }

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError("SsMember API Failed: {StatusCode}, {Content}",
                response.StatusCode, responseContent);
            throw new HttpRequestException(
                $"SsMember API Error: {response.StatusCode}, Content: {responseContent}");
        }

        var result = JsonConvert.DeserializeObject<TResponse>(responseContent)
            ?? new TResponse { Code = (int)response.StatusCode, Message = "Failed to parse response" };

        return result;
    }
}
