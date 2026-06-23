using System.Threading;
using System.Threading.Tasks;
using Ss.CmbXft.Sdk.Models;
using Ss.CmbXft.Sdk.Models.Post;

namespace Ss.CmbXft.Sdk.Services;

/// <summary>
/// 岗位服务接口
/// </summary>
public interface IPostService
{
    /// <summary>
    /// 分页查询岗位
    /// </summary>
    /// <param name="request">查询请求</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>岗位列表响应</returns>
    Task<ApiResponse<PostQueryResponseBody>> QueryPostAsync(
        PostQueryRequest request,
        CancellationToken cancellationToken = default);
}
