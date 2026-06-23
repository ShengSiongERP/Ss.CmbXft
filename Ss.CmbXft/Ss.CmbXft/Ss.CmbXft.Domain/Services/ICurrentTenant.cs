namespace Ss.CmbXft.Domain.Services;

/// <summary>
/// 当前租户服务接口
/// 用于在应用程序中获取和设置当前请求的租户信息
/// </summary>
public interface ICurrentTenant
{
    /// <summary>
    /// 当前租户ID
    /// 可能为null，表示当前请求没有租户上下文（如系统管理员操作）
    /// </summary>
    long? Id { get; }

    /// <summary>
    /// 当前机构ID
    /// </summary>
    long OrgId { get; }

    /// <summary>
    /// 租户名称
    /// </summary>
    string? Name { get; }

    /// <summary>
    /// 是否可用（租户是否有效）
    /// </summary>
    bool IsAvailable { get; }

    /// <summary>
    /// 设置当前租户信息
    /// 通常由中间件调用，从请求上下文中提取租户信息
    /// </summary>
    /// <param name="tenantId">租户ID</param>
    /// <param name="orgId">机构ID</param>
    /// <param name="tenantName">租户名称</param>
    void SetCurrent(long? tenantId, long orgId, string? tenantName = null);

    /// <summary>
    /// 清除当前租户信息
    /// 用于请求结束或需要切换租户上下文时
    /// </summary>
    void Clear();
}