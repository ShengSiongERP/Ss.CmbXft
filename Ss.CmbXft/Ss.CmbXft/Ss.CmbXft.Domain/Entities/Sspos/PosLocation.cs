namespace Ss.CmbXft.Domain.Entities.Sspos;

/// <summary>
/// 门店信息表实体（连接外部SQL Server数据库，非CodeFirst）
/// 对应表：[dbo].[POS_MLocation]
/// </summary>
public class PosLocation
{
    /// <summary>
    /// 门店编码（主键）
    /// </summary>
    public string LocationCode { get; set; } = string.Empty;

    /// <summary>
    /// 门店名称
    /// </summary>
    public string LocationName { get; set; } = string.Empty;

    /// <summary>
    /// 工资门店编码
    /// </summary>
    public string? PayrollLocationCode { get; set; }

    /// <summary>
    /// 地址1
    /// </summary>
    public string? Address1 { get; set; }

    /// <summary>
    /// 地址2
    /// </summary>
    public string? Address2 { get; set; }

    /// <summary>
    /// 地址3
    /// </summary>
    public string? Address3 { get; set; }

    /// <summary>
    /// 电话
    /// </summary>
    public string? TelePhone { get; set; }

    /// <summary>
    /// 传真号
    /// </summary>
    public string? FaxNo { get; set; }

    /// <summary>
    /// GST注册号
    /// </summary>
    public string? GSTRegNo { get; set; }

    /// <summary>
    /// 公司注册号
    /// </summary>
    public string? CoRegNo { get; set; }

    /// <summary>
    /// 是否仓库
    /// </summary>
    public bool WareHouse { get; set; }

    /// <summary>
    /// GST税率
    /// </summary>
    public decimal? GST { get; set; }

    /// <summary>
    /// GST百分比
    /// </summary>
    public decimal? GSTPer { get; set; }

    /// <summary>
    /// 负责人
    /// </summary>
    public string? PersonIncharge { get; set; }

    /// <summary>
    /// 短代码
    /// </summary>
    public string? ShortCode { get; set; }

    /// <summary>
    /// 日结日期
    /// </summary>
    public DateTime? EODDate { get; set; }

    /// <summary>
    /// 备份天数
    /// </summary>
    public short? BackupDays { get; set; }

    /// <summary>
    /// 创建人
    /// </summary>
    public string CreateUser { get; set; } = string.Empty;

    /// <summary>
    /// 创建日期
    /// </summary>
    public DateTime? CreateDate { get; set; }

    /// <summary>
    /// 修改人
    /// </summary>
    public string? ModifyUser { get; set; }

    /// <summary>
    /// 修改日期
    /// </summary>
    public DateTime? ModifyDate { get; set; }

    /// <summary>
    /// 班次1
    /// </summary>
    public string? Shift1 { get; set; }

    /// <summary>
    /// 班次2
    /// </summary>
    public string? Shift2 { get; set; }

    /// <summary>
    /// 班次3
    /// </summary>
    public string? Shift3 { get; set; }

    /// <summary>
    /// 班次4
    /// </summary>
    public string? Shift4 { get; set; }

    /// <summary>
    /// 是否数字
    /// </summary>
    public bool? ISDigi { get; set; }

    /// <summary>
    /// 是否激活
    /// </summary>
    public bool Active { get; set; }

    /// <summary>
    /// 业务日期
    /// </summary>
    public DateTime? BusinessDate { get; set; }

    /// <summary>
    /// 上次班次关闭
    /// </summary>
    public bool? LastShiftClose { get; set; }

    /// <summary>
    /// 投入金额
    /// </summary>
    public decimal? PayInAmount { get; set; }
}
