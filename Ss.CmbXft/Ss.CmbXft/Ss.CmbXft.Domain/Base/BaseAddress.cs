using System.ComponentModel.DataAnnotations;

namespace Ss.CmbXft.Domain.Base;

/// <summary>
/// 基础地址信息
/// </summary>
public abstract class BaseAddress
{
    /// <summary>
    /// 姓名
    /// </summary>
    [MaxLength(30)]
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// 手机号码
    /// </summary>
    [MaxLength(20)]
    public string? Phone { get; set; }

    /// <summary>
    /// 区域编号
    /// </summary>
    [MaxLength(20)]
    public string? AreaId { get; set; }

    /// <summary>
    /// 详细地址
    /// </summary>
    [MaxLength(255)]
    public string? Address { get; set; }

    /// <summary>
    /// 标签（如：家、公司等）
    /// </summary>
    [MaxLength(20)]
    public string? Tag { get; set; }

    /// <summary>
    /// 是否默认地址
    /// </summary>
    public bool IsDefault { get; set; }
}
