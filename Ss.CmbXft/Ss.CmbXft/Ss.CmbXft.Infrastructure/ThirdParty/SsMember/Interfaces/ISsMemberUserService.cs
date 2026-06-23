using Ss.CmbXft.Domain.ThirdParty.SsMember;
using Ss.CmbXft.Infrastructure.ThirdParty.SsMember.Model.UserInfo;

namespace Ss.CmbXft.Infrastructure.ThirdParty.SsMember.Interfaces;

/// <summary>
/// 昇菘会员系统用户服务接口
/// </summary>
public interface ISsMemberUserService
{
    /// <summary>
    /// 获取用户信息
    /// </summary>
    /// <param name="userSign">用户标识（动态二维码）</param>
    /// <param name="ct">取消令牌</param>
    /// <returns>用户信息</returns>
    Task<SsMemberResponse<SsUserInfoData>> GetUserInfoAsync(string userSign, CancellationToken ct = default);
}
