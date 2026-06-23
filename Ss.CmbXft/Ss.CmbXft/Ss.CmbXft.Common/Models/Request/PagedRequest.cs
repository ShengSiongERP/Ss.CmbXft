namespace Ss.CmbXft.Common.Models;

/// <summary>
/// 分页请求基类
/// </summary>
public abstract class PagedRequestBase : BaseRequest
{
    private const int MaxPageSize = 1000;
    private const int DefaultPageSize = 20;
    private const int DefaultPageIndex = 1;

    /// <summary>
    /// 当前页码（从1开始）
    /// </summary>
    public int PageIndex { get; set; } = DefaultPageIndex;

    /// <summary>
    /// 每页大小
    /// </summary>
    public int PageSize { get; set; } = DefaultPageSize;

    /// <summary>
    /// 排序条件
    /// </summary>
    public List<SortItem>? Sorts { get; set; }

    /// <summary>
    /// 筛选条件
    /// </summary>
    public List<FilterItem>? Filters { get; set; }

    /// <summary>
    /// 获取跳过的记录数
    /// </summary>
    public int Skip => (PageIndex - 1) * PageSize;

    /// <summary>
    /// 获取获取的记录数
    /// </summary>
    public int Take => PageSize;

    /// <summary>
    /// 获取排序字符串（用于数据库查询）
    /// </summary>
    public string? GetSortString()
    {
        if (Sorts == null || Sorts.Count == 0)
            return null;

        return string.Join(", ", Sorts.Select(s => s.ToString()));
    }
}
