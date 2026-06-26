using Microsoft.Extensions.Logging;
using Ss.CmbXft.Application.Services;
using Quartz;

namespace Ss.CmbXft.Infrastructure.Jobs;

/// <summary>
/// 薪福通员工数据同步定时任务。
/// 每天凌晨 3:00 从薪福通拉取最新员工数据并同步到本地数据库。
/// </summary>
/// <remarks>
/// 调度配置见 <see cref="QuartzServiceExtensions.AddQuartzJobs"/> 中的 XftStaffSyncJob 注册。
/// 也可通过 appsettings.json 的 Quartz 节调整 cron 表达式。
/// </remarks>
[DisallowConcurrentExecution] // 禁止并发执行，防止上一次尚未完成时下一次被触发
public class XftStaffSyncJob : JobBase
{
    private readonly IXftErpSyncService _syncService;
    private readonly ILogger<XftStaffSyncJob> _logger;

    public XftStaffSyncJob(
        IXftErpSyncService syncService,
        ILogger<XftStaffSyncJob> logger)
    {
        _syncService = syncService;
        _logger = logger;
    }

    protected override ILogger Logger => _logger;

    protected override async Task ExecuteJobAsync(IJobExecutionContext context, CancellationToken cancellationToken)
    {
        _logger.LogInformation("开始执行薪福通员工数据同步...");

        var syncedCount = await _syncService.SyncStaffAsync(cancellationToken);

        _logger.LogInformation("薪福通员工数据同步完成，共同步 {SyncedCount} 条记录", syncedCount);
    }
}