using System;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ss.CmbXft.Sdk.Client;
using Ss.CmbXft.Sdk.Configuration;
using Ss.CmbXft.Sdk.Services;

namespace Ss.CmbXft.Sdk.Extensions;

/// <summary>
/// 服务集合扩展
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// 添加薪福通SDK服务
    /// </summary>
    /// <param name="services">服务集合</param>
    /// <param name="setupAction">配置选项设置</param>
    /// <returns>服务集合</returns>
    public static IServiceCollection AddXftSdk(
        this IServiceCollection services,
        Action<XftOptions> setupAction)
    {
        if (services == null)
            throw new ArgumentNullException(nameof(services));
        if (setupAction == null)
            throw new ArgumentNullException(nameof(setupAction));

        // 配置选项
        services.Configure(setupAction);

        // 注册XFT客户端
        services.AddSingleton<IXftClient>(sp =>
        {
            var options = sp.GetRequiredService<IOptions<XftOptions>>().Value;
            var httpClient = sp.GetService<HttpClient>() ?? new HttpClient();
            var logger = sp.GetService<ILogger<XftClient>>();
            return new XftClient(options, httpClient, logger);
        });

        // 注册员工服务
        services.AddSingleton<IStaffService>(sp =>
        {
            var client = sp.GetRequiredService<IXftClient>();
            var options = sp.GetRequiredService<IOptions<XftOptions>>().Value;
            var logger = sp.GetService<ILogger<StaffService>>();
            return new StaffService(client, options, logger);
        });

        // 注册角色服务
        services.AddSingleton<IRoleService>(sp =>
        {
            var client = sp.GetRequiredService<IXftClient>();
            var options = sp.GetRequiredService<IOptions<XftOptions>>().Value;
            var logger = sp.GetService<ILogger<RoleService>>();
            return new RoleService(client, options, logger);
        });

        // 注册组织服务
        services.AddSingleton<IOrganizationService>(sp =>
        {
            var client = sp.GetRequiredService<IXftClient>();
            var options = sp.GetRequiredService<IOptions<XftOptions>>().Value;
            var logger = sp.GetService<ILogger<OrganizationService>>();
            return new OrganizationService(client, options, logger);
        });

        // 注册职位服务
        services.AddSingleton<IPositionService>(sp =>
        {
            var client = sp.GetRequiredService<IXftClient>();
            var options = sp.GetRequiredService<IOptions<XftOptions>>().Value;
            var logger = sp.GetService<ILogger<PositionService>>();
            return new PositionService(client, options, logger);
        });

        // 注册岗位服务
        services.AddSingleton<IPostService>(sp =>
        {
            var client = sp.GetRequiredService<IXftClient>();
            var options = sp.GetRequiredService<IOptions<XftOptions>>().Value;
            var logger = sp.GetService<ILogger<PostService>>();
            return new PostService(client, options, logger);
        });

        return services;
    }

    /// <summary>
    /// 添加薪福通SDK服务（使用预配置的选项）
    /// </summary>
    /// <param name="services">服务集合</param>
    /// <param name="options">配置选项</param>
    /// <returns>服务集合</returns>
    public static IServiceCollection AddXftSdk(
        this IServiceCollection services,
        XftOptions options)
    {
        if (services == null)
            throw new ArgumentNullException(nameof(services));
        if (options == null)
            throw new ArgumentNullException(nameof(options));

        // 验证配置
        var validationErrors = new System.Collections.Generic.List<string>();
        if (!options.Validate(out validationErrors))
        {
            throw new ArgumentException($"XftOptions配置验证失败: {string.Join(", ", validationErrors)}");
        }

        // 注册XFT客户端
        services.AddSingleton<IXftClient>(sp =>
        {
            var httpClient = sp.GetService<HttpClient>() ?? new HttpClient();
            var logger = sp.GetService<ILogger<XftClient>>();
            return new XftClient(options, httpClient, logger);
        });

        // 注册员工服务
        services.AddSingleton<IStaffService>(sp =>
        {
            var client = sp.GetRequiredService<IXftClient>();
            var logger = sp.GetService<ILogger<StaffService>>();
            return new StaffService(client, options, logger);
        });

        // 注册角色服务
        services.AddSingleton<IRoleService>(sp =>
        {
            var client = sp.GetRequiredService<IXftClient>();
            var logger = sp.GetService<ILogger<RoleService>>();
            return new RoleService(client, options, logger);
        });

        // 注册组织服务
        services.AddSingleton<IOrganizationService>(sp =>
        {
            var client = sp.GetRequiredService<IXftClient>();
            var logger = sp.GetService<ILogger<OrganizationService>>();
            return new OrganizationService(client, options, logger);
        });

        // 注册职位服务
        services.AddSingleton<IPositionService>(sp =>
        {
            var client = sp.GetRequiredService<IXftClient>();
            var logger = sp.GetService<ILogger<PositionService>>();
            return new PositionService(client, options, logger);
        });

        // 注册岗位服务
        services.AddSingleton<IPostService>(sp =>
        {
            var client = sp.GetRequiredService<IXftClient>();
            var logger = sp.GetService<ILogger<PostService>>();
            return new PostService(client, options, logger);
        });

        return services;
    }
}
