using Ss.CmbXft.Domain.Repositories;

namespace Ss.CmbXft.Infrastructure.EntityFrameworkCore;

/// <summary>
/// POS数据库的工作单元实现
/// </summary>
public class SsposUnitOfWork : UnitOfWorkBase<SsposDbContext>, ISsposUnitOfWork
{
    public SsposUnitOfWork(SsposDbContext context, IServiceProvider serviceProvider) : base(context, serviceProvider)
    {
    }
}
