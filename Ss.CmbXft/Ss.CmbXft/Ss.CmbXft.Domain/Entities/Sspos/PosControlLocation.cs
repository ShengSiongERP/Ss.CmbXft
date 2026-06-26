namespace Ss.CmbXft.Domain.Entities.Sspos;

/// <summary>
/// 控制门店表实体（连接外部SQL Server数据库，非CodeFirst）
/// 对应表：[dbo].[POS_Mst_ControlLocation]
/// </summary>
public class PosControlLocation
{
    /// <summary>
    /// 连接编码（主键）
    /// </summary>
    public string LinkedCode { get; set; } = string.Empty;

    /// <summary>
    /// 门店编码
    /// </summary>
    public string? LocationCode { get; set; }

    /// <summary>
    /// 是否激活
    /// </summary>
    public bool? Active { get; set; }

    /// <summary>
    /// 创建日期
    /// </summary>
    public DateTime CreateDate { get; set; }

    /// <summary>
    /// 创建人
    /// </summary>
    public string CreateUser { get; set; } = string.Empty;

    /// <summary>
    /// 修改日期
    /// </summary>
    public DateTime? ModifyDate { get; set; }

    /// <summary>
    /// 修改人
    /// </summary>
    public string? ModifyUser { get; set; }
}
