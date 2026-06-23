using System.Threading.Tasks;
using Ss.CmbXft.Application.Dtos;
using Ss.CmbXft.Common.Models;

namespace Ss.CmbXft.Application.Services;

/// <summary>
/// 角色应用服务接口
/// </summary>
public interface IRoleAppService
{
    /// <summary>
    /// 分页获取企业下所有角色
    /// </summary>
    /// <param name="input">查询请求</param>
    /// <returns>角色列表分页结果</returns>
    Task<PageResult<RoleDto>> GetRoleListAsync(GetRoleListRequestDto input);
}
