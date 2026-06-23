using System.Threading;
using System.Threading.Tasks;
using Ss.CmbXft.Sdk.Models;
using Ss.CmbXft.Sdk.Models.Role;

namespace Ss.CmbXft.Sdk.Services;

/// <summary>
/// 角色服务接口
/// </summary>
public interface IRoleService
{
    /// <summary>
    /// 分页获取企业下所有角色
    /// </summary>
    /// <param name="request">角色列表请求</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>角色列表响应</returns>
    Task<ApiResponse<RoleListResponseBody>> GetRoleListAsync(
        RoleListRequest request,
        CancellationToken cancellationToken = default);
}
