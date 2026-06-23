using System.Threading;
using System.Threading.Tasks;
using Ss.CmbXft.Sdk.Models;
using Ss.CmbXft.Sdk.Models.Position;

namespace Ss.CmbXft.Sdk.Services;

/// <summary>
/// 职位服务接口
/// </summary>
public interface IPositionService
{
    /// <summary>
    /// 分页查询职位
    /// </summary>
    /// <param name="request">查询请求</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>职位列表响应</returns>
    Task<ApiResponse<PositionQueryResponseBody>> QueryPositionAsync(
        PositionQueryRequest request,
        CancellationToken cancellationToken = default);
}
