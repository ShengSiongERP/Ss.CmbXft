namespace Ss.CmbXft.Domain.Entities.Sserp.Product;

/// <summary>
/// 部门表实体（连接外部SQL Server数据库，非CodeFirst）
/// 对应表：[Product].[T_Department]
/// </summary>
public class SserpDepartment
{
    /// <summary>
    /// 部门ID（主键，自增）
    /// </summary>
    public int DepartmentID { get; set; }

    /// <summary>
    /// 部门名称
    /// </summary>
    public string? DepartmentName { get; set; }

    /// <summary>
    /// 部门描述
    /// </summary>
    public string? DepartmentDescription { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public short? Status { get; set; }

    /// <summary>
    /// 部门编码
    /// </summary>
    public string? DepartmentCode { get; set; }

    /// <summary>
    /// NC部门编码
    /// </summary>
    public string? NCDepartmentCode { get; set; }

    /// <summary>
    /// NC部门名称
    /// </summary>
    public string? NCDepartmentName { get; set; }

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
