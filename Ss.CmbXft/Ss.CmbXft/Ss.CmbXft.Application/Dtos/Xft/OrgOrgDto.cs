using System;
using System.Collections.Generic;

namespace Ss.CmbXft.Application.Dtos;

/// <summary>
/// 组织数据传输对象
/// </summary>
public class OrgOrgDto
{
    /// <summary>
    /// 组织ID
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// 组织编码
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// 生效日期
    /// </summary>
    public DateTime? EffectiveDate { get; set; }

    /// <summary>
    /// 组织id全路径
    /// </summary>
    public string IdPath { get; set; } = string.Empty;

    /// <summary>
    /// 组织名称
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 组织名称全路径
    /// </summary>
    public string? NamePath { get; set; }

    /// <summary>
    /// 机构号
    /// </summary>
    public string? Number { get; set; }

    /// <summary>
    /// 同级排序序号
    /// </summary>
    public int? OrderNumber { get; set; }

    /// <summary>
    /// 上级组织code
    /// </summary>
    public string? ParentCode { get; set; }

    /// <summary>
    /// 上级组织ID
    /// </summary>
    public string? ParentId { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 生效状态，active-正常，delete-删除，stopped-停用
    /// </summary>
    public string? Status { get; set; }

    /// <summary>
    /// 组织类型，G-集团、B-分公司、SP-门店、S-子公司、BD-事业部、D-部门、PT-项目组
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// 组织负责人
    /// </summary>
    public List<OrganizationLeaderDto>? Leaders { get; set; }

    /// <summary>
    /// 审批主管
    /// </summary>
    public List<OrganizationApproverDto>? Approvers { get; set; }

    /// <summary>
    /// 组织自定义数据
    /// </summary>
    public List<OrganizationExtDataDto>? ExtData { get; set; }

    /// <summary>
    /// 组织是否为叶节点
    /// </summary>
    public bool? IsLeaf { get; set; }
}

/// <summary>
/// 组织负责人DTO
/// </summary>
public class OrganizationLeaderDto
{
    /// <summary>
    /// 企业用户id
    /// </summary>
    public string? EnterpriseUserId { get; set; }

    /// <summary>
    /// 姓名
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// 员工id
    /// </summary>
    public string? StaffId { get; set; }

    /// <summary>
    /// 员工号
    /// </summary>
    public string? StaffNum { get; set; }
}

/// <summary>
/// 审批主管DTO
/// </summary>
public class OrganizationApproverDto
{
    /// <summary>
    /// 企业用户id
    /// </summary>
    public string? EnterpriseUserId { get; set; }

    /// <summary>
    /// 姓名
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// 员工id
    /// </summary>
    public string? StaffId { get; set; }

    /// <summary>
    /// 员工号
    /// </summary>
    public string? StaffNum { get; set; }
}

/// <summary>
/// 组织自定义数据DTO
/// </summary>
public class OrganizationExtDataDto
{
    /// <summary>
    /// 自定义字段编码
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// 自定义字段名称
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// 扩展字段类型
    /// </summary>
    public string? FieldType { get; set; }

    /// <summary>
    /// 自定义字段值
    /// </summary>
    public object? Value { get; set; }
}

/// <summary>
/// 获取组织列表请求DTO
/// </summary>
public class GetOrganizationListRequestDto
{
    /// <summary>
    /// 组织编码集合
    /// </summary>
    public List<string>? Codes { get; set; }

    /// <summary>
    /// 组织id集合
    /// </summary>
    public List<string>? Ids { get; set; }

    /// <summary>
    /// 搜索关键词
    /// </summary>
    public string? Keyword { get; set; }

    /// <summary>
    /// 父组织id
    /// </summary>
    public string? ParentId { get; set; }

    /// <summary>
    /// 生效状态集合
    /// </summary>
    public List<string>? Status { get; set; }

    /// <summary>
    /// 当前页
    /// </summary>
    public long CurrentPage { get; set; } = 1;

    /// <summary>
    /// 页大小
    /// </summary>
    public long PageSize { get; set; } = 10;

    /// <summary>
    /// 扩展查询选项
    /// </summary>
    public List<string>? ExtOptions { get; set; }
}
