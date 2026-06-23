namespace Ss.CmbXft.Domain.Repositories;

/// <summary>
/// POS数据库仓储接口（用于POS数据库同步场景的DI标识）
/// </summary>
public interface ISsposRepository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class
{
}
