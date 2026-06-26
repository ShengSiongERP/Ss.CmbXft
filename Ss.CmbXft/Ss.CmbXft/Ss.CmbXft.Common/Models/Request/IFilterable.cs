namespace Ss.CmbXft.Common.Models.Request;

/// <summary>
/// 筛选与排序能力标记接口：实现此接口的 DTO 可自动启用 Sorts/Filters 验证
/// </summary>
/// <remarks>
/// <para>配合 <see cref="Application.Validators.BaseValidator{T}.RuleForFilters(string[])"/> 与 <see cref="Application.Validators.BaseValidator{T}.RuleForSorts(string[])"/> 使用。</para>
/// <para>业务层可直接继承 <see cref="PageQueryRequestBase"/> 获得完整查询能力。</para>
/// </remarks>
public interface IFilterable
{
    /// <summary>
    /// 排序条件列表
    /// </summary>
    List<SortItem>? Sorts { get; set; }

    /// <summary>
    /// 筛选条件列表
    /// </summary>
    List<FilterItem>? Filters { get; set; }
}
