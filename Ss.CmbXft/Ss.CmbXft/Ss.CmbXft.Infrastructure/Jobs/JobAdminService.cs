using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Impl.Matchers;
using Ss.CmbXft.Application.Dtos.Job;
using Ss.CmbXft.Application.Services;

namespace Ss.CmbXft.Infrastructure.Jobs;

/// <summary>
/// 定时任务管理服务实现。
/// 通过 Quartz IScheduler 提供查询、手动触发、暂停/恢复等运维操作。
/// </summary>
public class JobAdminService : IJobAdminService
{
    private readonly ISchedulerFactory _schedulerFactory;
    private readonly ILogger<JobAdminService> _logger;

    public JobAdminService(
        ISchedulerFactory schedulerFactory,
        ILogger<JobAdminService> logger)
    {
        _schedulerFactory = schedulerFactory;
        _logger = logger;
    }

    public async Task<SchedulerOverviewDto> GetSchedulerStatusAsync(CancellationToken cancellationToken = default)
    {
        var scheduler = await _schedulerFactory.GetScheduler(cancellationToken);

        var metaData = await scheduler.GetMetaData(cancellationToken);
        var currentlyExecuting = await scheduler.GetCurrentlyExecutingJobs(cancellationToken);
        var allJobKeys = await scheduler.GetJobKeys(GroupMatcher<JobKey>.AnyGroup(), cancellationToken);

        return new SchedulerOverviewDto
        {
            SchedulerName = scheduler.SchedulerName,
            SchedulerInstanceId = scheduler.SchedulerInstanceId,
            Status = scheduler.IsStarted ? "Started" : scheduler.InStandbyMode ? "Standby" : "Shutdown",
            RunningSince = metaData.RunningSince?.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss") ?? "N/A",
            TotalJobs = allJobKeys.Count,
            CurrentlyExecutingJobs = currentlyExecuting.Count,
            ThreadPoolSize = metaData.ThreadPoolSize,
            CheckTimeUtc = DateTime.UtcNow
        };
    }

    public async Task<List<JobDetailDto>> GetAllJobsAsync(CancellationToken cancellationToken = default)
    {
        var scheduler = await _schedulerFactory.GetScheduler(cancellationToken);
        var jobKeys = await scheduler.GetJobKeys(GroupMatcher<JobKey>.AnyGroup(), cancellationToken);
        var currentlyExecuting = await scheduler.GetCurrentlyExecutingJobs(cancellationToken);
        var runningJobKeys = new HashSet<JobKey>(currentlyExecuting.Select(c => c.JobDetail.Key));

        var result = new List<JobDetailDto>();

        foreach (var jobKey in jobKeys)
        {
            var detail = await BuildJobDetailDto(scheduler, jobKey, runningJobKeys, cancellationToken);
            if (detail is not null)
                result.Add(detail);
        }

        return result;
    }

    public async Task<JobDetailDto?> GetJobDetailAsync(string jobName, string group = "DEFAULT", CancellationToken cancellationToken = default)
    {
        var scheduler = await _schedulerFactory.GetScheduler(cancellationToken);
        var jobKey = new JobKey(jobName, group);

        var currentlyExecuting = await scheduler.GetCurrentlyExecutingJobs(cancellationToken);
        var runningJobKeys = new HashSet<JobKey>(currentlyExecuting.Select(c => c.JobDetail.Key));

        return await BuildJobDetailDto(scheduler, jobKey, runningJobKeys, cancellationToken);
    }

    public async Task<bool> TriggerJobAsync(string jobName, string group = "DEFAULT", CancellationToken cancellationToken = default)
    {
        var scheduler = await _schedulerFactory.GetScheduler(cancellationToken);
        var jobKey = new JobKey(jobName, group);

        if (!await scheduler.CheckExists(jobKey, cancellationToken))
        {
            _logger.LogWarning("手动触发失败: Job [{Group}.{JobName}] 不存在", group, jobName);
            return false;
        }

        await scheduler.TriggerJob(jobKey, cancellationToken);
        _logger.LogInformation("已手动触发 Job [{Group}.{JobName}]", group, jobName);
        return true;
    }

    public async Task<bool> PauseJobAsync(string jobName, string group = "DEFAULT", CancellationToken cancellationToken = default)
    {
        var scheduler = await _schedulerFactory.GetScheduler(cancellationToken);
        var jobKey = new JobKey(jobName, group);

        if (!await scheduler.CheckExists(jobKey, cancellationToken))
        {
            _logger.LogWarning("暂停失败: Job [{Group}.{JobName}] 不存在", group, jobName);
            return false;
        }

        await scheduler.PauseJob(jobKey, cancellationToken);
        _logger.LogInformation("已暂停 Job [{Group}.{JobName}] 及其所有触发器", group, jobName);
        return true;
    }

