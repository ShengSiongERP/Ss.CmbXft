using System.Threading.Tasks;
using Ss.CmbXft.Application.Dtos;
using Ss.CmbXft.Common.Models;

namespace Ss.CmbXft.Application.Services;

/// <summary>
/// 职位应用服务接口
/// </summary>
public interface IXftPositionAppService
{
    /// <summary>
    /// 分页查询职位
    /// </summary>
    /// <param name="input">查询请求</param>
    /// <returns>职位列表分页结果</returns>
    Task<PageResult<PositionDto>> GetPositionListAsync(GetPositionListRequestDto input);
}