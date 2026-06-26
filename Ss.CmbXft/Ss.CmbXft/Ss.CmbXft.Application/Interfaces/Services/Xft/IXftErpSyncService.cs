using System.Threading;
using System.Threading.Tasks;

namespace Ss.CmbXft.Application.Services;

/// <summary>
/// 员工数据数据库同步服务接口
/// </summary>
public interface IXftErpSyncService
{
    /// <summary>
    /// 从薪福通同步员工数据到本地数据库（同时同步到主数据库和备用数据库）
    /// </summary>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>同步的员工数量</returns>
    Task<int> SyncStaffAsync(CancellationToken cancellationToken = default);

}
