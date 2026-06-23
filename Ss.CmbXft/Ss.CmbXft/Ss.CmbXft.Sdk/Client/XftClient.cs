using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Ss.CmbXft.Sdk.Configuration;
using Ss.CmbXft.Sdk.Exceptions;
using Ss.CmbXft.Sdk.Models;
using Ss.CmbXft.Sdk.Utils;

namespace Ss.CmbXft.Sdk.Client;

/// <summary>
/// 薪福通API客户端实现
/// </summary>
public class XftClient : IXftClient
{
    private readonly HttpClient _httpClient;
    private readonly XftOptions _options;
    private readonly ILogger<XftClient> _logger;
    private bool _disposed;

    /// <summary>
    /// 初始化 <see cref="XftClient"/> 类的新实例
    /// </summary>
    /// <param name="options">配置选项</param>
    /// <param name="httpClient">HTTP客户端（可选）</param>
    /// <param name="logger">日志记录器（可选）</param>
    public XftClient(
        XftOptions options,
        HttpClient? httpClient = null,
        ILogger<XftClient>? logger = null)
    {
        // 验证配置
        var validationErrors = new List<string>();
        if (!options.Validate(out validationErrors))
        {
            throw new XftBusinessException($"CONFIG_ERROR", $"配置验证失败: {string.Join(", ", validationErrors)}");
        }

        _options = options;
        _logger = logger ?? NullLogger<XftClient>.Instance;

        // 创建或使用提供的HttpClient
        _httpClient = httpClient ?? new HttpClient();
        _httpClient.BaseAddress = new Uri(_options.BaseUrl);
        _httpClient.Timeout = TimeSpan.FromSeconds(_options.TimeoutSeconds);
    }

    /// <summary>
    /// 发送POST请求
    /// </summary>
    public async Task<ApiResponse<TResponse>> PostAsync<TRequest, TResponse>(
        string apiPath,
        TRequest request,
        Dictionary<string, string>? queryParams = null,
        CancellationToken cancellationToken = default)
    {
        string requestBody;
        if (request is string str)
        {
            requestBody = str;
        }
        else
        {
            requestBody = JsonConvert.SerializeObject(request);
        }

        LogDebug("POST请求 - URL: {Url}, Body: {Body}", apiPath, requestBody);

        var signInfo = SignUtils.BuildSignInfo(
            _options.AppId,
            _options.CompanyId,
            _options.UsrUid,
            _options.UsrNbr,
            apiPath,
            queryParams);

        var digest = SignUtils.Sm3Digest(requestBody);
        var signStr = SignUtils.BuildPostSignString(signInfo.Path, requestBody, signInfo.Timestamp);
        var apiSign = GmUtil.Sm3WithSm2Signature(_options.AuthoritySecret, signStr);

        // 设置请求头
        var httpRequest = new HttpRequestMessage(HttpMethod.Post, signInfo.Url);
        httpRequest.Headers.Add("appid", _options.AppId);
        httpRequest.Headers.Add("x-alb-timestamp", signInfo.Timestamp);
        httpRequest.Headers.Add("x-alb-verify", "sm3withsm2");
        httpRequest.Headers.Add("x-alb-digest", digest);
        httpRequest.Headers.Add("apisign", apiSign);
        httpRequest.Headers.Add("KeepAlive", "false");
        httpRequest.Headers.Add("UserAgent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.11 (KHTML, like Gecko) Chrome/23.0.1271.95 Safari/537.11");

        httpRequest.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");

        return await SendRequestAsync<TResponse>(httpRequest, cancellationToken);
    }

