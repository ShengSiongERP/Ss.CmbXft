using System.ComponentModel.DataAnnotations;

namespace Ss.CmbXft.Common.Models;

/// <summary>
/// 排序方向
/// </summary>
public enum SortDirection
{
    /// <summary>
    /// 升序
    /// </summary>
    Asc = 0,
    /// <summary>
    /// 降序
    /// </summary>
    Desc = 1,
}

/// <summary>
/// 排序请求项
/// </summary>
public sealed class SortItem
{
    /// <summary>
    /// 排序字段名
    /// </summary>
    [Required(ErrorMessage = "排序字段不能为空")]
    [StringLength(100, ErrorMessage = "排序字段长度不能超过100个字符")]
    public string Field { get; set; } = string.Empty;

    /// <summary>
    /// 排序方向
    /// </summary>
    public SortDirection Direction { get; set; } = SortDirection.Asc;

    /// <summary>
    /// 创建升序排序项
    /// </summary>
    public static SortItem Asc(string field) => new() { Field = field, Direction = SortDirection.Asc };

    /// <summary>
    /// 创建降序排序项
    /// </summary>
    public static SortItem Desc(string field) => new() { Field = field, Direction = SortDirection.Desc };

    /// <summary>
    /// 转换为字符串表示（用于数据库查询）
    /// </summary>
    public override string ToString()
    {
        return Direction == SortDirection.Asc ? $"{Field} ASC" : $"{Field} DESC";
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
    public string Logic { get; set; } = "AND";

    /// <summary>
    /// 子条件（用于复杂筛选）
    /// </summary>
    public List<FilterItem>? Children { get; set; }

    /// <summary>
    /// 创建等于条件
    /// </summary>
    public static FilterItem Equal(string field, string value) => new() { Field = field, Operator = FilterOperator.Equal, Value = value };

    /// <summary>
    /// 创建不等于条件
    /// </summary>
    public static FilterItem NotEqual(string field, string value) => new() { Field = field, Operator = FilterOperator.NotEqual, Value = value };

    /// <summary>
    /// 创建包含条件
    /// </summary>
    public static FilterItem Contains(string field, string value) => new() { Field = field, Operator = FilterOperator.Contains, Value = value };

    /// <summary>
    /// 创建 In 条件
    /// </summary>
    public static FilterItem In(string field, params string[] values) => new() { Field = field, Operator = FilterOperator.In, Values = values.ToList() };

    /// <summary>
    /// 创建 Between 条件
    /// </summary>
    public static FilterItem Between(string field, string minValue, string maxValue) => new() { Field = field, Operator = FilterOperator.Between, MinValue = minValue, MaxValue = maxValue };

    /// <summary>
    /// 创建为空条件
    /// </summary>
    public static FilterItem IsNull(string field) => new() { Field = field, Operator = FilterOperator.IsNull };

    /// <summary>
    /// 创建不为空条件
    /// </summary>
    public static FilterItem IsNotNull(string field) => new() { Field = field, Operator = FilterOperator.IsNotNull };
}
