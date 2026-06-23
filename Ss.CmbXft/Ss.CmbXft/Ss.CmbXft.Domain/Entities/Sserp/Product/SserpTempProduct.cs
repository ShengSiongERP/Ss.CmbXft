namespace Ss.CmbXft.Domain.Entities.Sserp.Product;

/// <summary>
/// 临时商品表实体（连接外部SQL Server数据库，非CodeFirst）
/// 对应表：[Product].[T_TempProduct]
/// 用于商品同步的临时数据表
/// </summary>
public class SserpTempProduct
{
    /// <summary>
    /// 临时商品ID（主键，自增）
    /// </summary>
    public int TempProductID { get; set; }

    /// <summary>
    /// 箱码条码
    /// </summary>
    public string? CartonBarcode { get; set; }

    /// <summary>
    /// 条码
    /// </summary>
    public string Barcode { get; set; } = string.Empty;

    /// <summary>
    /// 商品描述
    /// </summary>
    public string ProductDescription { get; set; } = string.Empty;

    /// <summary>
    /// 品牌
    /// </summary>
    public string Brand { get; set; } = string.Empty;

    /// <summary>
    /// 原产国
    /// </summary>
    public string? CountryOfOrigin { get; set; }

    /// <summary>
    /// 商品类型
    /// </summary>
    public string? ProductType { get; set; }

    /// <summary>
    /// 单位
    /// </summary>
    public string UOM { get; set; } = string.Empty;

    /// <summary>
    /// 单位换算因子
    /// </summary>
    public decimal? UOMConversionFactor { get; set; }

    /// <summary>
    /// 箱毛重
    /// </summary>
    public decimal? ProductCartonGrossWeight { get; set; }

    /// <summary>
    /// 箱宽度
    /// </summary>
    public decimal? ProductCartonWidth { get; set; }

    /// <summary>
    /// 箱长度
    /// </summary>
    public decimal? ProductCartonLength { get; set; }

    /// <summary>
    /// 箱高度
    /// </summary>
    public decimal? ProductCartonHeight { get; set; }

    /// <summary>
    /// 净重
    /// </summary>
    public decimal? ProductNetWeight { get; set; }

    /// <summary>
    /// 毛重
    /// </summary>
    public decimal? ProductGrossWeight { get; set; }

    /// <summary>
    /// 宽度
    /// </summary>
    public decimal? ProductWidth { get; set; }

    /// <summary>
    /// 长度
    /// </summary>
    public decimal? ProductLength { get; set; }

    /// <summary>
    /// 高度
    /// </summary>
    public decimal? ProductHeight { get; set; }

    /// <summary>
    /// 箱成本（批发价）
    /// </summary>
    public decimal CartonCostWG { get; set; }

    /// <summary>
    /// 散装成本（批发价）
    /// </summary>
    public decimal LooseCostWG { get; set; }

    /// <summary>
    /// 建议零售价
    /// </summary>
    public decimal RSP { get; set; }

    /// <summary>
    /// 利润率
    /// </summary>
    public decimal Margin { get; set; }

    /// <summary>
    /// 税率
    /// </summary>
    public decimal TaxRate { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remarks { get; set; }

    /// <summary>
    /// 部门
    /// </summary>
    public string Department { get; set; } = string.Empty;

    /// <summary>
    /// 类别
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// 子类别
    /// </summary>
    public string SubCategory { get; set; } = string.Empty;

    /// <summary>
    /// 细分市场
    /// </summary>
    public string Segment { get; set; } = string.Empty;

    /// <summary>
    /// 商品分类
    /// </summary>
    public string ItemClass { get; set; } = string.Empty;

    /// <summary>
    /// 是否自动归零
    /// </summary>
    public bool IsAutoZero { get; set; }

    /// <summary>
    /// 是否已发送到秤
    /// </summary>
    public bool IsSentToScale { get; set; }

    /// <summary>
    /// 温度
    /// </summary>
    public decimal? Temperature { get; set; }

    /// <summary>
    /// 供应商
    /// </summary>
    public string? Vendor { get; set; }

    /// <summary>
    /// 税码
    /// </summary>
    public string? TaxCode { get; set; }

    /// <summary>
    /// 采购员
    /// </summary>
    public string? Buyer { get; set; }

    /// <summary>
    /// 保质期
    /// </summary>
    public string? ShelfLife { get; set; }

    /// <summary>
    /// 覆盖税
    /// </summary>
    public string? OverrideTax { get; set; }

    /// <summary>
    /// 皮重
    /// </summary>
    public decimal? TareWeight { get; set; }

    /// <summary>
    /// 商品图片（二进制数据）
    /// </summary>
    public byte[]? ProductImage { get; set; }

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
