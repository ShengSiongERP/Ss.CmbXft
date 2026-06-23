namespace Ss.CmbXft.Domain.Entities.Sserp.Product;

/// <summary>
/// 商品子类别表实体（连接外部SQL Server数据库，非CodeFirst）
/// 对应表：[Product].[T_SubCategory]
/// </summary>
public class SserpSubCategory
{
    /// <summary>
    /// 子类别ID（主键，自增）
    /// </summary>
    public int SubCategoryID { get; set; }

    /// <summary>
    /// 子类别名称
    /// </summary>
    public string? SubCategoryName { get; set; }

    /// <summary>
    /// 子类别描述
    /// </summary>
    public string? SubCategoryDescription { get; set; }

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
