namespace Ss.CmbXft.Domain.Entities.Sspos;

/// <summary>
/// 优惠券信息表实体（连接外部SQL Server数据库，非CodeFirst）
/// 对应表：[dbo].[POS_Mst_Voucher]
/// </summary>
public class PosVoucher
{
    /// <summary>
    /// 优惠券编码（主键）
    /// </summary>
    public string VoucherCode { get; set; } = string.Empty;

    /// <summary>
    /// 优惠券名称
    /// </summary>
    public string VoucherName { get; set; } = string.Empty;

    /// <summary>
    /// 优惠券条码
    /// </summary>
    public string? VoucherBarcode { get; set; }

    /// <summary>
    /// 优惠券金额
    /// </summary>
    public decimal VoucherAmount { get; set; }

    /// <summary>
    /// 优惠券类型
    /// </summary>
    public string? VoucherType { get; set; }

    /// <summary>
    /// 供应商名称
    /// </summary>
    public string? VendorName { get; set; }

    /// <summary>
    /// 有效期开始
    /// </summary>
    public DateTime ValidFrom { get; set; }

    /// <summary>
    /// 有效期结束
    /// </summary>
    public DateTime ValidTo { get; set; }

    /// <summary>
    /// 最小购买金额
    /// </summary>
    public decimal? MinPurchaseAmount { get; set; }

    /// <summary>
    /// 每件商品最大数量
    /// </summary>
    public int? MaxQtyPerItem { get; set; }

    /// <summary>
    /// 优惠券图片
    /// </summary>
    public byte[]? VoucherImg { get; set; }

    /// <summary>
    /// 位置
    /// </summary>
    public short? Position { get; set; }

    /// <summary>
    /// 是否激活
    /// </summary>
    public bool Active { get; set; }

    /// <summary>
    /// 创建人
    /// </summary>
    public string CreateUser { get; set; } = string.Empty;

    /// <summary>
    /// 创建日期
    /// </summary>
    public DateTime CreateDate { get; set; }

    /// <summary>
    /// 修改人
    /// </summary>
    public string? ModifyUser { get; set; }

    /// <summary>
    /// 修改日期
    /// </summary>
    public DateTime? ModifyDate { get; set; }

    /// <summary>
    /// 每张小票最大数量
    /// </summary>
    public int? MaxQtyPerReceipt { get; set; }

    /// <summary>
    /// 兑换类型
    /// </summary>
    public int? RedeemType { get; set; }

    /// <summary>
    /// 需要数量
    /// </summary>
    public decimal? RequiredQty { get; set; }

    /// <summary>
    /// 需要数量
    /// </summary>
    public decimal? TotalQty { get; set; }

    /// <summary>
    /// 是否可作废
    /// </summary>
    public bool? IsVoidable { get; set; }
}
