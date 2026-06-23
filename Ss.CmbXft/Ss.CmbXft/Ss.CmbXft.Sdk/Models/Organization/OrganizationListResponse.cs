using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Ss.CmbXft.Sdk.Models.Organization;

/// <summary>
/// 获取组织列表响应Body
/// </summary>
public class OrganizationListResponseBody
{
    /// <summary>
    /// 当前页
    /// </summary>
    [JsonProperty("currentPage")]
    public long? CurrentPage { get; set; }

    /// <summary>
    /// 每页大小
    /// </summary>
    [JsonProperty("pageSize")]
    public long? PageSize { get; set; }

    /// <summary>
    /// 总条数
    /// </summary>
    [JsonProperty("totalSize")]
    public long? TotalSize { get; set; }

    /// <summary>
    /// 数据记录
    /// </summary>
    [JsonProperty("records")]
    public List<OrganizationInfo> Records { get; set; } = new();
}

/// <summary>
/// 组织信息
/// </summary>
public class OrganizationInfo
{
    /// <summary>
    /// 组织ID
    /// </summary>
    [JsonProperty("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// 组织编码
    /// </summary>
    [JsonProperty("code")]
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// 生效日期，日期格式：yyyy-MM-dd
    /// </summary>
    [JsonProperty("effectiveDate")]
    public DateTime? EffectiveDate { get; set; }

    /// <summary>
    /// 组织id全路径
    /// </summary>
    [JsonProperty("idPath")]
    public string IdPath { get; set; } = string.Empty;

    /// <summary>
    /// 组织名称
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 组织名称全路径
    /// </summary>
    [JsonProperty("namePath")]
    public string? NamePath { get; set; }

    /// <summary>
    /// 机构号
    /// </summary>
    [JsonProperty("number")]
    public string? Number { get; set; }

    /// <summary>
    /// 同级排序序号，小的排在前面
    /// </summary>
    [JsonProperty("orderNumber")]
    public int? OrderNumber { get; set; }

    /// <summary>
    /// 上级组织code
    /// </summary>
    [JsonProperty("parentCode")]
    public string? ParentCode { get; set; }

    /// <summary>
    /// 上级组织ID
    /// </summary>
    [JsonProperty("parentId")]
    public string? ParentId { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [JsonProperty("remark")]
    public string? Remark { get; set; }

    /// <summary>
    /// 生效状态，active-正常，delete-删除，stopped-停用
    /// </summary>
    [JsonProperty("status")]
    public string? Status { get; set; }

    /// <summary>
    /// 组织类型，G-集团、B-分公司、SP-门店、S-子公司、BD-事业部、D-部门、PT-项目组
    /// </summary>
    [JsonProperty("type")]
    public string? Type { get; set; }

    /// <summary>
    /// 组织负责人
    /// </summary>
    [JsonProperty("leaders")]
    public List<OrganizationLeader>? Leaders { get; set; }

    /// <summary>
    /// 审批主管
    /// </summary>
    [JsonProperty("approvers")]
    public List<OrganizationApprover>? Approvers { get; set; }

    /// <summary>
    /// 组织自定义数据
    /// </summary>
    [JsonProperty("extData")]
    public List<OrganizationExtData>? ExtData { get; set; }

    /// <summary>
    /// 组织是否为叶节点
    /// </summary>
    [JsonProperty("isLeaf")]
    public bool? IsLeaf { get; set; }
}

/// <summary>
/// 组织负责人
/// </summary>
public class OrganizationLeader
{
    /// <summary>
    /// 企业用户id
    /// </summary>
    [JsonProperty("enterpriseUserId")]
    public string? EnterpriseUserId { get; set; }

    /// <summary>
    /// 姓名
    /// </summary>
    [JsonProperty("name")]
    public string? Name { get; set; }

    /// <summary>
    /// 员工id
    /// </summary>
    [JsonProperty("staffId")]
    public string? StaffId { get; set; }

    /// <summary>
    /// 员工号
    /// </summary>
    [JsonProperty("staffNum")]
    public string? StaffNum { get; set; }
}

/// <summary>
/// 审批主管
/// </summary>
public class OrganizationApprover
{
    /// <summary>
    /// 企业用户id
    /// </summary>
    [JsonProperty("enterpriseUserId")]
    public string? EnterpriseUserId { get; set; }

    /// <summary>
    /// 姓名
    /// </summary>
    [JsonProperty("name")]
    public string? Name { get; set; }

    /// <summary>
    /// 员工id
    /// </summary>
    [JsonProperty("staffId")]
    public string? StaffId { get; set; }

    /// <summary>
    /// 员工号
    /// </summary>
    [JsonProperty("staffNum")]
    public string? StaffNum { get; set; }
}

/// <summary>
/// 组织自定义数据
/// </summary>
public class OrganizationExtData
{
    /// <summary>
    /// 自定义字段编码
    /// </summary>
    [JsonProperty("code")]
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// 自定义字段名称
    /// </summary>
    [JsonProperty("name")]
    public string? Name { get; set; }

    /// <summary>
    /// 扩展字段类型，NUMBER-数字 DATE-日期 TEXT-文本 SELECT-单选 CHECKBOX-多选 USER_SELECT-选择用户
    /// </summary>
    [JsonProperty("fieldType")]
    public string? FieldType { get; set; }

    /// <summary>
    /// 自定义字段值，类型不固定，可能为数字，日期，字符串，对象，数组
    /// </summary>
    [JsonProperty("value")]
    public object? Value { get; set; }
}