    /// <summary>
    /// 发送GET请求
    /// </summary>
    public async Task<ApiResponse<TResponse>> GetAsync<TResponse>(
        string apiPath,
        Dictionary<string, string>? queryParams = null,
        CancellationToken cancellationToken = default)
    {
        LogDebug("GET请求 - URL: {Url}", apiPath);

        var signInfo = SignUtils.BuildSignInfo(
            _options.AppId,
            _options.CompanyId,
            _options.UsrUid,
            _options.UsrNbr,
            apiPath,
            queryParams);

        var signStr = SignUtils.BuildGetSignString(signInfo.Path, signInfo.Timestamp);
        var apiSign = GmUtil.Sm3WithSm2Signature(_options.AuthoritySecret, signStr);

        // 设置请求头
        var httpRequest = new HttpRequestMessage(HttpMethod.Get, signInfo.Url);
        httpRequest.Headers.Add("appid", _options.AppId);
        httpRequest.Headers.Add("x-alb-timestamp", signInfo.Timestamp);
        httpRequest.Headers.Add("x-alb-verify", "sm3withsm2");
        httpRequest.Headers.Add("apisign", apiSign);
        httpRequest.Headers.Add("KeepAlive", "false");
        httpRequest.Headers.Add("UserAgent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.11 (KHTML, like Gecko) Chrome/23.0.1271.95 Safari/537.11");

        return await SendRequestAsync<TResponse>(httpRequest, cancellationToken);
    }

    /// <summary>
    /// 发送加密POST请求（SM4ECB加密）
    /// </summary>
    public async Task<ApiResponse<TResponse>> PostEncryptedAsync<TRequest, TResponse>(
        string apiPath,
        TRequest request,
        Dictionary<string, string>? queryParams = null,
        CancellationToken cancellationToken = default)
    {
        var requestBody = JsonConvert.SerializeObject(request);
        LogDebug("加密POST请求 - URL: {Url}, Body: {Body}", apiPath, requestBody);

        // 加密请求体
        var secretMsg = CryptographyUtils.EncryptWithSm4Ecb(requestBody, _options.GetEncryptionKey());
        var encryptedBody = JsonConvert.SerializeObject(new { secretMsg });

        return await PostAsync<string, TResponse>(apiPath, encryptedBody, queryParams, cancellationToken);
    }

    /// <summary>
    /// 发送加密POST请求（SM4ECB加密，返回解密后的响应）
    /// </summary>
    public async Task<string> PostEncryptedWithDecryptionAsync<TRequest>(
        string apiPath,
        TRequest request,
        Dictionary<string, string>? queryParams = null,
        CancellationToken cancellationToken = default)
    {
        string requestBody;
        if (request is string str)
        {
            requestBody = str;
        }
        else
        {
            requestBody = JsonConvert.SerializeObject(request);
        }

        LogDebug("加密POST请求（带解密） - URL: {Url}, Body: {Body}", apiPath, requestBody);

        // 加密请求体
        var secretMsg = CryptographyUtils.EncryptWithSm4Ecb(requestBody, _options.GetEncryptionKey());
        var encryptedBody = JsonConvert.SerializeObject(new { secretMsg });

        var signInfo = SignUtils.BuildSignInfo(
            _options.AppId,
            _options.CompanyId,
            _options.UsrUid,
            _options.UsrNbr,
            apiPath,
            queryParams);

        var digest = SignUtils.Sm3Digest(encryptedBody);
        var signStr = SignUtils.BuildPostSignString(signInfo.Path, encryptedBody, signInfo.Timestamp);
        var apiSign = GmUtil.Sm3WithSm2Signature(_options.AuthoritySecret, signStr);

        // 设置请求头
        var httpRequest = new HttpRequestMessage(HttpMethod.Post, signInfo.Url);
        httpRequest.Headers.Add("appid", _options.AppId);
        httpRequest.Headers.Add("x-alb-timestamp", signInfo.Timestamp);
        httpRequest.Headers.Add("x-alb-verify", "sm3withsm2");
        httpRequest.Headers.Add("x-alb-digest", digest);
        httpRequest.Headers.Add("apisign", apiSign);
        httpRequest.Headers.Add("KeepAlive", "false");
        httpRequest.Headers.Add("UserAgent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.11 (KHTML, like Gecko) Chrome/23.0.1271.95 Safari/537.11");

        httpRequest.Content = new StringContent(encryptedBody, Encoding.UTF8, "application/json");

        var rawResponse = await SendRawRequestAsync(httpRequest, cancellationToken);

        // 解密响应
        string decrypted;
        try
        {
            decrypted = CryptographyUtils.DecryptWithSm4Ecb(rawResponse, _options.GetEncryptionKey());
        }
        catch (Exception ex)
        {
            //主要针对 {"message":"You cannot consume this service"}
            LogError(ex, "响应解密失败，原始响应: {RawResponse}", rawResponse);
            throw new XftBusinessException("DECRYPT_FAILED", "响应解密失败", rawResponse);
        }

        // 检查解密后的响应是否包含业务错误
        CheckBusinessError(decrypted);

        return decrypted;
    }

