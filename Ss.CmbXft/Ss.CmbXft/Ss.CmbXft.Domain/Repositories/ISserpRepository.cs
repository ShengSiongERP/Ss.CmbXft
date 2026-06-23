namespace Ss.CmbXft.Domain.Repositories;

/// <summary>
/// 备用数据库仓储接口（用于双数据库同步场景的DI标识）
/// </summary>
public interface ISserpRepository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class
{
}