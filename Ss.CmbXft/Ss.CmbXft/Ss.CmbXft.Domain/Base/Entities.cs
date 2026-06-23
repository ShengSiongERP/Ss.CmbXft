using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ss.CmbXft.Domain.Base;
// 基础实体
public interface IEntity<TKey>
{
    TKey Id { get; set; }
}
// 创建审计
public interface IHasCreate
{
    DateTime CreateTime { get; set; }
    long CreateId { get; set; }
}
// 修改审计
public interface IHasUpdate
{
    DateTime? UpdateTime { get; set; }
    long UpdateId { get; set; }
}
// 软删除
public interface ISoftDelete
{
    bool IsDeleted { get; set; }
}
// 审核状态
public interface IHasAudit
{
    int Audit { get; set; }
    long AuditId { get; set; }
    DateTime? AuditTime { get; set; }
}
// 租户
public interface IHasTenantId
{
    /// <summary>
    /// 租户Id
    /// </summary>
    long? TenantId { get; set; }
}
// 机构
public interface IHasOrgId
{
    /// <summary>
    /// 机构Id
    /// </summary>
    long OrgId { get; set; }
}

/// <summary>
/// 机构实体基类（数据权限、删除标志）
/// </summary>
public abstract class OrgEntityBase<TKey> : AllEntityBase<TKey>, IHasOrgId
{
    /// <summary>
    /// 机构Id
    /// </summary>
    public virtual long OrgId { get; set; }
}
/// <summary>
/// 租户实体基类
/// </summary>
public abstract class TenantEntityBase<TKey> : OrgEntityBase<TKey>, IHasTenantId
{
    /// <summary>
    /// 租户Id
    /// </summary>
    public virtual long? TenantId { get; set; }
}


public abstract class EntityBase<TKey> : IEntity<TKey>
{
    public virtual TKey Id { get; set; } = default!;
}

public abstract class AuditEntityBase<TKey> : AllEntityBase<TKey>, IHasAudit
{
    /// <summary>
    /// 审核状态 0-待审，1-通过，2-草稿，100-审核不通过
    /// </summary>
    public virtual int Audit { get; set; }
    /// <summary>
    /// 审核用户
    /// </summary>
    public virtual long AuditId { get; set; } = 0;
    /// <summary>
    /// 审核时间
    /// </summary>
    public virtual DateTime? AuditTime { get; set; }
}
public abstract class AllEntityBase<TKey> : CreateEntityBase<TKey>, IHasUpdate
{
    public virtual DateTime? UpdateTime { get; set; }
    public virtual long UpdateId { get; set; } = 0;
}

public abstract class CreateEntityBase<TKey> : DeleteEntityBase<TKey>, IHasCreate
{
    public virtual DateTime CreateTime { get; set; } = DateTime.UtcNow;
    public virtual long CreateId { get; set; } = 0;
}

public abstract class DeleteEntityBase<TKey> : EntityBase<TKey>, ISoftDelete
{
    public virtual bool IsDeleted { get; set; }
}
