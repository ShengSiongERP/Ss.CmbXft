namespace Ss.CmbXft.Common.Models.Request;

/// <summary>
/// 分页能力标记接口：实现此接口的 DTO 可自动启用分页验证
/// </summary>
/// <remarks>
/// <para>配合 <see cref="Application.Validators.BaseValidator{T}.RuleForPagination"/> 使用。</para>
/// <para>业务层无需手动实现,直接继承 <see cref="PageRequestBase"/> 或实现此接口即可。</para>
/// </remarks>
public interface IPageable
{
    /// <summary>
    /// 当前页码（从 1 开始）
    /// </summary>
    int PageIndex { get; set; }

    /// <summary>
    /// 每页大小
    /// </summary>
    int PageSize { get; set; }
}
