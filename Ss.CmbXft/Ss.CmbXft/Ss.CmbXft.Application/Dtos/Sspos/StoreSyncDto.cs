using Ss.CmbXft.Common.Models.Request;

namespace Ss.CmbXft.Application.Dtos.Sspos;

/// <summary>
/// 门店同步查询条件 DTO
/// </summary>
public class StoreSyncQueryDto : PageRequestBase
{
    /// <summary>
    /// 门店编码（精确匹配，多个用逗号分隔）
    /// </summary>
    public string? LocationCode { get; set; }

    /// <summary>
    /// 门店名称（模糊搜索）
    /// </summary>
    public string? LocationName { get; set; }

    /// <summary>
    /// 是否只同步激活状态的门店（默认只同步激活的门店）
    /// </summary>
    public bool? Active { get; set; } = true;
}
