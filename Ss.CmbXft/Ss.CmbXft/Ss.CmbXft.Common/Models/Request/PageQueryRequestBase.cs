namespace Ss.CmbXft.Common.Models.Request;

/// <summary>
/// 完整查询请求基类：组合分页能力（<see cref="PageRequestBase"/>）与筛选/排序能力（<see cref="IFilterable"/>）
/// </summary>
/// <remarks>
/// <para>这是最常用的查询基类,适用于 90% 的列表查询场景。</para>
/// <para>验证器使用示例:<br/></para>
/// <code>
/// public class MyQueryValidator : BaseValidator&lt;MyQuery&gt;
/// {
///     public MyQueryValidator()
///     {
///         RuleForPagination();           // 自动验证 PageIndex/PageSize
///         RuleForSorts("Id", "Code");    // 自动验证 Sorts 字段白名单
///         RuleForFilters("Id", "Code");  // 自动验证 Filters 字段白名单
///     }
/// }
/// </code>
/// </remarks>
public abstract class PageQueryRequestBase : PageRequestBase, IFilterable
{
    /// <summary>
    /// 排序条件列表
    /// </summary>
    public List<SortItem>? Sorts { get; set; }

    /// <summary>
    /// 筛选条件列表
    /// </summary>
    public List<FilterItem>? Filters { get; set; }
}
