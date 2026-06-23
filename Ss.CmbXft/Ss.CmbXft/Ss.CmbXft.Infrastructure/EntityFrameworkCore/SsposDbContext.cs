using Microsoft.EntityFrameworkCore;
using Ss.CmbXft.Domain.Entities.Sspos;
using Ss.CmbXft.Infrastructure.EntityFrameworkCore.Configurations.Sspos;

namespace Ss.CmbXft.Infrastructure.EntityFrameworkCore;

/// <summary>
/// POS DbContext
/// </summary>
public class SsposDbContext : DbContext
{
    public SsposDbContext(DbContextOptions<SsposDbContext> options) : base(options)
    {
    }

    // POS 数据库实体
    public DbSet<PosLocation> Locations { get; set; }
    public DbSet<PosVoucher> Vouchers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // POS 实体配置
        modelBuilder.ApplyConfiguration(new PosLocationConfiguration());
        modelBuilder.ApplyConfiguration(new PosVoucherConfiguration());
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries();

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                // POS数据库的实体通常在创建时不需要自动设置时间戳
                // 因为它们是从外部系统读取的
            }
            else if (entry.State == EntityState.Modified)
            {
                // POS数据库的实体通常在修改时不需要自动设置时间戳
                // 因为它们是从外部系统读取的
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
