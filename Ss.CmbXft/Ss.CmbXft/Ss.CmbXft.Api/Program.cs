using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Ss.CmbXft.Api.Infrastructure.Middleware;
using Ss.CmbXft.Application.Extensions;
using Ss.CmbXft.Infrastructure.EntityFrameworkCore;
using Ss.CmbXft.Infrastructure.Extensions;
using Ss.CmbXft.Infrastructure.Tenant;
using Ss.CmbXft.Sdk.Configuration;
using Ss.CmbXft.Sdk.Extensions;
using System.Text;
using Yitter.IdGenerator;

// 初始化雪花算法ID生成器
YitIdHelper.SetIdGenerator(new IdGeneratorOptions(1));

var builder = WebApplication.CreateBuilder(args);

// 配置 Serilog
builder.Services.AddSerilogLogging(builder.Configuration);
builder.Host.UseSerilog();

// 添加基础架构服务
builder.Services.AddInfrastructure(builder.Configuration);

// 添加应用层服务
builder.Services.AddApplication();

// 添加 Quartz 定时任务
builder.Services.AddQuartzJobs(builder.Configuration);

// 添加控制器
//builder.Services.AddControllers();

// 注册全局模型验证过滤器
builder.Services.AddControllers(options =>
{
    options.Filters.Add<GlobalValidationFilter>();
})
// 关闭默认自动验证返回
.ConfigureApiBehaviorOptions(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});



// 添加OpenAPI/Swagger
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 添加薪福通SDK服务
builder.Services.AddXftSdk(options =>
{
    options.CompanyId = builder.Configuration["XftOptions:CompanyId"] ?? string.Empty;
    options.AppId = builder.Configuration["XftOptions:AppId"] ?? string.Empty;
    options.AuthoritySecret = builder.Configuration["XftOptions:AuthoritySecret"] ?? string.Empty;
    options.BaseUrl = builder.Configuration["XftOptions:BaseUrl"] ?? string.Empty;
    options.Environment = int.TryParse(builder.Configuration["XftOptions:Environment"], out var env) ? (XftEnvironment)env : XftEnvironment.Test;
    options.TimeoutSeconds = int.TryParse(builder.Configuration["XftOptions:TimeoutSeconds"], out var timeout) ? timeout : 30;
    options.EnableLogging = bool.TryParse(builder.Configuration["XftOptions:EnableLogging"], out bool enbaleLogging) ? enbaleLogging : false;
    options.UsrUid = builder.Configuration["XftOptions:UsrUid"] ?? "AUTO0001";
    options.UsrNbr = builder.Configuration["XftOptions:UsrNbr"] ?? "A0001";
});

// 添加JWT认证
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var secretKey = builder.Configuration["Jwt:Secret"] ?? "SsCmbXft2024SecretKeyForJwtTokenGeneration!@#$%";
    var key = Encoding.UTF8.GetBytes(secretKey);

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"] ?? "Ss.CmbXft",
        ValidAudience = builder.Configuration["Jwt:Audience"] ?? "Ss.CmbXft.Client",
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization();

// 添加CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();


// 全局异常处理（必须放在管道最前面，捕获所有异常）
app.UseGlobalExceptionMiddleware();
//app.UseValidationMiddleware();不需要 全局的接管了

// 请求/响应日志记录
app.UseMiddleware<RequestResponseLoggingMiddleware>();

// 使用 Serilog 请求日志记录
app.UseSerilogRequestLogging(options =>
{
    options.MessageTemplate = "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms";
});

// 配置HTTP请求管道
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

// 使用当前租户中间件（必须在认证和授权之前）
app.UseCurrentTenant();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


try
{
    Log.Information("Starting web host");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