    public async Task<bool> ResumeJobAsync(string jobName, string group = "DEFAULT", CancellationToken cancellationToken = default)
    {
        var scheduler = await _schedulerFactory.GetScheduler(cancellationToken);
        var jobKey = new JobKey(jobName, group);

        if (!await scheduler.CheckExists(jobKey, cancellationToken))
        {
            _logger.LogWarning("恢复失败: Job [{Group}.{JobName}] 不存在", group, jobName);
            return false;
        }

        await scheduler.ResumeJob(jobKey, cancellationToken);
        _logger.LogInformation("已恢复 Job [{Group}.{JobName}] 及其所有触发器", group, jobName);
        return true;
    }

    public async Task<bool> InterruptJobAsync(string jobName, string group = "DEFAULT", CancellationToken cancellationToken = default)
    {
        var scheduler = await _schedulerFactory.GetScheduler(cancellationToken);
        var jobKey = new JobKey(jobName, group);

        var result = await scheduler.Interrupt(jobKey, cancellationToken);
        if (result)
        {
            _logger.LogInformation("已发送中断信号给 Job [{Group}.{JobName}]", group, jobName);
        }
        else
        {
            _logger.LogWarning("中断 Job [{Group}.{JobName}] 失败（可能未在执行中）", group, jobName);
        }

        return result;
    }

    #region Private Helpers

    private static async Task<JobDetailDto?> BuildJobDetailDto(
        IScheduler scheduler,
        JobKey jobKey,
        HashSet<JobKey> runningJobKeys,
        CancellationToken ct)
    {
        var jobDetail = await scheduler.GetJobDetail(jobKey, ct);
        if (jobDetail is null) return null;

        var triggers = await scheduler.GetTriggersOfJob(jobKey, ct);

        // 计算所有触发器的最近/下次时间
        var prevFire = triggers
            .Where(t => t.GetPreviousFireTimeUtc().HasValue)
            .Select(t => t.GetPreviousFireTimeUtc())
            .Max();
        var nextFire = triggers
            .Where(t => t.GetNextFireTimeUtc().HasValue)
            .Select(t => t.GetNextFireTimeUtc())
            .Min();

        var dto = new JobDetailDto
        {
            JobName = jobKey.Name,
            Group = jobKey.Group,
            Description = jobDetail.Description,
            JobType = jobDetail.JobType.FullName ?? jobDetail.JobType.Name,
            IsDurable = jobDetail.Durable,
            IsRunning = runningJobKeys.Contains(jobKey),
            PreviousFireTimeUtc = prevFire?.UtcDateTime,
            NextFireTimeUtc = nextFire?.UtcDateTime,
            Triggers = triggers.Select(MapTrigger).ToList(),
            LastExecution = ReadExecutionSnapshot(jobDetail.JobDataMap)
        };

        return dto;
    }

    private static JobTriggerDto MapTrigger(ITrigger trigger)
    {
        var dto = new JobTriggerDto
        {
            TriggerName = trigger.Key.Name,
            Group = trigger.Key.Group,
            Description = trigger.Description,
            TriggerType = trigger switch
            {
                ICronTrigger => "Cron",
                ISimpleTrigger => "Simple",
                ICalendarIntervalTrigger => "CalendarInterval",
                IDailyTimeIntervalTrigger => "DailyTimeInterval",
                _ => trigger.GetType().Name
            },
            StartTimeUtc = trigger.StartTimeUtc.UtcDateTime,
            EndTimeUtc = trigger.EndTimeUtc?.UtcDateTime,
            PreviousFireTimeUtc = trigger.GetPreviousFireTimeUtc()?.UtcDateTime,
            NextFireTimeUtc = trigger.GetNextFireTimeUtc()?.UtcDateTime,
            TimeZone = trigger is ICronTrigger ctz ? ctz.TimeZone?.Id : null
        };

        if (trigger is ICronTrigger cronTrigger)
        {
            dto.CronExpression = cronTrigger.CronExpressionString;
        }

        return dto;
    }

    /// <summary>
    /// 从 JobDataMap 读取最近一次执行快照。
    /// JobBase 在执行完成后将结果写入 JobDataMap。
    /// </summary>
    private static JobExecutionSnapshot? ReadExecutionSnapshot(JobDataMap dataMap)
    {
        var hasData = dataMap.ContainsKey(JobBase.LastElapsedMsKey);
        if (!hasData) return null;

        return new JobExecutionSnapshot
        {
            LastFireTimeUtc = dataMap.ContainsKey(JobBase.LastFireTimeKey)
                ? dataMap.GetDateTime(JobBase.LastFireTimeKey)
                : null,
            ElapsedMilliseconds = dataMap.ContainsKey(JobBase.LastElapsedMsKey)
                ? dataMap.GetLong(JobBase.LastElapsedMsKey)
                : -1,
            ResultMessage = dataMap.ContainsKey(JobBase.LastResultKey)
                ? dataMap.GetString(JobBase.LastResultKey)
                : null,
            IsSuccess = dataMap.ContainsKey(JobBase.IsSuccessKey)
                ? dataMap.GetBoolean(JobBase.IsSuccessKey)
                : null
        };
    }

    #endregion
}