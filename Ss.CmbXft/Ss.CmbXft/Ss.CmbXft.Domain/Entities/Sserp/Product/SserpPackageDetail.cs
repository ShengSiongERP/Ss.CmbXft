namespace Ss.CmbXft.Domain.Entities.Sserp.Product;

/// <summary>
/// 包装明细表实体（连接外部SQL Server数据库，非CodeFirst）
/// 对应表：[Product].[T_PackageDetail]
/// </summary>
public class SserpPackageDetail
{
    /// <summary>
    /// 包装明细ID（主键，自增）
    /// </summary>
    public int PackageDetailID { get; set; }

    /// <summary>
    /// 包包头ID（关联 T_PackageHeader）
    /// </summary>
    public int? PackageHeaderID { get; set; }

    /// <summary>
    /// 商品编码（关联 T_Product）
    /// </summary>
    public string? ProductCode { get; set; }

    /// <summary>
    /// 数量
    /// </summary>
    public decimal? Quantity { get; set; }

    /// <summary>
    /// 零售价
    /// </summary>
    public decimal? RetailPrice { get; set; }

    /// <summary>
    /// 比例
    /// </summary>
    public decimal? Ratio { get; set; }

    /// <summary>
    /// 最低价
    /// </summary>
    public decimal? MinPrice { get; set; }

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