    /// <summary>
    /// 发送HTTP请求并返回原始响应字符串
    /// </summary>
    private async Task<string> SendRawRequestAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        try
        {
            var response = await _httpClient.SendAsync(request, cancellationToken);
            var responseContent = await response.Content.ReadAsStringAsync();

            LogDebug("HTTP响应 - Status: {Status}, Content: {Content}", response.StatusCode, responseContent);

            return responseContent;
        }
        catch (HttpRequestException ex)
        {
            LogError(ex, "HTTP请求异常");
            throw new XftBusinessException($"HTTP请求失败: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            LogError(ex, "未知异常");
            throw new XftBusinessException($"API调用异常: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// 发送HTTP请求并处理响应
    /// </summary>
    private async Task<ApiResponse<TResponse>> SendRequestAsync<TResponse>(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        try
        {
            var response = await _httpClient.SendAsync(request, cancellationToken);
            var responseContent = await response.Content.ReadAsStringAsync();

            LogDebug("HTTP响应 - Status: {Status}, Content: {Content}", response.StatusCode, responseContent);

            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<TResponse>>(responseContent)
                ?? new ApiResponse<TResponse> { ReturnCode = "PARSE_ERROR", ErrorMsg = "响应解析失败" };

            if (!apiResponse.IsSuccess)
            {
                LogWarning("API请求失败 - Code: {Code}, Message: {Message}", apiResponse.ReturnCode, apiResponse.ErrorMsg);
            }

            return apiResponse;
        }
        catch (HttpRequestException ex)
        {
            LogError(ex, "HTTP请求异常");
            throw new XftBusinessException($"HTTP请求失败: {ex.Message}", ex);
        }
        catch (TaskCanceledException ex) when (!cancellationToken.IsCancellationRequested)
        {
            LogError(ex, "请求超时");
            throw new XftBusinessException($"请求超时: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            LogError(ex, "未知异常");
            throw new XftBusinessException($"API调用异常: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// 记录调试日志
    /// </summary>
    private void LogDebug(string message, params object[] args)
    {
        if (_options.EnableLogging)
        {
            _logger.LogDebug(message, args);
        }
    }

    /// <summary>
    /// 记录警告日志
    /// </summary>
    private void LogWarning(string message, params object[] args)
    {
        if (_options.EnableLogging)
        {
            _logger.LogWarning(message, args);
        }
    }

    /// <summary>
    /// 记录错误日志
    /// </summary>
    private void LogError(Exception ex, string message, params object[] args)
    {
        if (_options.EnableLogging)
        {
            _logger.LogError(ex, message, args);
        }
    }

    /// <summary>
    /// 检查业务错误
    /// </summary>
    private void CheckBusinessError(string decryptedResponse)
    {
        //{"SYCOMRETZ":[{"ERRMSG":"应用没有访问接口权限","ERRCOD":"OGRST02","ERRDTL":"","ERRPAM":""}]}
        var json = JObject.Parse(decryptedResponse);

        // 检查是否包含SYCOMRETZ错误
        var sycomretzToken = json["SYCOMRETZ"];
        if (sycomretzToken != null && sycomretzToken is JArray errors && errors.Count > 0)
        {
            throw new XftBusinessException("SYCOMRETZ", "薪福通API请求错误:" + decryptedResponse);
        }
    }

    /// <summary>
    /// 释放资源
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// 释放资源
    /// </summary>
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _httpClient?.Dispose();
            }
            _disposed = true;
        }
    }
}
