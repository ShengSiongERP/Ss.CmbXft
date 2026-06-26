namespace Ss.CmbXft.Common.Models.Request;

/// <summary>
/// 筛选与排序请求基类：提供 Sorts 与 Filters 集合
/// </summary>
/// <remarks>
/// <para>适用于需要筛选/排序但不需要分页的场景（如列表导出、统计查询）。</para>
/// <para>如需分页+筛选,请继承 <see cref="PageRequestBase"/>。</para>
/// </remarks>
public abstract class QueryRequestBase : BaseRequest, IFilterable
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
