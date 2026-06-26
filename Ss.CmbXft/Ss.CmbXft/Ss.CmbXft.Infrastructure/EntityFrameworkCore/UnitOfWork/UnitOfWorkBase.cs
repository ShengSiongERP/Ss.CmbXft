using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Ss.CmbXft.Domain.Repositories;

namespace Ss.CmbXft.Infrastructure.EntityFrameworkCore;

/// <summary>
/// 通用工作单元泛型基类，聚合事务 / 保存 / 释放逻辑。
/// 子类只需指定具体的 DbContext 类型即可。
/// </summary>
public abstract class UnitOfWorkBase<TDbContext> : IUnitOfWork, IDisposable, IAsyncDisposable
    where TDbContext : DbContext
{
    private readonly TDbContext _dbContext;
    private IDbContextTransaction? _transaction;

    protected UnitOfWorkBase(TDbContext context, IServiceProvider serviceProvider)
    {
        _dbContext = context;
    }

    #region 事务
    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        _transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction != null)
        {
            await _transaction.CommitAsync(cancellationToken);
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync(cancellationToken);
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }
    #endregion

    #region 保存

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => await _dbContext.SaveChangesAsync(cancellationToken);

    #endregion

    #region 释放

    public void Dispose()
    {
        _transaction?.Dispose();
        _dbContext.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        if (_transaction != null)
            await _transaction.DisposeAsync();

        await _dbContext.DisposeAsync();
    }
    #endregion
}
