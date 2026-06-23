namespace Ss.CmbXft.Domain.Entities.Sserp.Product;

/// <summary>
/// 商品条码表实体（连接外部SQL Server数据库，非CodeFirst）
/// 对应表：[Product].[T_Barcode]
/// </summary>
public class SserpBarcode
{
    /// <summary>
    /// 条码ID（主键，自增）
    /// </summary>
    public int BarcodeId { get; set; }

    /// <summary>
    /// 条码（唯一）
    /// </summary>
    public string Barcode { get; set; } = string.Empty;

    /// <summary>
    /// 商品编码（关联 T_Product）
    /// </summary>
    public string ProductCode { get; set; } = string.Empty;

    /// <summary>
    /// 计量单位ID（关联 T_UOM）
    /// </summary>
    public int UOMID { get; set; }

    /// <summary>
    /// 是否可订货（默认true）
    /// </summary>
    public bool IsOrder { get; set; } = true;

    /// <summary>
    /// 是否可销售（默认true）
    /// </summary>
    public bool IsSelling { get; set; } = true;

    /// <summary>
    /// 状态
    /// </summary>
    public short Status { get; set; }

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
