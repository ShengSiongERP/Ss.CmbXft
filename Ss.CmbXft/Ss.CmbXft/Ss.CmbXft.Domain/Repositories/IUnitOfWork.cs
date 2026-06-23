namespace Ss.CmbXft.Domain.Repositories;

public interface IUnitOfWork 
{
    #region 事务
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
    #endregion

    #region 保存
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    #endregion
}