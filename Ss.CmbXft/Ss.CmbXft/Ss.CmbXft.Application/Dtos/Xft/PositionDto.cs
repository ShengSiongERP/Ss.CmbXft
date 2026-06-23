using System.Collections.Generic;

namespace Ss.CmbXft.Application.Dtos;

/// <summary>
/// 职位数据传输对象
/// </summary>
public class PositionDto
{
    /// <summary>
    /// 职位流水号
    /// </summary>
    public string SequenceNumber { get; set; } = string.Empty;

    /// <summary>
    /// 职位编号
    /// </summary>
    public string CodeNumber { get; set; } = string.Empty;

    /// <summary>
    /// 职位名称
    /// </summary>
    public string JobName { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 顺序号
    /// </summary>
    public string? OrderNumber { get; set; }
}

/// <summary>
/// 查询职位请求DTO
/// </summary>
public class GetPositionListRequestDto
{
    /// <summary>
    /// 流水号列表
    /// </summary>
    public List<string>? SequenceNumbers { get; set; }

    /// <summary>
    /// 职位名称，支持模糊查询
    /// </summary>
    public string? JobName { get; set; }

    /// <summary>
    /// 职位编号，支持模糊查询
    /// </summary>
    public string? CodeNumber { get; set; }

    /// <summary>
    /// 当前页数
    /// </summary>
    public long CurrentPage { get; set; } = 1;

    /// <summary>
    /// 每页大小
    /// </summary>
    public long PageSize { get; set; } = 10;
}
