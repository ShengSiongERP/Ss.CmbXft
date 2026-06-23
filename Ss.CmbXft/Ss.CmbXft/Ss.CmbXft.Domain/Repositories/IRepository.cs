using System.Linq.Expressions;
using Ss.CmbXft.Common.Models;

namespace Ss.CmbXft.Domain.Repositories;

public interface IRepository<TEntity, TKey> where TEntity : class
{
    #region 查询
    Task<TEntity?> GetByIdAsync(TKey id, bool isTracking = false, bool ignoreFilter = false, CancellationToken cancellationToken = default);
    Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool isTracking = false, bool ignoreFilter = false, CancellationToken cancellationToken = default);

    /// <summary>
    /// 获取DTO（根据ID，支持selector投影，提高性能）
    /// </summary>
    Task<TResult?> GetByIdAsync<TResult>(TKey id, Expression<Func<TEntity, TResult>> selector, bool ignoreFilter = false, CancellationToken cancellationToken = default);

    /// <summary>
    /// 获取DTO（根据条件，支持selector投影，提高性能）
    /// </summary>
    Task<TResult?> FirstOrDefaultAsync<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> selector, bool ignoreFilter = false, CancellationToken cancellationToken = default);

    /// <summary>
    /// 获取列表
    /// </summary>
    Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? predicate = null, string? sorting = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, bool isTracking = false, bool ignoreFilter = false, CancellationToken cancellationToken = default);

    /// <summary>
    /// 获取DTO列表（支持selector投影，支持表达式排序，提高性能）
    /// </summary>
    Task<List<TResult>> GetListAsync<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>>? predicate = null, string? sorting = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, bool ignoreFilter = false, CancellationToken cancellationToken = default);

    IQueryable<TEntity> GetQueryable(bool isTracking = false, bool ignoreFilter = false);
    #endregion

    #region 分页
   
    /// <summary>
    /// 分页查询
    /// </summary>
    Task<PageResult<TEntity>> GetPagedListAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        string? sorting = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        int pageIndex = 1,
        int pageSize = 20,
        bool isTracking = false,
        bool ignoreFilter = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 分页查询（支持selector投影，支持表达式排序，提高性能）
    /// </summary>
    Task<PageResult<TResult>> GetPagedListAsync<TResult>(
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        string? sorting = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        int pageIndex = 1,
        int pageSize = 20,
        bool ignoreFilter = false,
        CancellationToken cancellationToken = default);
    #endregion

    #region 新增
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
    #endregion

    #region 更新
    void Update(TEntity entity);
    void Update(IEnumerable<TEntity> entities);
    Task UpdateAsync(TEntity entity);
    Task UpdateRangeAsync(IEnumerable<TEntity> entities);

    /// <summary>
    /// 根据条件批量更新（指定要更新的字段）
    /// </summary>
    Task<int> UpdateAsync(Expression<Func<TEntity, bool>> predicate, Action<TEntity> updateAction, CancellationToken cancellationToken = default);

    /// <summary>
    /// 更新实体指定属性
    /// </summary>
    Task UpdateAsync(TEntity entity, params Expression<Func<TEntity, object>>[] propertyExpressions);

    /// <summary>
    /// 批量更新实体指定属性
    /// </summary>
    Task UpdateAsync(IEnumerable<TEntity> entities, params Expression<Func<TEntity, object>>[] propertyExpressions);
    Task<int> UpdateAsync(Expression<Func<TEntity, bool>> predicate, Action<IUpdateSetters<TEntity>> setPropertyCalls, bool ignoreFilter = false, CancellationToken cancellationToken = default);
    #endregion

    #region 删除
    void Delete(TEntity entity);
    void DeleteRange(IEnumerable<TEntity> entities);
    Task DeleteAsync(TEntity entity);
    Task DeleteRangeAsync(IEnumerable<TEntity> entities);
    Task DeleteAsync(TKey id, CancellationToken cancellationToken = default);

    /// <summary>
    /// 根据条件批量删除
    /// </summary>
    Task<int> DeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    #endregion

    #region 计数
    Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null, bool ignoreFilter = false, CancellationToken cancellationToken = default);
    Task<long> LongCountAsync(Expression<Func<TEntity, bool>>? predicate = null, bool ignoreFilter = false, CancellationToken cancellationToken = default);
    Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? predicate = null, bool ignoreFilter = false, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(TKey id, bool ignoreFilter = false, CancellationToken cancellationToken = default);
    #endregion

    #region 事务
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    #endregion
}
public interface IUpdateSetters<TEntity>
{
    void SetProperty<TProperty>(
        Expression<Func<TEntity, TProperty>> propertyExpression,
        TProperty value);
}