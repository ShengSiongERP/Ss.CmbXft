namespace Ss.CmbXft.Domain.Entities.Sserp.Product;

/// <summary>
/// 商品分类表实体（连接外部SQL Server数据库，非CodeFirst）
/// 对应表：[Product].[T_ItemClass]
/// </summary>
public class SserpItemClass
{
    /// <summary>
    /// 商品分类ID（主键，自增）
    /// </summary>
    public int ItemClassID { get; set; }

    /// <summary>
    /// 商品分类名称
    /// </summary>
    public string? ItemClassName { get; set; }

    /// <summary>
    /// 商品分类编码
    /// </summary>
    public string? ItemClassCode { get; set; }

    /// <summary>
    /// 税ID（关联 dbo.T_Tax）
    /// </summary>
    public int TaxID { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public short? Status { get; set; }

    /// <summary>
    /// NC部门编码
    /// </summary>
    public string? NCDepartmentCode { get; set; }

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
