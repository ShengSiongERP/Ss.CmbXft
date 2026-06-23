using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ss.CmbXft.Infrastructure.EntityFrameworkCore;

/// <summary>
/// 实体配置接口
/// </summary>
public interface IEntityTypeConfiguration<TEntity> where TEntity : class
{
    void Configure(EntityTypeBuilder<TEntity> builder);
}

public static class EntityTypeConfigurationExtensions
{
    public static void ApplyConfiguration<TEntity>(this ModelBuilder modelBuilder, IEntityTypeConfiguration<TEntity> configuration) where TEntity : class
    {
        configuration.Configure(modelBuilder.Entity<TEntity>());
    }
}
