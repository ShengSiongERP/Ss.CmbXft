using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Ss.CmbXft.Common.Models;
using Ss.CmbXft.Domain.Base;
using Ss.CmbXft.Domain.Repositories;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace Ss.CmbXft.Infrastructure.EntityFrameworkCore.Repository;

/// <summary>
/// 通用仓储泛型基类，聚合所有公共 CRUD / 分页 / 事务逻辑。
/// 子类只需指定具体的 DbContext 类型即可。
/// </summary>
public abstract class RepositoryBase<TEntity, TKey, TDbContext> : IRepository<TEntity, TKey>
    where TEntity : class
    where TDbContext : DbContext
{
    protected readonly TDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    protected RepositoryBase(TDbContext dbContext)
    {
        _context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _dbSet = dbContext.Set<TEntity>();
    }

    #region 查询

    public virtual async Task<TEntity?> GetByIdAsync(TKey id, bool isTracking = false, bool ignoreFilter = false, CancellationToken cancellationToken = default)
    {
        return await GetQueryable(isTracking, ignoreFilter).FirstOrDefaultAsync(e => EF.Property<TKey>(e, nameof(IEntity<TKey>.Id))!.Equals(id), cancellationToken);
    }

    public virtual async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool isTracking = false, bool ignoreFilter = false, CancellationToken cancellationToken = default)
    {
        return await GetQueryable(isTracking, ignoreFilter).FirstOrDefaultAsync(predicate, cancellationToken);
    }

    /// <summary>
    /// 获取DTO（根据ID，支持selector投影，提高性能）
    /// </summary>
    public virtual async Task<TResult?> GetByIdAsync<TResult>(TKey id, Expression<Func<TEntity, TResult>> selector, bool ignoreFilter = false, CancellationToken cancellationToken = default)
    {
        return await GetQueryable(false, ignoreFilter).Where(e => EF.Property<TKey>(e, nameof(IEntity<TKey>.Id))!.Equals(id)).Select(selector).FirstOrDefaultAsync(cancellationToken);
    }

    /// <summary>
    /// 获取DTO（根据条件，支持selector投影，提高性能）
    /// </summary>
    public virtual async Task<TResult?> FirstOrDefaultAsync<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> selector, bool ignoreFilter = false, CancellationToken cancellationToken = default)
    {
        return await GetQueryable(false, ignoreFilter).Where(predicate).Select(selector).FirstOrDefaultAsync(cancellationToken);
    }

    /// <summary>
    /// 获取列表（支持表达式排序）
    /// </summary>
    public virtual async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? predicate = null, string? sorting = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, bool isTracking = false, bool ignoreFilter = false, CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = GetQueryable(isTracking, ignoreFilter);
        if (predicate != null) query = query.Where(predicate);
        if (!string.IsNullOrWhiteSpace(sorting)) query = query.OrderBy(sorting);
        else if (orderBy != null) query = orderBy(query);
        return await query.ToListAsync(cancellationToken);
    }

    /// <summary>
    /// 获取DTO列表（支持selector投影，支持表达式排序，提高性能）
    /// </summary>
    public virtual async Task<List<TResult>> GetListAsync<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>>? predicate = null, string? sorting = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, bool ignoreFilter = false, CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = GetQueryable(false, ignoreFilter);
        if (predicate != null) query = query.Where(predicate);
        if (!string.IsNullOrWhiteSpace(sorting)) query = query.OrderBy(sorting);
        else if (orderBy != null) query = orderBy(query);
        return await query.Select(selector).ToListAsync(cancellationToken);
    }

    public virtual IQueryable<TEntity> GetQueryable(bool isTracking = false, bool ignoreFilter = false)
    {
        IQueryable<TEntity> query = _dbSet;
        if (ignoreFilter)
            query = query.IgnoreQueryFilters();
        if (!isTracking) query = query.AsNoTracking();
        return query;
    }
    #endregion

    #region 分页

    /// <summary>
    /// 分页查询（支持表达式排序）
    /// </summary>
    public virtual async Task<PageResult<TEntity>> GetPagedListAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        string? sorting = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        int pageIndex = 1,
        int pageSize = 20,
        bool isTracking = false,
        bool ignoreFilter = false,
        CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = GetQueryable(isTracking, ignoreFilter);
        if (predicate != null) query = query.Where(predicate);
        if (!string.IsNullOrWhiteSpace(sorting)) query = query.OrderBy(sorting);
        else if (orderBy != null) query = orderBy(query);

        long total = await query.LongCountAsync(cancellationToken);
        List<TEntity> items = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
        return new PageResult<TEntity>(total, pageIndex, pageSize, items);
    }

    /// <summary>
    /// 分页查询（支持selector投影，支持表达式排序，提高性能）
    /// </summary>
    public virtual async Task<PageResult<TResult>> GetPagedListAsync<TResult>(
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        string? sorting = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        int pageIndex = 1,
        int pageSize = 20,
        bool ignoreFilter = false,
        CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = GetQueryable(false, ignoreFilter);
        if (predicate != null) query = query.Where(predicate);
        if (!string.IsNullOrWhiteSpace(sorting)) query = query.OrderBy(sorting);
        else if (orderBy != null) query = orderBy(query);

        long total = await query.LongCountAsync(cancellationToken);
        List<TResult> items = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).Select(selector).ToListAsync(cancellationToken);
        return new PageResult<TResult>(total, pageIndex, pageSize, items);
    }
    #endregion

    #region 新增
    public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        => await _dbSet.AddAsync(entity, cancellationToken);

    public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        => await _dbSet.AddRangeAsync(entities, cancellationToken);
    #endregion

    #region 更新
    public virtual void Update(TEntity entity) => _dbSet.Update(entity);
    public virtual void Update(IEnumerable<TEntity> entities) => _dbSet.UpdateRange(entities);
    public virtual Task UpdateAsync(TEntity entity)
    {
        _ = _dbSet.Update(entity);
        return Task.CompletedTask;
    }
    public virtual Task UpdateRangeAsync(IEnumerable<TEntity> entities)
    {
        _dbSet.UpdateRange(entities);
        return Task.CompletedTask;
    }

    //await _roleRepository.UpdateAsync(predicate: u => true, updateAction: u => { u.Status = status; u.UpdateTime = DateTime.UtcNow; },);
    /// <summary>
    /// 根据条件批量更新（指定要更新的字段）
    /// </summary>
    public virtual async Task<int> UpdateAsync(Expression<Func<TEntity, bool>> predicate, Action<TEntity> updateAction, CancellationToken cancellationToken = default)
    {
        List<TEntity> entities = await _dbSet.Where(predicate).ToListAsync(cancellationToken);
        foreach (TEntity? entity in entities)
        {
            updateAction(entity);
        }
        _dbSet.UpdateRange(entities);
        return entities.Count;
    }

    //var role = new PermRole { Id = id, Status = status, UpdateTime = DateTime.UtcNow };
    //await _roleRepository.UpdatePropertiesAsync(entity: role, propertyExpressions: [u => u.Status, u => u.UpdateTime]);
    /// <summary>
    /// 更新实体 指定属性
    /// </summary>
    public virtual Task UpdateAsync(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions)
    {
        EntityEntry<TEntity> entry = _context.Entry(entity);
        entry.State = EntityState.Unchanged;
        foreach (Expression<Func<TEntity, object>> property in propertyExpressions)
        {
            entry.Property(property).IsModified = true;
        }
        return Task.CompletedTask;
    }
    //var list = new List<PermRole>() { new PermRole { Id = 1, Status = status, UpdateTime = DateTime.UtcNow }};
    //list, [u => u.Status, u => u.UpdateTime]
    /// <summary>
    /// 批量 更新实体 指定属性
    /// </summary>
    public virtual Task UpdateAsync(IEnumerable<TEntity> entities, params Expression<Func<TEntity, object>>[] propertyExpressions)
    {
        foreach (TEntity entity in entities)
        {
            EntityEntry<TEntity> entry = _context.Entry(entity);
            entry.State = EntityState.Unchanged;
            foreach (Expression<Func<TEntity, object>> property in propertyExpressions)
            {
                entry.Property(property).IsModified = true;
            }
        }
        return Task.CompletedTask;
    }
    //var i = await _roleRepository.UpdateAsync(b => true, s => { s.SetProperty(x => x.Status, status); s.SetProperty(x => x.UpdateTime, DateTime.UtcNow); }, ignoreFilter: true);
    /// <summary>
    /// 批量 sql 更新
    /// </summary>
    public virtual async Task<int> UpdateAsync(Expression<Func<TEntity, bool>> predicate, Action<IUpdateSetters<TEntity>> setPropertyCalls, bool ignoreFilter = false, CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = GetQueryable(true, ignoreFilter);
        return await query
            .Where(predicate)
            .ExecuteUpdateAsync(builder =>
            {
                UpdateSettersAdapter<TEntity> adapter = new(builder);
                setPropertyCalls(adapter);
            }, cancellationToken);
    }

    #endregion

    #region 删除
    /// <summary>
    /// 软删除实体（设置IsDeleted为true）
    /// </summary>
    public virtual void Delete(TEntity entity)
    {
        if (entity is ISoftDelete softDeletable)
        {
            softDeletable.IsDeleted = true;
            EntityEntry<TEntity> entry = _context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                _ = _dbSet.Attach(entity);
                entry = _context.Entry(entity);
            }

            entry.State = EntityState.Unchanged;
            entry.Property(nameof(ISoftDelete.IsDeleted)).IsModified = true;
        }
        else
        {
            _ = _dbSet.Remove(entity);
        }
    }

    /// <summary>
    /// 软删除实体集合（设置IsDeleted为true）
    /// </summary>
    public virtual void Delete(IEnumerable<TEntity> entities)
    {
        if (entities == null) throw new ArgumentNullException(nameof(entities));
        foreach (TEntity entity in entities)
        {
            Delete(entity);
        }
    }
    public virtual Task DeleteAsync(TEntity entity)
    {
        Delete(entity);
        return Task.CompletedTask;
    }

    public virtual Task DeleteAsync(IEnumerable<TEntity> entities)
    {
        if (entities == null) throw new ArgumentNullException(nameof(entities));
        foreach (TEntity entity in entities)
        {
            Delete(entity);
        }
        return Task.CompletedTask;
    }

    /// <summary>
    /// 根据ID软删除实体（设置IsDeleted为true）
    /// </summary>
    public virtual async Task DeleteAsync(TKey id, CancellationToken cancellationToken = default)
    {
        _ = await DeleteAsync(e => EF.Property<TKey>(e, nameof(IEntity<TKey>.Id))!.Equals(id), cancellationToken);
    }

    /// <summary>
    /// 根据条件批量软删除（设置IsDeleted为true）
    /// </summary>
    public virtual async Task<int> DeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        bool isSoftDeleteEntity = typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity));
        if (isSoftDeleteEntity)
        {
            return await _dbSet.Where(predicate).ExecuteUpdateAsync(set => set.SetProperty(e => ((ISoftDelete)e).IsDeleted, true), cancellationToken);
        }
        else
        {
            return await RemoveAsync(predicate, cancellationToken);
        }
    }

    /// <summary>
    /// 硬删除实体（从数据库中彻底移除）
    /// </summary>
    public virtual void Remove(TEntity entity) => _dbSet.Remove(entity);

    /// <summary>
    /// 硬删除实体集合（从数据库中彻底移除）
    /// </summary>
    public virtual void Remove(IEnumerable<TEntity> entities) => _dbSet.RemoveRange(entities);

    /// <summary>
    /// 硬删除实体（异步，从数据库中彻底移除）
    /// </summary>
    public virtual Task RemoveAsync(TEntity entity)
    {
        _ = _dbSet.Remove(entity);
        return Task.CompletedTask;
    }

    /// <summary>
    /// 硬删除实体集合（异步，从数据库中彻底移除）
    /// </summary>
    public virtual Task RemoveAsync(IEnumerable<TEntity> entities)
    {
        _dbSet.RemoveRange(entities);
        return Task.CompletedTask;
    }

    /// <summary>
    /// 根据ID硬删除实体（从数据库中彻底移除）
    /// </summary>
    public virtual async Task RemoveAsync(TKey id, CancellationToken cancellationToken = default)
    {
        _ = await _dbSet.Where(e => EF.Property<TKey>(e, nameof(IEntity<TKey>.Id))!.Equals(id)).IgnoreQueryFilters().ExecuteDeleteAsync(cancellationToken);
    }

    /// <summary>
    /// 根据条件批量硬删除（从数据库中彻底移除）
    /// </summary>
    public virtual async Task<int> RemoveAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        // 直接生成DELETE SQL语句，不加载实体到内存
        return await _dbSet.Where(predicate).IgnoreQueryFilters().ExecuteDeleteAsync(cancellationToken);
    }
    #endregion

    #region 计数
    public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null, bool ignoreFilter = false, CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = GetQueryable(false, ignoreFilter);
        if (predicate != null) query = query.Where(predicate);
        return await query.CountAsync(cancellationToken);
    }

    public virtual async Task<long> LongCountAsync(Expression<Func<TEntity, bool>>? predicate = null, bool ignoreFilter = false, CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = GetQueryable(false, ignoreFilter);
        if (predicate != null) query = query.Where(predicate);
        return await query.LongCountAsync(cancellationToken);
    }

    public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? predicate = null, bool ignoreFilter = false, CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = GetQueryable(false, ignoreFilter);
        if (predicate != null) query = query.Where(predicate);
        return await query.AnyAsync(cancellationToken);
    }

    public virtual async Task<bool> ExistsAsync(TKey id, bool ignoreFilter = false, CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = GetQueryable(false, ignoreFilter);
        return await query.AnyAsync(e => EF.Property<TKey>(e, nameof(IEntity<TKey>.Id))!.Equals(id), cancellationToken);
    }
    #endregion

    #region 事务
    public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
       => await _context.SaveChangesAsync(cancellationToken);
    #endregion
}
