namespace Ss.CmbXft.Application.Dtos;

/// <summary>
/// 角色数据传输对象
/// </summary>
public class RoleDto
{
    /// <summary>
    /// 角色编号
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// 角色名称
    /// </summary>
    public string Name { get; set; } = string.Empty;
}

/// <summary>
/// 获取角色列表请求DTO
/// </summary>
public class GetRoleListRequestDto
{
    /// <summary>
    /// 当前页
    /// </summary>
    public long CurrentPage { get; set; } = 1;

    /// <summary>
    /// 每页大小
    /// </summary>
    public long PageSize { get; set; } = 20;
}
