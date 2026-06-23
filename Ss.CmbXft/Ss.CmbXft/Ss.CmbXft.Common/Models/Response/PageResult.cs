using System.Text.Json.Serialization;

namespace Ss.CmbXft.Common.Models;

/// <summary>
/// 统一分页返回体
/// </summary>
public class PageResult<T>
{
    /// <summary>
    /// 数据列表
    /// </summary>
    [JsonPropertyName("items")]
    public IReadOnlyList<T> Items { get; set; } = Array.Empty<T>();

    /// <summary>
    /// 总条数
    /// </summary>
    [JsonPropertyName("totalCount")]
    public long TotalCount { get; set; }

    /// <summary>
    /// 当前页码
    /// </summary>
    [JsonPropertyName("pageIndex")]
    public int PageIndex { get; set; }

    /// <summary>
    /// 每页条数
    /// </summary>
    [JsonPropertyName("pageSize")]
    public int PageSize { get; set; }

    /// <summary>
    /// 总页数
    /// </summary>
    [JsonPropertyName("totalPages")]
    public long TotalPages => TotalCount / PageSize + (TotalCount % PageSize > 0 ? 1 : 0);

    /// <summary>
    /// 是否有上一页
    /// </summary>
    [JsonPropertyName("hasPrevious")]
    public bool HasPrevious => PageIndex > 1;

    /// <summary>
    /// 是否有下一页
    /// </summary>
    [JsonPropertyName("hasNext")]
    public bool HasNext => PageIndex < TotalPages;

    /// <summary>
    /// 构造函数
    /// </summary>
    public PageResult(long totalCount, int pageIndex, int pageSize, IReadOnlyList<T> items)
    {
        TotalCount = totalCount;
        PageIndex = pageIndex;
        PageSize = pageSize;
        Items = items;
    }

    /// <summary>
    /// 创建分页结果
    /// </summary>
    public static PageResult<T> Create(long totalCount, int pageIndex, int pageSize, IReadOnlyList<T> items)
    {
        return new PageResult<T>(totalCount, pageIndex, pageSize, items);
    }
}
