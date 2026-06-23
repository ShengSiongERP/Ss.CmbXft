namespace Ss.CmbXft.Domain.Entities.Sserp.Product;

/// <summary>
/// 商品信息表实体（连接外部SQL Server数据库，非CodeFirst）
/// 对应表：[Product].[T_Product]
/// </summary>
public class SserpProduct
{
    /// <summary>
    /// 商品编码（主键）
    /// </summary>
    public string ProductCode { get; set; } = string.Empty;

    /// <summary>
    /// 商品描述1（主要描述）
    /// </summary>
    public string? ProductDescription1 { get; set; }

    /// <summary>
    /// 商品描述2
    /// </summary>
    public string? ProductDescription2 { get; set; }

    /// <summary>
    /// 商品描述3
    /// </summary>
    public string? ProductDescription3 { get; set; }

    /// <summary>
    /// 商品分组ID（关联 T_ProductGroup）
    /// </summary>
    public int? ProductGroupID { get; set; }

    /// <summary>
    /// 税ID（关联 dbo.T_Tax）
    /// </summary>
    public int? TaxID { get; set; }

    /// <summary>
    /// 品牌ID（关联 T_Brand）
    /// </summary>
    public int? BrandID { get; set; }

    /// <summary>
    /// 默认计量单位ID（关联 T_UOM）
    /// </summary>
    public int? DefaultUOMID { get; set; }

    /// <summary>
    /// 商品类型名称
    /// </summary>
    public string? ProductTypeName { get; set; }

    /// <summary>
    /// 净重（kg）
    /// </summary>
    public decimal? ProductNetWeight { get; set; }

    /// <summary>
    /// 毛重（kg）
    /// </summary>
    public decimal? ProductGrossWeight { get; set; }

    /// <summary>
    /// 长度（cm）
    /// </summary>
    public decimal? ProductLength { get; set; }

    /// <summary>
    /// 高度（cm）
    /// </summary>
    public decimal? ProductHeight { get; set; }

    /// <summary>
    /// 宽度（cm）
    /// </summary>
    public decimal? ProductWidth { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public short? Status { get; set; }

    /// <summary>
    /// 原产国
    /// </summary>
    public string? CountryOfOrigin { get; set; }

    /// <summary>
    /// 商品图片（二进制数据）
    /// </summary>
    public byte[]? ProductImage { get; set; }

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
