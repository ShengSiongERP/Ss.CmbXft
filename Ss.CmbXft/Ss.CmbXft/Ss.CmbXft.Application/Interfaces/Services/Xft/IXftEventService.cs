using System.Threading.Tasks;
using Ss.CmbXft.Sdk.Models.Events;

namespace Ss.CmbXft.Application.Services;

/// <summary>
/// 薪福通事件处理服务接口
/// </summary>
public interface IXftEventService
{
    /// <summary>
    /// 处理薪福通事件推送
    /// </summary>
    /// <param name="request">事件推送请求</param>
    /// <returns>处理结果</returns>
    Task<bool> ProcessEventAsync(XftEventPushRequest request);
}
