using Microsoft.Extensions.Logging;
using Ss.CmbXft.Application.Dtos.Sspos;
using Quartz;
using Ss.CmbXft.Infrastructure.ThirdParty.SsMember.Interfaces;

namespace Ss.CmbXft.Infrastructure.Jobs;

/// <summary>
/// 门店数据同步定时任务。
/// 按计划从POS数据库拉取门店数据并同步到昇菘会员系统。
/// </summary>
/// <remarks>
/// 调度配置见 <see cref="QuartzServiceExtensions.AddQuartzJobs"/> 中的 StoreSyncJob 注册。
/// 也可通过 appsettings.json 的 Quartz 节调整 cron 表达式。
/// </remarks>
[DisallowConcurrentExecution]
public class StoreSyncJob : JobBase
{
    private readonly IStoreSyncAppService _storeSyncService;
    private readonly ILogger<StoreSyncJob> _logger;

    public StoreSyncJob(
        IStoreSyncAppService storeSyncService,
        ILogger<StoreSyncJob> logger)
    {
        _storeSyncService = storeSyncService;
        _logger = logger;
    }

    protected override ILogger Logger => _logger;

    protected override async Task ExecuteJobAsync(IJobExecutionContext context, CancellationToken cancellationToken)
    {
        _logger.LogInformation("开始执行门店数据同步...");

        var query = new StoreSyncQueryDto();
        var result = await _storeSyncService.SyncStoresAsync(query, cancellationToken);

        _logger.LogInformation("门店数据同步完成：{Msg}", result.Msg);
    }
}
