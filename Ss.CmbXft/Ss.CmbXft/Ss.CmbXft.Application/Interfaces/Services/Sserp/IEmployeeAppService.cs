using Ss.CmbXft.Application.Dtos.Sserp;
using Ss.CmbXft.Common.Models;

namespace Ss.CmbXft.Application.Services;

/// <summary>
/// 员工信息应用服务接口
/// </summary>
public interface IEmployeeAppService
{
    /// <summary>
    /// 分页查询员工列表
    /// </summary>
    Task<PageResult<EmployeeDto>> GetPageAsync(EmployeeQueryDto query);

    /// <summary>
    /// 获取所有员工列表
    /// </summary>
    Task<List<EmployeeDto>> GetListAsync();

    /// <summary>
    /// 根据员工编码获取详情
    /// </summary>
    Task<EmployeeDto?> GetAsync(string employeeCode);

    /// <summary>
    /// 创建员工
    /// </summary>
    Task<EmployeeDto> CreateAsync(EmployeeSaveDto dto);

    /// <summary>
    /// 更新员工
    /// </summary>
    Task<EmployeeDto> UpdateAsync(string employeeCode, EmployeeSaveDto dto);

    /// <summary>
    /// 删除员工
    /// </summary>
    Task DeleteAsync(string employeeCode);

    /// <summary>
    /// 批量删除员工
    /// </summary>
    Task DeleteBatchAsync(List<string> employeeCodes);

    /// <summary>
    /// 批量添加员工
    /// </summary>
    Task<List<EmployeeDto>> CreateBatchAsync(List<EmployeeSaveDto> dtos);
}