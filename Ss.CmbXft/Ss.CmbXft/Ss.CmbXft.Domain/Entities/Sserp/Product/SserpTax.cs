namespace Ss.CmbXft.Domain.Entities.Sserp.Product;

/// <summary>
/// 税率表实体（连接外部SQL Server数据库，非CodeFirst）
/// 对应表：[dbo].[T_Tax]
/// </summary>
public class SserpTax
{
    /// <summary>
    /// 税ID（主键）
    /// </summary>
    public int TaxID { get; set; }

    /// <summary>
    /// 税名称
    /// </summary>
    public string? TaxName { get; set; }

    /// <summary>
    /// 税描述
    /// </summary>
    public string? TaxDescription { get; set; }

    /// <summary>
    /// 税率
    /// </summary>
    public decimal? TaxRate { get; set; }

    /// <summary>
    /// 生效开始日期
    /// </summary>
    public DateTime? ValidFrom { get; set; }

    /// <summary>
    /// 生效截止日期
    /// </summary>
    public DateTime? ValidTill { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public short? Status { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedOn { get; set; }

    /// <summary>
    /// 创建人ID
    /// </summary>
    public Guid CreatedBy { get; set; }

    /// <summary>
    /// 修改时间
    /// </summary>
    public DateTime? ModifiedOn { get; set; }

    /// <summary>
    /// 修改人ID
    /// </summary>
    public Guid? ModifiedBy { get; set; }
}
