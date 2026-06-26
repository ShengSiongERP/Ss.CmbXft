using Ss.CmbXft.Domain.Repositories;

namespace Ss.CmbXft.Infrastructure.EntityFrameworkCore;

/// <summary>
/// Sserp数据库的工作单元实现
/// </summary>
public class SserpUnitOfWork : UnitOfWorkBase<SserpDbContext>, ISserpUnitOfWork
{
    public SserpUnitOfWork(SserpDbContext context, IServiceProvider serviceProvider) : base(context, serviceProvider)
    {
    }
}
