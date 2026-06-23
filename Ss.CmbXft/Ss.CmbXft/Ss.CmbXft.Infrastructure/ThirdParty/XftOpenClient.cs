using Ss.CmbXft.Sdk.Utils;
using Ss.CmbXft.Sdk.Configuration;
using Ss.CmbXft.Sdk.Models;
using System.Text.Json;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Ss.CmbXft.Infrastructure.ThirdParty;

public interface IXftOpenClient
{
    Task<string> DoPostAsync(string url, Dictionary<string, object> queryParams, object requestBody, CancellationToken ct = default);
    Task<string> DoGetAsync(string url, Dictionary<string, object> queryParams, CancellationToken ct = default);
    Task<string> DoSM4PostAsync(string url, Dictionary<string, object> queryParams, object requestBody, CancellationToken ct = default);
}

public class XftOpenClient : IXftOpenClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<XftOpenClient> _logger;
    private readonly XftOptions _options;

    public XftOpenClient(
        HttpClient httpClient,
        ILogger<XftOpenClient> logger,
        IOptions<XftOptions> options)
    {
        _httpClient = httpClient;
        _logger = logger;
        _options = options.Value;
    }

    public async Task<string> DoPostAsync(string url, Dictionary<string, object> queryParams, object requestBody, CancellationToken ct = default)
    {
        var bodyJson = requestBody is string s ? s : JsonSerializer.Serialize(requestBody);
        
        var stringQueryParams = queryParams?.ToDictionary(k => k.Key, v => v.Value?.ToString() ?? "");
        var signInf = SignUtils.BuildSignInfo(_options.AppId, _options.CompanyId, _options.UsrUid, _options.UsrNbr, url, stringQueryParams);
        
        // SM3 Digest
        var digest = CryptographyUtils.ComputeSm3Hash(bodyJson);
        
        var signStr = $"POST {signInf.Path}\nx-alb-digest: {bodyJson}\nx-alb-timestamp: {signInf.Timestamp}";
        var apiSign = GmUtil.Sm3WithSm2Signature(_options.AuthoritySecret, signStr);

        using var request = new HttpRequestMessage(HttpMethod.Post, signInf.Url);
        AddHeaders(request, signInf.Timestamp, digest, apiSign);
        request.Content = new StringContent(bodyJson, Encoding.UTF8, "application/json");

        return await SendAsync(request, ct);
    }

    public async Task<string> DoGetAsync(string url, Dictionary<string, object> queryParams, CancellationToken ct = default)
    {
        var stringQueryParams = queryParams?.ToDictionary(k => k.Key, v => v.Value?.ToString() ?? "");
        var signInf = SignUtils.BuildSignInfo(_options.AppId, _options.CompanyId, _options.UsrUid, _options.UsrNbr, url, stringQueryParams);
        
        var signStr = $"GET {signInf.Path}\nx-alb-timestamp: {signInf.Timestamp}";
        var apiSign = GmUtil.Sm3WithSm2Signature(_options.AuthoritySecret, signStr);

        using var request = new HttpRequestMessage(HttpMethod.Get, signInf.Url);
        AddHeaders(request, signInf.Timestamp, null, apiSign);

        return await SendAsync(request, ct);
    }

    public async Task<string> DoSM4PostAsync(string url, Dictionary<string, object> queryParams, object requestBody, CancellationToken ct = default)
    {
        var bodyJson = requestBody is string s ? s : JsonSerializer.Serialize(requestBody);
        var secretMsg = CryptographyUtils.EncryptWithSm4Ecb(bodyJson, _options.AuthoritySecret.Substring(0, 32));
        
        var encryptedBody = JsonSerializer.Serialize(new { secretMsg });
        return await DoPostAsync(url, queryParams, encryptedBody, ct);
    }

    private void AddHeaders(HttpRequestMessage request, string timestamp, string? digest, string apiSign)
    {
        request.Headers.Add("appid", _options.AppId);
        request.Headers.Add("x-alb-timestamp", timestamp);
        request.Headers.Add("x-alb-verify", "sm3withsm2");
        if (!string.IsNullOrEmpty(digest))
        {
            request.Headers.Add("x-alb-digest", digest);
        }
        request.Headers.Add("apisign", apiSign);
        request.Headers.Add("User-Agent", "Ss.CmbXft.Api/1.0");
    }

    private async Task<string> SendAsync(HttpRequestMessage request, CancellationToken ct)
    {
        var response = await _httpClient.SendAsync(request, ct);
        var content = await response.Content.ReadAsStringAsync(ct);

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError("XFT API Failed: {StatusCode}, {Content}", response.StatusCode, content);
            throw new HttpRequestException($"XFT API Error: {response.StatusCode}");
        }

        return content;
    }
}
