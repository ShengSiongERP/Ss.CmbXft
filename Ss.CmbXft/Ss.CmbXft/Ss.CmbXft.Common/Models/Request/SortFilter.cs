using System.ComponentModel.DataAnnotations;

namespace Ss.CmbXft.Common.Models.Request;

/// <summary>
/// 排序请求项
/// </summary>
public sealed class SortItem
{
    /// <summary>
    /// 排序字段名
    /// </summary>
    public string Field { get; set; } = string.Empty;

    /// <summary>
    /// 是否降序
    /// </summary>
    public bool Desc { get; set; }

    public override string ToString()
    {
        if (string.IsNullOrWhiteSpace(Field))
            return string.Empty;
        return $"{Field} {(Desc ? "DESC" : "ASC")}";
    }
}

/// <summary>
/// 筛选操作符
/// </summary>
public enum FilterOperator
{
    Equal = 0,
    NotEqual = 1,

    GreaterThan = 2,
    GreaterThanOrEqual = 3,
    LessThan = 4,
    LessThanOrEqual = 5,

    Contains = 6,
    NotContains = 7,

    StartsWith = 8,
    EndsWith = 9,

    In = 10,
    NotIn = 11,

    IsNull = 12,
    IsNotNull = 13,

    Between = 14,
    NotBetween = 15,
}

/// <summary>
/// 筛选条件项
/// </summary>
public sealed class FilterItem
{
    /// <summary>
    /// 字段名
    /// </summary>
    [Required(ErrorMessage = "筛选字段不能为空")]
    [StringLength(100, ErrorMessage = "筛选字段长度不能超过100个字符")]
    public string Field { get; set; } = string.Empty;

    /// <summary>
    /// 操作符
    /// </summary>
    public FilterOperator Operator { get; set; } = FilterOperator.Equal;

    /// <summary>
    /// 值（用于单值操作符）
    /// </summary>
    public string? Value { get; set; }

    /// <summary>
    /// 值列表（用于 In/NotIn 操作符）
    /// </summary>
    public List<string>? Values { get; set; }

    /// <summary>
    /// 最小值（用于 Between 操作符）
    /// </summary>
    public string? MinValue { get; set; }

    /// <summary>
    /// 最大值（用于 Between 操作符）
    /// </summary>
    public string? MaxValue { get; set; }

    /// <summary>
    /// 逻辑关系（AND/OR）
    /// </summary>
    public bool IsOr { get; set; }
}
