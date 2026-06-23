using Newtonsoft.Json;

namespace Ss.CmbXft.Domain.ThirdParty.SsMember;

/// <summary>
/// 会员系统API通用响应
/// </summary>
public class SsMemberResponse
{
    /// <summary>
    /// 状态（success=成功）
    /// </summary>
    [JsonProperty("status")]
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// 响应码（200=成功）
    /// </summary>
    [JsonProperty("code")]
    public int Code { get; set; }

    /// <summary>
    /// 消息
    /// </summary>
    [JsonProperty("message")]
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// 错误信息
    /// </summary>
    [JsonProperty("error")]
    public object? Error { get; set; }

    /// <summary>
    /// 判断请求是否成功
    /// </summary>
    public bool IsSuccess => Status == "success" && Code == 200;

    /// <summary>
    /// 获取错误信息（如果失败）
    /// </summary>
    public string GetErrorMessage()
    {
        if (IsSuccess)
            return string.Empty;

        return !string.IsNullOrEmpty(Message) ? Message : $"未知错误 (Code: {Code})";
    }
}

/// <summary>
/// 会员系统API通用响应（带泛型数据）
/// </summary>
public class SsMemberResponse<T> : SsMemberResponse
{
    /// <summary>
    /// 响应数据（强类型）
    /// </summary>
    [JsonProperty("data")]
    public T? Data { get; set; }
}

//// ========== 门店相关 ==========

///// <summary>
///// 门店信息
///// </summary>
//public class SsStoreInfo
//{
//    /// <summary>
//    /// 门店唯一标识
//    /// </summary>
//    [JsonProperty("store_id")]
//    public string StoreId { get; set; } = string.Empty;

//    /// <summary>
//    /// 门店名称
//    /// </summary>
//    [JsonProperty("store_name")]
//    public string StoreName { get; set; } = string.Empty;

//    /// <summary>
//    /// 门店地址
//    /// </summary>
//    [JsonProperty("address")]
//    public string Address { get; set; } = string.Empty;

//    /// <summary>
//    /// 门店信息（可选扩展字段）
//    /// </summary>
//    [JsonProperty("data")]
//    public object? Data { get; set; }
//}

//// ========== 商品相关 ==========

///// <summary>
///// 商品条码信息
///// </summary>
//public class SsBarcodeInfo
//{
//    /// <summary>
//    /// 条码
//    /// </summary>
//    [JsonProperty("barcode")]
//    public string Barcode { get; set; } = string.Empty;

//    /// <summary>
//    /// 单位
//    /// </summary>
//    [JsonProperty("uom")]
//    public string? Uom { get; set; }

//    /// <summary>
//    /// 单位名称
//    /// </summary>
//    [JsonProperty("uomName")]
//    public string? UomName { get; set; }
//}

///// <summary>
///// 商品信息
///// </summary>
//public class SsProductInfo
//{
//    /// <summary>
//    /// 商品编码
//    /// </summary>
//    [JsonProperty("productCode")]
//    public string ProductCode { get; set; } = string.Empty;

//    /// <summary>
//    /// 商品描述
//    /// </summary>
//    [JsonProperty("productDescription")]
//    public string ProductDescription { get; set; } = string.Empty;

//    /// <summary>
//    /// 部门
//    /// </summary>
//    [JsonProperty("department")]
//    public string? Department { get; set; }

//    /// <summary>
//    /// 子类别
//    /// </summary>
//    [JsonProperty("subCategory")]
//    public string? SubCategory { get; set; }

//    /// <summary>
//    /// 类别
//    /// </summary>
//    [JsonProperty("category")]
//    public string? Category { get; set; }

//    /// <summary>
//    /// 细分市场
//    /// </summary>
//    [JsonProperty("segment")]
//    public string? Segment { get; set; }

//    /// <summary>
//    /// 商品分类
//    /// </summary>
//    [JsonProperty("itemClass")]
//    public string? ItemClass { get; set; }

//    /// <summary>
//    /// 品牌
//    /// </summary>
//    [JsonProperty("brand")]
//    public string? Brand { get; set; }

//    /// <summary>
//    /// 单位
//    /// </summary>
//    [JsonProperty("uom")]
//    public string? Uom { get; set; }

//    /// <summary>
//    /// 状态
//    /// </summary>
//    [JsonProperty("status")]
//    public object? Status { get; set; }

//    /// <summary>
//    /// 条码列表
//    /// </summary>
//    [JsonProperty("barcodes")]
//    public List<SsBarcodeInfo>? Barcodes { get; set; }

//    /// <summary>
//    /// 税率
//    /// </summary>
//    [JsonProperty("taxRate")]
//    public object? TaxRate { get; set; }

//    /// <summary>
//    /// 税码
//    /// </summary>
//    [JsonProperty("taxCode")]
//    public string? TaxCode { get; set; }
//}

//// ========== 优惠券相关 ==========

///// <summary>
///// 优惠券信息
///// </summary>
//public class SsCouponSyncInfo
//{
//    /// <summary>
//    /// ERP优惠券ID
//    /// </summary>
//    [JsonProperty("code_id")]
//    public int CodeId { get; set; }

//    /// <summary>
//    /// 优惠券数量
//    /// </summary>
//    [JsonProperty("count")]
//    public int Count { get; set; }

//    /// <summary>
//    /// 满多少
//    /// </summary>
//    [JsonProperty("enough")]
//    public string? Enough { get; set; }

//    /// <summary>
//    /// 减多少
//    /// </summary>
//    [JsonProperty("deduct")]
//    public string? Deduct { get; set; }

//    /// <summary>
//    /// 折扣
//    /// </summary>
//    [JsonProperty("discount")]
//    public int? Discount { get; set; }

//    /// <summary>
//    /// 可用门店
//    /// </summary>
//    [JsonProperty("store_id")]
//    public string? StoreId { get; set; }

//    /// <summary>
//    /// 代金多少
//    /// </summary>
//    [JsonProperty("money")]
//    public string? Money { get; set; }

//    /// <summary>
//    /// 优惠券说明
//    /// </summary>
//    [JsonProperty("remark")]
//    public string? Remark { get; set; }

//    /// <summary>
//    /// 有效期开始时间
//    /// </summary>
//    [JsonProperty("start_time")]
//    public string StartTime { get; set; } = string.Empty;

//    /// <summary>
//    /// 有效期结束时间
//    /// </summary>
//    [JsonProperty("finish_time")]
//    public string FinishTime { get; set; } = string.Empty;

//    /// <summary>
//    /// 有效期方式
//    /// </summary>
//    [JsonProperty("time_mode")]
//    public string TimeMode { get; set; } = "1";

//    /// <summary>
//    /// 领取几天内后过期
//    /// </summary>
//    [JsonProperty("receive_time")]
//    public string ReceiveTime { get; set; } = string.Empty;

//    /// <summary>
//    /// 优惠券类型
//    /// </summary>
//    [JsonProperty("coupon_type")]
//    public string? CouponType { get; set; }

//    /// <summary>
//    /// 券名称
//    /// </summary>
//    [JsonProperty("name")]
//    public string Name { get; set; } = string.Empty;

//    /// <summary>
//    /// 券图片
//    /// </summary>
//    [JsonProperty("img_src")]
//    public string ImgSrc { get; set; } = string.Empty;
//}

//// ========== 用户相关 ==========

