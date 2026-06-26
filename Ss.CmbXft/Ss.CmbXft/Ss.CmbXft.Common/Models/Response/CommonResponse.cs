using System.Text.Json.Serialization;

namespace Ss.CmbXft.Common.Models;

/// <summary>
/// 下拉选项（通用键值对）
/// </summary>
public sealed class SelectOption
{
    /// <summary>
    /// 值
    /// </summary>
    public string Value { get; set; } = string.Empty;

    /// <summary>
    /// 显示文本
    /// </summary>
    public string Label { get; set; } = string.Empty;

    /// <summary>
    /// 是否禁用
    /// </summary>
    public bool Disabled { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    public int Sort { get; set; }

    /// <summary>
    /// 创建选项
    /// </summary>
    public static SelectOption Create(string value, string label, bool disabled = false, int sort = 0) =>
        new() { Value = value, Label = label, Disabled = disabled, Sort = sort };
}

/// <summary>
/// 泛型下拉选项
/// </summary>
public sealed class SelectOption<TValue>
{
    /// <summary>
    /// 值
    /// </summary>
    public required TValue Value { get; set; }

    /// <summary>
    /// 显示文本
    /// </summary>
    public string Label { get; set; } = string.Empty;

    /// <summary>
    /// 是否禁用
    /// </summary>
    public bool Disabled { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    public int Sort { get; set; }
}

/// <summary>
/// 带分组的下拉选项
/// </summary>
public sealed class SelectGroupOption
{
    /// <summary>
    /// 分组名称
    /// </summary>
    public string Group { get; set; } = string.Empty;

    /// <summary>
    /// 选项列表
    /// </summary>
    public List<SelectOption> Options { get; set; } = [];
}

/// <summary>
/// 枚举项（用于前端渲染枚举下拉）
/// </summary>
public sealed class EnumOption
{
    /// <summary>
    /// 枚举值
    /// </summary>
    public int Value { get; set; }

    /// <summary>
    /// 枚举名称
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 显示文本
    /// </summary>
    public string Label { get; set; } = string.Empty;

    /// <summary>
    /// 描述
    /// </summary>
    public string? Description { get; set; }
}

/// <summary>
/// 审计信息（公共创建/修改字段），可嵌入到响应 DTO 中
/// </summary>
public sealed class AuditInfo
{
    /// <summary>
    /// 创建人ID
    /// </summary>
    public string? CreatedBy { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime? CreatedOn { get; set; }

    /// <summary>
    /// 修改人ID
    /// </summary>
    public string? ModifiedBy { get; set; }

    /// <summary>
    /// 修改时间
    /// </summary>
    public DateTime? ModifiedOn { get; set; }
}

/// <summary>
/// 批量操作结果
/// </summary>
public sealed class BatchResult
{
    /// <summary>
    /// 成功数量
    /// </summary>
    public int SuccessCount { get; set; }

    /// <summary>
    /// 失败数量
    /// </summary>
    public int FailCount { get; set; }

    /// <summary>
    /// 失败详情
    /// </summary>
    public List<BatchFailureDetail> Failures { get; set; } = [];

    /// <summary>
    /// 总数
    /// </summary>
    public int TotalCount => SuccessCount + FailCount;

    /// <summary>
    /// 是否全部成功
    /// </summary>
    public bool AllSuccess => FailCount == 0;

    /// <summary>
    /// 创建批量操作结果
    /// </summary>
    public static BatchResult Create(int success, int fail, List<BatchFailureDetail>? failures = null) =>
        new()
        {
            SuccessCount = success,
            FailCount = fail,
            Failures = failures ?? []
        };
}

/// <summary>
/// 批量操作失败详情
/// </summary>
public sealed class BatchFailureDetail
{
    /// <summary>
    /// 失败项标识（ID 或 编码）
    /// </summary>
    public string? Identifier { get; set; }

    /// <summary>
    /// 失败原因
    /// </summary>
    public string Reason { get; set; } = string.Empty;
}

/// <summary>
/// 树形节点（含 ID 和 ParentId）
/// </summary>
public sealed class TreeNode
{
    /// <summary>
    /// 节点ID
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 父节点ID
    /// </summary>
    public long? ParentId { get; set; }

    /// <summary>
    /// 节点名称
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 子节点
    /// </summary>
    public List<TreeNode> Children { get; set; } = [];
}

/// <summary>
/// 泛型树形节点
/// </summary>
public sealed class TreeNode<T>
{
    /// <summary>
    /// 节点数据
    /// </summary>
    public required T Data { get; set; }

    /// <summary>
    /// 子节点
    /// </summary>
    public List<TreeNode<T>> Children { get; set; } = [];

    /// <summary>
    /// 是否为叶子节点
    /// </summary>
    [JsonIgnore]
    public bool IsLeaf => Children.Count == 0;
}
