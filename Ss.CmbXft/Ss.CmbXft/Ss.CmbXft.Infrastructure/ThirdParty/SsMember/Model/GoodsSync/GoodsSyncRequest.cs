using Newtonsoft.Json;

namespace Ss.CmbXft.Infrastructure.ThirdParty.SsMember.Model.GoodsSync;

/// <summary>
/// 商品条码信息
/// </summary>
public class GoodsBarcode
{
    /// <summary>
    /// 条码
    /// </summary>
    [JsonProperty("barcode")]
    public string Barcode { get; set; } = string.Empty;

    /// <summary>
    /// 单位（与uomName二选一）
    /// </summary>
    [JsonProperty("uom")]
    public string? Uom { get; set; }

    /// <summary>
    /// 单位名称（与uom二选一）
    /// </summary>
    [JsonProperty("uomName")]
    public string? UomName { get; set; }
}

/// <summary>
/// 商品信息（用于同步到会员系统）
/// </summary>
public class GoodsInfo
{
    /// <summary>
    /// 商品编码（必填）
    /// </summary>
    [JsonProperty("productCode")]
    public string ProductCode { get; set; } = string.Empty;

    /// <summary>
    /// 商品描述（必填）
    /// </summary>
    [JsonProperty("productDescription")]
    public string ProductDescription { get; set; } = string.Empty;

    /// <summary>
    /// 部门（可选）
    /// </summary>
    [JsonProperty("department")]
    public string? Department { get; set; }

    /// <summary>
    /// 子类别（可选）
    /// </summary>
    [JsonProperty("subCategory")]
    public string? SubCategory { get; set; }

    /// <summary>
    /// 类别（可选）
    /// </summary>
    [JsonProperty("category")]
    public string? Category { get; set; }

    /// <summary>
    /// 细分市场（可选）
    /// </summary>
    [JsonProperty("segment")]
    public string? Segment { get; set; }

    /// <summary>
    /// 商品分类（可选）
    /// </summary>
    [JsonProperty("itemClass")]
    public string? ItemClass { get; set; }

    /// <summary>
    /// 品牌（可选）
    /// </summary>
    [JsonProperty("brand")]
    public string? Brand { get; set; }

    /// <summary>
    /// 单位（可选）
    /// </summary>
    [JsonProperty("uom")]
    public string? Uom { get; set; }

    /// <summary>
    /// 状态（1=启用，可传string或int）
    /// </summary>
    [JsonProperty("status")]
    public object? Status { get; set; }

    /// <summary>
    /// 条码列表（可选）
    /// </summary>
    [JsonProperty("barcodes")]
    public List<GoodsBarcode>? Barcodes { get; set; }

    /// <summary>
    /// 税率（可传string或decimal，如"0.13"或0.13）
    /// </summary>
    [JsonProperty("taxRate")]
    public object? TaxRate { get; set; }

    /// <summary>
    /// 税码（可选）
    /// </summary>
    [JsonProperty("taxCode")]
    public string? TaxCode { get; set; }
}

/// <summary>
/// 商品信息同步请求
/// 接口URL: https://member.shengsiongcn.com/api/erp/goods_sync
/// </summary>
public class GoodsSyncRequest
{
    /// <summary>
    /// 当前时间戳（10位）
    /// </summary>
    [JsonProperty("current")]
    public long Current { get; set; }

    /// <summary>
    /// 签名
    /// </summary>
    [JsonProperty("sign")]
    public string Sign { get; set; } = string.Empty;

    /// <summary>
    /// 商品列表
    /// </summary>
    [JsonProperty("products")]
    public List<GoodsInfo> Products { get; set; } = new();
}
