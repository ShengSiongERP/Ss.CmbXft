namespace Ss.CmbXft.Domain.Entities.Sserp.Product;

/// <summary>
/// 计量单位表实体（连接外部SQL Server数据库，非CodeFirst）
/// 对应表：[Product].[T_UOM]
/// </summary>
public class SserpUom
{
    /// <summary>
    /// 计量单位ID（主键，自增）
    /// </summary>
    public int UOMID { get; set; }

    /// <summary>
    /// 单位名称
    /// </summary>
    public string? UOMName { get; set; }

    /// <summary>
    /// 单位描述
    /// </summary>
    public string? UOMDescription { get; set; }

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
