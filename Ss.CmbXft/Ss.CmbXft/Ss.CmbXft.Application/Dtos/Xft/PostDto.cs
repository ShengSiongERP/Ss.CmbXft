using System.Collections.Generic;

namespace Ss.CmbXft.Application.Dtos;

/// <summary>
/// 岗位数据传输对象
/// </summary>
public class PostDto
{
    /// <summary>
    /// 岗位流水号
    /// </summary>
    public string SequenceNumber { get; set; } = string.Empty;

    /// <summary>
    /// 岗位编号
    /// </summary>
    public string CodeNumber { get; set; } = string.Empty;

    /// <summary>
    /// 岗位名称
    /// </summary>
    public string PositionName { get; set; } = string.Empty;

    /// <summary>
    /// 所属组织机构列表
    /// </summary>
    public List<PostOrganizationDto>? Organizations { get; set; }

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
/// 岗位所属组织机构DTO
/// </summary>
public class PostOrganizationDto
{
    /// <summary>
    /// 组织ID
    /// </summary>
    public string OrganizationId { get; set; } = string.Empty;

    /// <summary>
    /// 组织名称
    /// </summary>
    public string OrganizationName { get; set; } = string.Empty;

    /// <summary>
    /// 组织名称路径
    /// </summary>
    public string? OrganizationNamePath { get; set; }
}

/// <summary>
/// 查询岗位请求DTO
/// </summary>
public class GetPostListRequestDto
{
    /// <summary>
    /// 流水号列表
    /// </summary>
    public List<string>? SequenceNumbers { get; set; }

    /// <summary>
    /// 岗位名称，支持模糊查询
    /// </summary>
    public string? PositionName { get; set; }

    /// <summary>
    /// 所属组织机构ID
    /// </summary>
    public List<string>? OrganizationIds { get; set; }

    /// <summary>
    /// 岗位编号，支持模糊查询
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
