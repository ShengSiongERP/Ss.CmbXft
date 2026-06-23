namespace Ss.CmbXft.Domain.Entities.Sserp.Product;

/// <summary>
/// 商品属性表实体（连接外部SQL Server数据库，非CodeFirst）
/// 对应表：[Product].[T_ProductAttribute]
/// </summary>
public class SserpProductAttribute
{
    /// <summary>
    /// 商品属性ID（主键，自增）
    /// </summary>
    public int ProductAttributeID { get; set; }

    /// <summary>
    /// 属性ID（关联 dbo.T_Attribute）
    /// </summary>
    public int? AttributeId { get; set; }

    /// <summary>
    /// 商品编码（关联 T_Product）
    /// </summary>
    public string? ProductCode { get; set; }

    /// <summary>
    /// 属性值
    /// </summary>
    public string? Value { get; set; }

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
