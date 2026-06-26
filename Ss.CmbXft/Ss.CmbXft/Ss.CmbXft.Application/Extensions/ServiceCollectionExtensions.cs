using FluentValidation;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using Ss.CmbXft.Application.Services;
using Ss.CmbXft.Application.Validators;
using System.Reflection;

namespace Ss.CmbXft.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // 注册应用服务
        services.AddScoped<IXftErpSyncService, XftErpSyncService>();
        services.AddScoped<IRoleAppService, RoleAppService>();
        services.AddScoped<IOrganizationAppService, OrganizationAppService>();
        services.AddScoped<IXftPositionAppService, XftPositionAppService>();
        services.AddScoped<IPostAppService, PostAppService>();

        // 注册验证服务
        services.AddScoped<IValidationService, ValidationService>();

        // 自动扫描并注册所有验证器
        //services.AddValidatorsAutoRegister();已废弃
        //services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining(typeof(IFluentValidation));
        //// 注册 AutoMapper
        //services.AddAutoMapper(config =>
        //{
        //    config.AddMaps(AppDomain.CurrentDomain.GetAssemblies());
        //});
        // 注册 Mapster 映射配置
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());
        // Infrastructure 程序集扫描需要手动添加  只扫当前程序集 放在program也不扫其他的
        //config.Scan(Assembly.Load("Ss.CmbXft.Infrastructure"));  // config.Scan(typeof(SsMemberClient).Assembly);

        services.AddSingleton(config);

        // 注册 Mapster IMapper（DI 容器中使用）
        services.AddScoped<MapsterMapper.IMapper, MapsterMapper.ServiceMapper>();

        return services;
    }
}
