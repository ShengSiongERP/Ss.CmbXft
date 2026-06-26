using System.ComponentModel.DataAnnotations;

namespace Ss.CmbXft.Common.Models.Request;

/// <summary>
/// 单 ID 输入（默认 long 类型）
/// </summary>
public sealed class IdReq
{
    /// <summary>
    /// 主键ID
    /// </summary>
    [Required(ErrorMessage = "ID不能为空")]
    public long Id { get; set; }
}

/// <summary>
/// 泛型单 ID 输入
/// </summary>
public sealed class IdReq<TKey>
{
    /// <summary>
    /// 主键ID
    /// </summary>
    [Required(ErrorMessage = "ID不能为空")]
    public required TKey Id { get; set; }
}

/// <summary>
/// 多 ID 输入
/// </summary>
public sealed class IdsReq
{
    /// <summary>
    /// ID列表
    /// </summary>
    [Required(ErrorMessage = "ID列表不能为空")]
    [MinLength(1, ErrorMessage = "至少需要一个ID")]
    public List<long> Ids { get; set; } = [];
}

/// <summary>
/// 泛型多 ID 输入
/// </summary>
public sealed class IdsReq<TKey>
{
    /// <summary>
    /// ID列表
    /// </summary>
    [Required(ErrorMessage = "ID列表不能为空")]
    [MinLength(1, ErrorMessage = "至少需要一个ID")]
    public required List<TKey> Ids { get; set; }
}

/// <summary>
/// 编码输入（用于按 Code/Number 查询或操作）
/// </summary>
public sealed class CodeReq
{
    /// <summary>
    /// 编码
    /// </summary>
    [Required(ErrorMessage = "编码不能为空")]
    public required string Code { get; set; }
}

/// <summary>
/// 多编码输入
/// </summary>
public sealed class CodesReq
{
    /// <summary>
    /// 编码列表
    /// </summary>
    [Required(ErrorMessage = "编码列表不能为空")]
    [MinLength(1, ErrorMessage = "至少需要一个编码")]
    public required List<string> Codes { get; set; }

    /// <summary>
    /// 从逗号分隔字符串解析
    /// </summary>
    public static CodesReq FromCommaSeparated(string csv) =>
        new()
        {
            Codes = csv.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToList()
        };
}

/// <summary>
/// 日期范围分页查询
/// </summary>
public sealed class DateRangeQuery : PageRequestBase
{
    /// <summary>
    /// 开始日期
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// 结束日期
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// 日期范围是否有效
    /// </summary>
    public bool HasDateRange => StartDate.HasValue && EndDate.HasValue;
}

/// <summary>
/// 状态变更输入
/// </summary>
public sealed class SetEnableReq
{
    /// <summary>
    /// 主键ID
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 目标状态值
    /// </summary>
    public required bool Enable{ get; set; }
}

/// <summary>
/// 泛型状态变更输入
/// </summary>
public sealed class StatusInput<TStatus>
{
    /// <summary>
    /// 主键ID
    /// </summary>
    [Required(ErrorMessage = "ID不能为空")]
    public long Id { get; set; }

    /// <summary>
    /// 目标状态值
    /// </summary>
    [Required(ErrorMessage = "状态值不能为空")]
    public required TStatus Status { get; set; }
}
