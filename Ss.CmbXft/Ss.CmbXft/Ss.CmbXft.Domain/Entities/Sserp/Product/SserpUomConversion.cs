namespace Ss.CmbXft.Domain.Entities.Sserp.Product;

/// <summary>
/// 单位换算表实体（连接外部SQL Server数据库，非CodeFirst）
/// 对应表：[Product].[T_UOMConversion]
/// </summary>
public class SserpUomConversion
{
    /// <summary>
    /// 单位换算ID（主键，自增）
    /// </summary>
    public int UOMConversionID { get; set; }

    /// <summary>
    /// 商品编码（关联 T_Product）
    /// </summary>
    public string? ProductCode { get; set; }

    /// <summary>
    /// 计量单位ID（关联 T_UOM）
    /// </summary>
    public int? UOMID { get; set; }

    /// <summary>
    /// 换算因子
    /// </summary>
    public decimal? ConversionFactor { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public short? Status { get; set; }

    /// <summary>
    /// 是否为库存单位（默认false）
    /// </summary>
    public bool IsStock { get; set; }

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
