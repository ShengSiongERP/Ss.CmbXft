using System.Threading;
using System.Threading.Tasks;
using Ss.CmbXft.Sdk.Models;
using Ss.CmbXft.Sdk.Models.Organization;

namespace Ss.CmbXft.Sdk.Services;

/// <summary>
/// 组织服务接口
/// </summary>
public interface IOrganizationService
{
    /// <summary>
    /// 分页获取组织列表
    /// </summary>
    /// <param name="request">查询请求</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>组织列表响应</returns>
    Task<ApiResponse<OrganizationListResponseBody>> GetOrganizationListAsync(
        OrganizationListRequest request,
        CancellationToken cancellationToken = default);
}
