using System;
using Ss.CmbXft.Domain.Base;

namespace Ss.CmbXft.Domain.Entities;

/// <summary>
/// 薪福通事件推送日志
/// </summary>
public class XftEventLog : AllEntityBase<long>
{
    /// <summary>
    /// 事件ID
    /// </summary>
    public string EventId { get; set; } = string.Empty;

    /// <summary>
    /// 事件业务码
    /// </summary>
    public string BusinessKey { get; set; } = string.Empty;

    /// <summary>
    /// 事件唯一记录码
    /// </summary>
    public long EventCd { get; set; }

    /// <summary>
    /// 企业号
    /// </summary>
    public string? PrjCod { get; set; }

    /// <summary>
    /// 变更时间
    /// </summary>
    public string EventTime { get; set; } = string.Empty;

    /// <summary>
    /// 应用ID
    /// </summary>
    public string AppId { get; set; } = string.Empty;

    /// <summary>
    /// 事件变更信息（原始JSON）
    /// </summary>
    public string EventRcdInf { get; set; } = string.Empty;

    /// <summary>
    /// 签名
    /// </summary>
    public string? Signature { get; set; }

    /// <summary>
    /// 处理状态：0-待处理，1-处理成功，2-处理失败
    /// </summary>
    public int ProcessStatus { get; set; }

    /// <summary>
    /// 处理结果消息
    /// </summary>
    public string? ProcessMessage { get; set; }

    /// <summary>
    /// 重试次数
    /// </summary>
    public int RetryCount { get; set; }
}
