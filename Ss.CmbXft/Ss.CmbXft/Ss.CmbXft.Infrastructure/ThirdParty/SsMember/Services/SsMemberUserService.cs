using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Ss.CmbXft.Domain.ThirdParty.SsMember;
using Ss.CmbXft.Infrastructure.ThirdParty;
using Ss.CmbXft.Infrastructure.ThirdParty.SsMember.Model.GoodsSync;
using Ss.CmbXft.Infrastructure.ThirdParty.SsMember.Model.UserInfo;
using Ss.CmbXft.Infrastructure.ThirdParty.SsMember.Interfaces;

namespace Ss.CmbXft.Infrastructure.ThirdParty.SsMember.Services;

/// <summary>
/// 昇菘会员系统商品同步服务实现
/// </summary>
public class SsMemberUserService : ISsMemberUserService
{
    private readonly ISsMemberClient _client;
    private readonly ILogger<SsMemberUserService> _logger;

    public SsMemberUserService(
        ISsMemberClient client,
        ILogger<SsMemberUserService> logger)
    {
        _client = client;
        _logger = logger;
    }

    /// <summary>
    /// 获取用户信息
    /// </summary>
    public async Task<SsMemberResponse<SsUserInfoData>> GetUserInfoAsync(string userSign, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(userSign))
        {
            _logger.LogWarning("用户标识为空，无法获取用户信息");
            return new SsMemberResponse<SsUserInfoData>
            {
                Code = 400,
                Status = "fail",
                Message = "用户标识不能为空"
            };
        }
        _logger.LogInformation("开始获取用户信息，UserSign: {UserSign}", userSign);
        var request = new GetUserInfoRequest { UserSign = userSign };
        var response = await _client.PostAsync<GetUserInfoRequest, SsMemberResponse<SsUserInfoData>>(
            "/api/erp/user", request, ct);
        if (response.IsSuccess)
        {
            _logger.LogInformation("成功获取用户信息，UID: {Uid}, 余额: {Money}, 积分: {Integral}",
                response.Data?.Uid ?? "N/A",
                response.Data?.Money ?? "N/A",
                response.Data?.Integral ?? "N/A");
        }
        else
        {
            _logger.LogError("获取用户信息失败: {Message}", response.GetErrorMessage());
        }
        return response;
    }
}
