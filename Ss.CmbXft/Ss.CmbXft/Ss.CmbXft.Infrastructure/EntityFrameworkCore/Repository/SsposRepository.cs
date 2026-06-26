using Ss.CmbXft.Domain.Repositories;

namespace Ss.CmbXft.Infrastructure.EntityFrameworkCore.Repository;

/// <summary>
/// 基于POS数据库（SsposDbContext）的通用仓储实现
/// </summary>
public class SsposRepository<TEntity, TKey> : RepositoryBase<TEntity, TKey, SsposDbContext>, ISsposRepository<TEntity, TKey>
    where TEntity : class
{
    public SsposRepository(SsposDbContext dbContext) : base(dbContext)
    {
    }
}
