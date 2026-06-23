namespace Ss.CmbXft.Domain.Entities.Sserp.Product;

/// <summary>
/// 商品属性门店关联表实体（连接外部SQL Server数据库，非CodeFirst）
/// 对应表：[Product].[T_ProductAttributeBranch]
/// </summary>
public class SserpProductAttributeBranch
{
    /// <summary>
    /// 商品属性门店关联ID（主键，自增）
    /// </summary>
    public int ProductAttributeBranchID { get; set; }

    /// <summary>
    /// 商品属性ID（关联 T_ProductAttribute）
    /// </summary>
    public int? ProductAttributeId { get; set; }

    /// <summary>
    /// 门店ID（关联 dbo.T_Branch）
    /// </summary>
    public int? BranchId { get; set; }

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
