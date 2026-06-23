using Ss.CmbXft.Domain.Services;

namespace Ss.CmbXft.Infrastructure.Tenant;

/// <summary>
/// 当前租户实现
/// 使用 AsyncLocal 确保在异步操作中租户上下文的一致性
/// </summary>
public class CurrentTenant : ICurrentTenant
{
    private static readonly AsyncLocal<long?> _currentTenantId = new();
    private static readonly AsyncLocal<long> _currentOrgId = new();
    private static readonly AsyncLocal<string?> _currentTenantName = new();

    /// <summary>
    /// 当前租户ID
    /// </summary>
    public long? Id => _currentTenantId.Value;

    /// <summary>
    /// 当前机构ID
    /// </summary>
    public long OrgId => _currentOrgId.Value;

    /// <summary>
    /// 租户名称
    /// </summary>
    public string? Name => _currentTenantName.Value;

    /// <summary>
    /// 是否可用（租户是否有效）
    /// </summary>
    public bool IsAvailable => Id.HasValue && Id.Value > 0;

    /// <summary>
    /// 设置当前租户信息
    /// </summary>
    /// <param name="tenantId">租户ID</param>
    /// <param name="orgId">机构ID</param>
    /// <param name="tenantName">租户名称</param>
    public void SetCurrent(long? tenantId, long orgId, string? tenantName = null)
    {
        _currentTenantId.Value = tenantId;
        _currentOrgId.Value = orgId;
        _currentTenantName.Value = tenantName;
    }

    /// <summary>
    /// 清除当前租户信息
    /// </summary>
    public void Clear()
    {
        _currentTenantId.Value = null;
        _currentOrgId.Value = 0;
        _currentTenantName.Value = null;
    }
}