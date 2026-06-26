using Microsoft.Extensions.Logging;
using Quartz;
using Ss.CmbXft.Application.Dtos.Sserp.Product;
using Ss.CmbXft.Application.Interfaces.Services.Sserp.SsMember;
using Ss.CmbXft.Common.Models;

namespace Ss.CmbXft.Infrastructure.Jobs;

/// <summary>
/// 商品数据同步定时任务。
/// 按计划从SSERP数据库拉取商品数据并同步到昇菘会员系统。
/// </summary>
/// <remarks>
/// 调度配置见 <see cref="QuartzServiceExtensions.AddQuartzJobs"/> 中的 ProductSyncJob 注册。
/// 也可通过 appsettings.json 的 Quartz 节调整 cron 表达式和分页参数。
/// </remarks>
[DisallowConcurrentExecution]
public class ProductSyncJob : JobBase
{
    private readonly IProductSyncAppService _productSyncService;
    private readonly ILogger<ProductSyncJob> _logger;

    public ProductSyncJob(
        IProductSyncAppService productSyncService,
        ILogger<ProductSyncJob> logger)
    {
        _productSyncService = productSyncService;
        _logger = logger;
    }

    protected override ILogger Logger => _logger;

    protected override async Task ExecuteJobAsync(IJobExecutionContext context, CancellationToken cancellationToken)
    {
        _logger.LogInformation("开始执行商品数据同步...");

        DateTime dt = DateTime.UtcNow.AddDays(-2);
        ProductSyncQueryDto query = new() { PageIndex = 1, PageSize = 1000, ModifiedSince = dt, CreatedSince = dt };
        ApiResult result = await _productSyncService.SyncProductsAsync(query, cancellationToken);

        _logger.LogInformation("商品数据同步完成：{Msg}", result.Msg);
    }
}
