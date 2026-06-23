namespace Ss.CmbXft.Domain.Entities.Sserp.Product;

/// <summary>
/// 商品分组表实体（连接外部SQL Server数据库，非CodeFirst）
/// 对应表：[Product].[T_ProductGroup]
/// 关联：部门、类别、子类别、细分市场、商品分类
/// </summary>
public class SserpProductGroup
{
    /// <summary>
    /// 商品分组ID（主键，自增）
    /// </summary>
    public int ProductGroupID { get; set; }

    /// <summary>
    /// 部门ID（关联 T_Department）
    /// </summary>
    public int? DepartmentID { get; set; }

    /// <summary>
    /// 类别ID（关联 T_Category）
    /// </summary>
    public int? CategoryID { get; set; }

    /// <summary>
    /// 子类别ID（关联 T_SubCategory）
    /// </summary>
    public int? SubCategoryID { get; set; }

    /// <summary>
    /// 细分市场ID（关联 T_Segment）
    /// </summary>
    public int? SegmentID { get; set; }

    /// <summary>
    /// 温度（冷链相关）
    /// </summary>
    public decimal? Temperature { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public short? Status { get; set; }

    /// <summary>
    /// 商品分类ID（关联 T_ItemClass）
    /// </summary>
    public int? ItemClassID { get; set; }

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
