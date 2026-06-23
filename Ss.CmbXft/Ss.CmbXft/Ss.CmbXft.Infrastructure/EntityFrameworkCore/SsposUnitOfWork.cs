using Microsoft.EntityFrameworkCore.Storage;
using Ss.CmbXft.Domain.Repositories;

namespace Ss.CmbXft.Infrastructure.EntityFrameworkCore;

/// <summary>
/// POS数据库的工作单元实现
/// </summary>
public class SsposUnitOfWork : ISsposUnitOfWork
{
    private readonly SsposDbContext _dbContext;
    private IDbContextTransaction? _transaction;
    private bool _disposed;

    public SsposUnitOfWork(SsposDbContext context, IServiceProvider serviceProvider)
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
