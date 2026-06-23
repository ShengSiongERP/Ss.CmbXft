namespace Ss.CmbXft.Domain.Entities.Sserp.Product;

/// <summary>
/// 品牌表实体（连接外部SQL Server数据库，非CodeFirst）
/// 对应表：[Product].[T_Brand]
/// </summary>
public class SserpBrand
{
    /// <summary>
    /// 品牌ID（主键，自增）
    /// </summary>
    public int BrandID { get; set; }

    /// <summary>
    /// 品牌名称
    /// </summary>
    public string? BrandName { get; set; }

    /// <summary>
    /// 品牌描述
    /// </summary>
    public string? BrandDescription { get; set; }

    /// <summary>
    /// 状态（默认0）
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
