using Ss.CmbXft.Domain.Repositories;

namespace Ss.CmbXft.Infrastructure.EntityFrameworkCore.Repository;

/// <summary>
/// 基于备用数据库（SserpDbContext）的通用仓储实现
/// </summary>
public class SserpRepository<TEntity, TKey> : RepositoryBase<TEntity, TKey, SserpDbContext>, ISserpRepository<TEntity, TKey>
    where TEntity : class
{
    public SserpRepository(SserpDbContext dbContext) : base(dbContext)
    {
    }
}
