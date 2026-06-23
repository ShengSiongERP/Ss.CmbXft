using Microsoft.EntityFrameworkCore;
using Ss.CmbXft.Common.Models;
using Ss.CmbXft.Domain.Repositories;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace Ss.CmbXft.Infrastructure.EntityFrameworkCore;

/// <summary>
/// 基于备用数据库（SecondaryDbContext）的通用仓储实现
/// </summary>
public class SserpRepository<TEntity, TKey> : ISserpRepository<TEntity, TKey> where TEntity : class
{
    protected readonly SserpDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public SserpRepository(SserpDbContext dbContext)
    {
        _context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _dbSet = dbContext.Set<TEntity>();
    }

    #region 查询

    public virtual async Task<TEntity?> GetByIdAsync(TKey id, bool isTracking = false, bool ignoreFilter = false, CancellationToken cancellationToken = default)
    {
        var query = GetQueryable(isTracking, ignoreFilter);
        return await query.FirstOrDefaultAsync(e => EF.Property<TKey>(e, "Id")!.Equals(id), cancellationToken);
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
        var query = GetQueryable(false, ignoreFilter);
        return await query.Where(e => EF.Property<TKey>(e, "Id")!.Equals(id))
                          .Select(selector)
                          .FirstOrDefaultAsync(cancellationToken);
    }

    /// <summary>
    /// 获取DTO（根据条件，支持selector投影，提高性能）
    /// </summary>
    public virtual async Task<TResult?> FirstOrDefaultAsync<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> selector, bool ignoreFilter = false, CancellationToken cancellationToken = default)
    {
        var query = GetQueryable(false, ignoreFilter);
        return await query.Where(predicate).Select(selector).FirstOrDefaultAsync(cancellationToken);
    }
    /// <summary>
    /// 获取列表（支持表达式排序）
    /// </summary>
    public virtual async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? predicate = null, string? sorting = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, bool isTracking = false, bool ignoreFilter = false, CancellationToken cancellationToken = default)
    {
        var query = GetQueryable(isTracking, ignoreFilter);
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
        var query = GetQueryable(false, ignoreFilter);
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
    /// 分页查询
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
        var query = GetQueryable(isTracking, ignoreFilter);
        if (predicate != null) query = query.Where(predicate);
        if (!string.IsNullOrWhiteSpace(sorting)) query = query.OrderBy(sorting);
        else if (orderBy != null) query = orderBy(query);

        var total = await query.LongCountAsync(cancellationToken);
        var items = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
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
        var query = GetQueryable(false, ignoreFilter);
        if (predicate != null) query = query.Where(predicate);
        if (!string.IsNullOrWhiteSpace(sorting)) query = query.OrderBy(sorting);
        else if (orderBy != null) query = orderBy(query);

        var total = await query.LongCountAsync(cancellationToken);
        var items = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).Select(selector).ToListAsync(cancellationToken);
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
        _dbSet.Update(entity);
        return Task.CompletedTask;
    }
    public virtual Task UpdateRangeAsync(IEnumerable<TEntity> entities)
    {
        _dbSet.UpdateRange(entities);
        return Task.CompletedTask;
    }

    /// <summary>
    /// 根据条件批量更新（指定要更新的字段）
    /// </summary>
    public virtual async Task<int> UpdateAsync(Expression<Func<TEntity, bool>> predicate, Action<TEntity> updateAction, CancellationToken cancellationToken = default)
    {
        var entities = await _dbSet.Where(predicate).ToListAsync(cancellationToken);
        foreach (var entity in entities)
        {
            updateAction(entity);
        }
        _dbSet.UpdateRange(entities);
        return entities.Count;
    }

    /// <summary>
    /// 更新实体指定属性
    /// </summary>
    public virtual Task UpdateAsync(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions)
    {
        var entry = _context.Entry(entity);
        entry.State = EntityState.Unchanged;
        foreach (var property in propertyExpressions)
        {
            entry.Property(property).IsModified = true;
        }
        return Task.CompletedTask;
    }

    /// <summary>
    /// 批量更新实体指定属性
    /// </summary>
    public virtual Task UpdateAsync(IEnumerable<TEntity> entities, params Expression<Func<TEntity, object>>[] propertyExpressions)
    {
        foreach (var entity in entities)
        {
            var entry = _context.Entry(entity);
            entry.State = EntityState.Unchanged;
            foreach (var property in propertyExpressions)
            {
                entry.Property(property).IsModified = true;
            }
        }
        return Task.CompletedTask;
    }

   public virtual async Task<int> UpdateAsync(Expression<Func<TEntity, bool>> predicate, Action<IUpdateSetters<TEntity>> setPropertyCalls, bool ignoreFilter = false, CancellationToken cancellationToken = default)
    {
        var query = GetQueryable(true, ignoreFilter);
        return await query
            .Where(predicate)
            .ExecuteUpdateAsync(builder =>
            {
                var adapter = new UpdateSettersAdapter<TEntity>(builder);
                setPropertyCalls(adapter);
            }, cancellationToken);
    }

    #endregion

    #region 删除
    public virtual void Delete(TEntity entity) => _dbSet.Remove(entity);
    public virtual void DeleteRange(IEnumerable<TEntity> entities) => _dbSet.RemoveRange(entities);
    public virtual Task DeleteAsync(TEntity entity)
    {
        _dbSet.Remove(entity);
        return Task.CompletedTask;
    }
    public virtual Task DeleteRangeAsync(IEnumerable<TEntity> entities)
    {
        _dbSet.RemoveRange(entities);
        return Task.CompletedTask;
    }

    public virtual async Task DeleteAsync(TKey id, CancellationToken cancellationToken = default)
    {
        var entity = await _dbSet.FindAsync([id], cancellationToken);
        if (entity != null)
        {
            _dbSet.Remove(entity);
        }
    }

    /// <summary>
    /// 根据条件批量删除
    /// </summary>
    public virtual async Task<int> DeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        var entities = await _dbSet.Where(predicate).ToListAsync(cancellationToken);
        _dbSet.RemoveRange(entities);
        return entities.Count;
    }

    #endregion

    #region 计数
    public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null, bool ignoreFilter = false, CancellationToken cancellationToken = default)
    {
        var query = GetQueryable(false, ignoreFilter);
        if (predicate != null) query = query.Where(predicate);
        return await query.CountAsync(cancellationToken);
    }

    public virtual async Task<long> LongCountAsync(Expression<Func<TEntity, bool>>? predicate = null, bool ignoreFilter = false, CancellationToken cancellationToken = default)
    {
        var query = GetQueryable(false, ignoreFilter);
        if (predicate != null) query = query.Where(predicate);
        return await query.LongCountAsync(cancellationToken);
    }

    public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? predicate = null, bool ignoreFilter = false, CancellationToken cancellationToken = default)
    {
        var query = GetQueryable(false, ignoreFilter);
        if (predicate != null) query = query.Where(predicate);
        return await query.AnyAsync(cancellationToken);
    }

    public virtual async Task<bool> ExistsAsync(TKey id, bool ignoreFilter = false, CancellationToken cancellationToken = default)
    {
        var query = GetQueryable(false, ignoreFilter);
        return await query.AnyAsync(e => EF.Property<TKey>(e, "Id")!.Equals(id), cancellationToken);
    }
    #endregion

    #region 事务
    public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
       => await _context.SaveChangesAsync(cancellationToken);
    #endregion
}