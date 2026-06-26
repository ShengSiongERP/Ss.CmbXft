namespace Ss.CmbXft.Common.Models.Request;

/// <summary>
/// 分页请求基类：仅提供分页能力（PageIndex、PageSize、Skip、Take）
/// </summary>
/// <remarks>
/// <para>继承体系清晰化:<br/></para>
/// <list type="bullet">
/// <item>只分页 → 继承 <see cref="PageRequestBase"/></item>
/// <item>只筛选 → 继承 <see cref="QueryRequestBase"/></item>
/// <item>分页+筛选 → 继承 <see cref="PageQueryRequestBase"/></item>
/// <item>完全自定义 → 实现 <see cref="IPageable"/> 或 <see cref="IFilterable"/></item>
/// </list>
/// </remarks>
public abstract class PageRequestBase : BaseRequest, IPageable
{
    //private const int MaxPageSize = 1000;
    private const int DefaultPageSize = 20;
    private const int DefaultPageIndex = 1;

    /// <summary>
    /// 当前页码（从 1 开始）
    /// </summary>
    public int PageIndex { get; set; } = DefaultPageIndex;

    /// <summary>
    /// 每页大小
    /// </summary>
    public int PageSize { get; set; } = DefaultPageSize;

    /// <summary>
    /// 跳过的记录数 = (PageIndex - 1) * PageSize
    /// </summary>
    public int Skip => (PageIndex - 1) * PageSize;

    /// <summary>
    /// 获取的记录数 = PageSize
    /// </summary>
    public int Take => PageSize;
}
