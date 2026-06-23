using Ss.CmbXft.Common.Models;

namespace Ss.CmbXft.Application.Dtos.Sserp.Product;

/// <summary>
/// 商品同步查询条件 DTO
/// </summary>
public class ProductSyncQueryDto : PagedRequestBase
{
    /// <summary>
    /// 商品编码（精确匹配，多个用逗号分隔）
    /// </summary>
    public string? ProductCode { get; set; }

    /// <summary>
    /// 商品描述（模糊搜索）
    /// </summary>
    public string? ProductDescription { get; set; }

    /// <summary>
    /// 部门名称（模糊搜索）
    /// </summary>
    public string? Department { get; set; }

    /// <summary>
    /// 类别名称（模糊搜索）
    /// </summary>
    public string? Category { get; set; }

    /// <summary>
    /// 子类别名称（模糊搜索）
    /// </summary>
    public string? SubCategory { get; set; }

    /// <summary>
    /// 细分市场名称（模糊搜索）
    /// </summary>
    public string? Segment { get; set; }

    /// <summary>
    /// 商品分类名称（模糊搜索）
    /// </summary>
    public string? ItemClass { get; set; }

    /// <summary>
    /// 品牌名称（模糊搜索）
    /// </summary>
    public string? Brand { get; set; }

    /// <summary>
    /// 商品状态筛选（默认只同步启用状态 Status=1 的商品）
    /// </summary>
    public short? Status { get; set; } = 1;

    /// <summary>
    /// 修改时间起始（用于增量同步，只同步该时间之后修改的商品）
    /// </summary>
    public DateTime? ModifiedSince { get; set; }

    /// <summary>
    /// 最大同步数量，不传则不限制。例如传20则最多同步20条
    /// </summary>
    public int? MaxCount { get; set; }
}

/// <summary>
/// 商品同步结果 DTO
/// </summary>
public class ProductSyncResultDto
{
    /// <summary>
    /// 是否同步成功
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// 结果消息
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// 同步的商品数量
    /// </summary>
    public int SyncCount { get; set; }

    /// <summary>
    /// 会员系统返回的响应码
    /// </summary>
    public int? ResponseCode { get; set; }

    /// <summary>
    /// 会员系统返回的消息
    /// </summary>
    public string? ResponseMessage { get; set; }
}

/// <summary>
/// 商品同步预览 DTO（用于在同步前查看将要同步的数据）
/// </summary>
public class ProductSyncPreviewDto
{
    /// <summary>
    /// 商品编码
    /// </summary>
    public string ProductCode { get; set; } = string.Empty;

    /// <summary>
    /// 商品描述
    /// </summary>
    public string ProductDescription { get; set; } = string.Empty;

    /// <summary>
    /// 部门名称
    /// </summary>
    public string? Department { get; set; }

    /// <summary>
    /// 类别名称
    /// </summary>
    public string? Category { get; set; }

    /// <summary>
    /// 子类别名称
    /// </summary>
    public string? SubCategory { get; set; }

    /// <summary>
    /// 细分市场名称
    /// </summary>
    public string? Segment { get; set; }

    /// <summary>
    /// 商品分类名称
    /// </summary>
    public string? ItemClass { get; set; }

    /// <summary>
    /// 品牌名称
    /// </summary>
    public string? Brand { get; set; }

    /// <summary>
    /// 默认单位
    /// </summary>
    public string? Uom { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public short? Status { get; set; }

    /// <summary>
    /// 条码数量
    /// </summary>
    public int BarcodeCount { get; set; }

    /// <summary>
    /// 税率
    /// </summary>
    public decimal? TaxRate { get; set; }

    /// <summary>
    /// 最后修改时间
    /// </summary>
    public DateTime? ModifiedOn { get; set; }
}

/// <summary>
/// 商品分页查询 DTO（用于浏览商品列表）
/// </summary>
public class ProductQueryDto : PagedRequestBase
{
    /// <summary>
    /// 商品编码（模糊搜索）
    /// </summary>
    public string? ProductCode { get; set; }

    /// <summary>
    /// 商品描述（模糊搜索）
    /// </summary>
    public string? ProductDescription { get; set; }

    /// <summary>
    /// 品牌ID筛选
    /// </summary>
    public int? BrandID { get; set; }

    /// <summary>
    /// 商品分组ID筛选
    /// </summary>
    public int? ProductGroupID { get; set; }

    /// <summary>
    /// 状态筛选
    /// </summary>
    public short? Status { get; set; }
}

/// <summary>
/// 商品列表响应 DTO
/// </summary>
public class ProductListDto
{
    /// <summary>
    /// 商品编码
    /// </summary>
    public string ProductCode { get; set; } = string.Empty;

    /// <summary>
    /// 商品描述1
    /// </summary>
    public string? ProductDescription1 { get; set; }

    /// <summary>
    /// 商品描述2
    /// </summary>
    public string? ProductDescription2 { get; set; }

    /// <summary>
    /// 品牌名称
    /// </summary>
    public string? BrandName { get; set; }

    /// <summary>
    /// 默认单位名称
    /// </summary>
    public string? DefaultUomName { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public short? Status { get; set; }

    /// <summary>
    /// 商品分组ID
    /// </summary>
    public int? ProductGroupID { get; set; }

    /// <summary>
    /// 部门名称
    /// </summary>
    public string? DepartmentName { get; set; }

    /// <summary>
    /// 类别名称
    /// </summary>
    public string? CategoryName { get; set; }

    /// <summary>
    /// 子类别名称
    /// </summary>
    public string? SubCategoryName { get; set; }

    /// <summary>
    /// 细分市场名称
    /// </summary>
    public string? SegmentName { get; set; }

    /// <summary>
    /// 商品分类名称
    /// </summary>
    public string? ItemClassName { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedOn { get; set; }

    /// <summary>
    /// 修改时间
    /// </summary>
    public DateTime? ModifiedOn { get; set; }
}
