namespace Ss.CmbXft.Domain.Entities.Sserp.Product;

/// <summary>
/// 商品类别表实体（连接外部SQL Server数据库，非CodeFirst）
/// 对应表：[Product].[T_Category]
/// </summary>
public class SserpCategory
{
    /// <summary>
    /// 类别ID（主键，自增）
    /// </summary>
    public int CategoryID { get; set; }

    /// <summary>
    /// 类别名称
    /// </summary>
    public string? CategoryName { get; set; }

    /// <summary>
    /// 类别描述
    /// </summary>
    public string? CategoryDescription { get; set; }

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
