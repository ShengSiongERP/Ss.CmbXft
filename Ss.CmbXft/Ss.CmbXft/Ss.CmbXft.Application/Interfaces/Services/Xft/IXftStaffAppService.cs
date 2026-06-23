using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ss.CmbXft.Application.Dtos;
using Ss.CmbXft.Application.Dtos.Xft;
using Ss.CmbXft.Common.Models;

namespace Ss.CmbXft.Application.Services;

/// <summary>
/// 员工应用服务接口
/// </summary>
public interface IXftStaffAppService
{
    /// <summary>
    /// 分页查询员工列表
    /// </summary>
    Task<PageResult<XftStaffDto>> GetPageAsync(XftStaffQueryDto query);

    /// <summary>
    /// 获取所有员工（下拉框等场景使用）
    /// </summary>
    Task<List<XftStaffDto>> GetListAsync();

    /// <summary>
    /// 根据ID获取员工详情
    /// </summary>
    Task<XftStaffDto?> GetAsync(long id);

    /// <summary>
    /// 创建员工
    /// </summary>
    Task<XftStaffDto> CreateAsync(XftStaffSaveDto dto);

    /// <summary>
    /// 更新员工
    /// </summary>
    Task<XftStaffDto> UpdateAsync(long id, XftStaffSaveDto dto);

    /// <summary>
    /// 删除员工（软删除）
    /// </summary>
    Task DeleteAsync(long id);

    /// <summary>
    /// 批量删除员工（软删除）
    /// </summary>
    Task DeleteBatchAsync(List<long> ids);
}
