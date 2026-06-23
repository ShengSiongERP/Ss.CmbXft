using Ss.CmbXft.Application.Dtos.Sserp;
using Ss.CmbXft.Common.Models;

namespace Ss.CmbXft.Application.Services;

/// <summary>
/// Abp用户应用服务接口
/// </summary>
public interface IAbpUserAppService
{
    /// <summary>
    /// 分页查询用户列表
    /// </summary>
    Task<PageResult<AbpUserDto>> GetPageAsync(AbpUserQueryDto query);

    /// <summary>
    /// 获取所有用户列表
    /// </summary>
    Task<List<AbpUserDto>> GetListAsync();

    /// <summary>
    /// 根据用户ID获取详情
    /// </summary>
    Task<AbpUserDto?> GetAsync(Guid id);

    /// <summary>
    /// 创建用户
    /// </summary>
    Task<AbpUserDto> CreateAsync(AbpUserSaveDto dto);

    /// <summary>
    /// 更新用户
    /// </summary>
    Task<AbpUserDto> UpdateAsync(Guid id, AbpUserSaveDto dto);

    /// <summary>
    /// 删除用户
    /// </summary>
    Task DeleteAsync(Guid id);

    /// <summary>
    /// 批量删除用户
    /// </summary>
    Task DeleteBatchAsync(List<Guid> ids);
}