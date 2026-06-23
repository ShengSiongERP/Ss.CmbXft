using System.Threading.Tasks;
using Ss.CmbXft.Application.Dtos;
using Ss.CmbXft.Common.Models;

namespace Ss.CmbXft.Application.Services;

/// <summary>
/// 岗位应用服务接口
/// </summary>
public interface IPostAppService
{
    /// <summary>
    /// 分页查询岗位
    /// </summary>
    /// <param name="input">查询请求</param>
    /// <returns>岗位列表分页结果</returns>
    Task<PageResult<PostDto>> GetPostListAsync(GetPostListRequestDto input);
}
