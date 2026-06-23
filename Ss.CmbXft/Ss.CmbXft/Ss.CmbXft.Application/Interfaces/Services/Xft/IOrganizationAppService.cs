using System.Threading.Tasks;
using Ss.CmbXft.Application.Dtos;
using Ss.CmbXft.Common.Models;

namespace Ss.CmbXft.Application.Services;

/// <summary>
/// 组织应用服务接口
/// </summary>
public interface IOrganizationAppService
{
    /// <summary>
    /// 分页获取组织列表
    /// </summary>
    /// <param name="input">查询请求</param>
    /// <returns>组织列表分页结果</returns>
    Task<PageResult<OrgOrgDto>> GetOrganizationListAsync(GetOrganizationListRequestDto input);
}
