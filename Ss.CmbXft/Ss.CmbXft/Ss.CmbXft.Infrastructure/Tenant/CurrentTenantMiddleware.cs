using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Ss.CmbXft.Domain.Services;
using System.Diagnostics;

namespace Ss.CmbXft.Infrastructure.Tenant;

/// <summary>
/// 当前租户中间件
/// 从 HTTP 请求中提取租户信息并设置到 ICurrentTenant 服务中
/// 支持从以下位置提取租户信息：
/// 1. HTTP Header: X-Tenant-Id, X-Org-Id, X-Tenant-Name
/// 2. Query String: tenantId, orgId, tenantName
/// 3. JWT Token Claims (如果使用 JWT 认证)
/// </summary>
public class CurrentTenantMiddleware
{
    private readonly RequestDelegate _next;

    // Header 常量定义
    private const string TenantIdHeader = "X-Tenant-Id";
    private const string OrgIdHeader = "X-Org-Id";
    private const string TenantNameHeader = "X-Tenant-Name";

    // Query String 常量定义
    private const string TenantIdQuery = "tenantId";
    private const string OrgIdQuery = "orgId";
    private const string TenantNameQuery = "tenantName";

    // JWT Claim 常量定义
    private const string TenantIdClaim = "tenant_id";
    private const string OrgIdClaim = "org_id";
    private const string TenantNameClaim = "tenant_name";

    public CurrentTenantMiddleware(RequestDelegate next)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
    }

    public async Task InvokeAsync(HttpContext context, ICurrentTenant currentTenant)
    {
        if (currentTenant == null)
        {
            throw new InvalidOperationException("ICurrentTenant service is not registered.");
        }

        // 尝试从多个来源提取租户信息
        var tenantId = ExtractTenantId(context);
        var orgId = ExtractOrgId(context);
        var tenantName = ExtractTenantName(context);

        // 设置当前租户信息
        currentTenant.SetCurrent(tenantId, orgId, tenantName);

        // 添加诊断信息，便于调试
        AddDiagnosticInformation(context, tenantId, orgId, tenantName);

        try
        {
            await _next(context);
        }
        finally
        {
            // 请求完成后清除租户信息，防止内存泄漏
            currentTenant.Clear();
        }
    }

    /// <summary>
    /// 提取租户ID
    /// 优先级: Header > Query String > JWT Claim
    /// </summary>
    private long? ExtractTenantId(HttpContext context)
    {
        // 1. 尝试从 Header 获取
        if (context.Request.Headers.TryGetValue(TenantIdHeader, out var headerValue))
        {
            if (long.TryParse(headerValue.FirstOrDefault(), out var tenantId))
            {
                return tenantId;
            }
        }

        // 2. 尝试从 Query String 获取
        if (context.Request.Query.TryGetValue(TenantIdQuery, out var queryValue))
        {
            if (long.TryParse(queryValue.FirstOrDefault(), out var tenantId))
            {
                return tenantId;
            }
        }

        // 3. 尝试从 JWT Claim 获取（如果用户已认证）
        if (context.User?.Identity?.IsAuthenticated == true)
        {
            var claim = context.User.FindFirst(TenantIdClaim);
            if (claim != null && long.TryParse(claim.Value, out var tenantId))
            {
                return tenantId;
            }
        }

        return null;
    }

    /// <summary>
    /// 提取机构ID
    /// 优先级: Header > Query String > JWT Claim
    /// </summary>
    private long ExtractOrgId(HttpContext context)
    {
        // 1. 尝试从 Header 获取
        if (context.Request.Headers.TryGetValue(OrgIdHeader, out var headerValue))
        {
            if (long.TryParse(headerValue.FirstOrDefault(), out var orgId))
            {
                return orgId;
            }
        }

        // 2. 尝试从 Query String 获取
        if (context.Request.Query.TryGetValue(OrgIdQuery, out var queryValue))
        {
            if (long.TryParse(queryValue.FirstOrDefault(), out var orgId))
            {
                return orgId;
            }
        }

        // 3. 尝试从 JWT Claim 获取（如果用户已认证）
        if (context.User?.Identity?.IsAuthenticated == true)
        {
            var claim = context.User.FindFirst(OrgIdClaim);
            if (claim != null && long.TryParse(claim.Value, out var orgId))
            {
                return orgId;
            }
        }

        // 默认返回 0
        return 0;
    }

    /// <summary>
    /// 提取租户名称
    /// 优先级: Header > Query String > JWT Claim
    /// </summary>
    private string? ExtractTenantName(HttpContext context)
    {
        // 1. 尝试从 Header 获取
        if (context.Request.Headers.TryGetValue(TenantNameHeader, out var headerValue))
        {
            var name = headerValue.FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(name))
            {
                return name;
            }
        }

        // 2. 尝试从 Query String 获取
        if (context.Request.Query.TryGetValue(TenantNameQuery, out var queryValue))
        {
            var name = queryValue.FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(name))
            {
                return name;
            }
        }

        // 3. 尝试从 JWT Claim 获取（如果用户已认证）
        if (context.User?.Identity?.IsAuthenticated == true)
        {
            var claim = context.User.FindFirst(TenantNameClaim);
            if (claim != null && !string.IsNullOrWhiteSpace(claim.Value))
            {
                return claim.Value;
            }
        }

        return null;
    }

    /// <summary>
    /// 添加诊断信息
    /// </summary>
    private void AddDiagnosticInformation(HttpContext context, long? tenantId, long orgId, string? tenantName)
    {
        Activity.Current?.SetTag("tenant.id", tenantId?.ToString() ?? "null");
        Activity.Current?.SetTag("tenant.orgId", orgId.ToString());
        Activity.Current?.SetTag("tenant.name", tenantName ?? "null");
        Activity.Current?.SetTag("tenant.available", (tenantId.HasValue && tenantId.Value > 0).ToString());
    }
}

/// <summary>
/// 中间件扩展方法
/// </summary>
public static class CurrentTenantMiddlewareExtensions
{
    /// <summary>
    /// 使用当前租户中间件
    /// </summary>
    /// <param name="builder">应用程序构建器</param>
    /// <returns>应用程序构建器</returns>
    public static IApplicationBuilder UseCurrentTenant(this IApplicationBuilder builder)
    {
        if (builder == null)
        {
            throw new ArgumentNullException(nameof(builder));
        }

        return builder.UseMiddleware<CurrentTenantMiddleware>();
    }
}