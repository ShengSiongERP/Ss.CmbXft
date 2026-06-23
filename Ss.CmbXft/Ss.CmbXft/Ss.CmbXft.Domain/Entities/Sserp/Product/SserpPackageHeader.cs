namespace Ss.CmbXft.Domain.Entities.Sserp.Product;

/// <summary>
/// 包装头表实体（连接外部SQL Server数据库，非CodeFirst）
/// 对应表：[Product].[T_PackageHeader]
/// </summary>
public class SserpPackageHeader
{
    /// <summary>
    /// 包包头ID（主键，自增）
    /// </summary>
    public int PackageHeaderID { get; set; }

    /// <summary>
    /// 商品编码（关联 T_Product）
    /// </summary>
    public string? ProductCode { get; set; }

    /// <summary>
    /// 有效期开始
    /// </summary>
    public DateTime? ValidFrom { get; set; }

    /// <summary>
    /// 有效期截止
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
