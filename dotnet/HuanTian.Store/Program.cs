using Autofac;
using Hangfire.HttpJob.Agent.MysqlConsole;
using HuanTian.WebCore;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace Huangtian.Store
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 静态类存储
            builder.Services.AddInject(builder.Configuration);
            // 动态Congtrole注入
            builder.Services.AddControllers().Services.AddDynamicApiControllers();
            builder.Services.AddSwaggerGen(options => SwaggerExtensions.BuildSwaggerService(options));
            builder.Services.AddEndpointsApiExplorer();

            #region 配置跨域服务
            builder.Services.AddCors(options =>
               {
                   options.AddPolicy("cors", p =>
                   {
                       p.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();

                   });
               });
            #endregion

            #region 日志服务
            // 开发环境不写入日志
#if !DEBUG
            builder.Logging.AddLocalFileLogger(options => options.SaveDays = 7);
#endif
            #endregion

            #region 全局过滤器  
            builder.Services.Configure<MvcOptions>(options =>
               {
                   options.Filters.Add<TemplateResultFilter>();
                   options.Filters.Add<HandlingExceptionFilter>();
                   options.Filters.Add<AuthenticationFilter>();
                   options.Filters.Add<AsyncActionFilter>();
               });
            #endregion

            #region 数据库注入

            // EntityFrameworkCore
            builder.Services.AddEntityFrameworkService();
            // SqlSugar
            builder.Services.AddSqlSugarService();
            #endregion

            #region Autofac - 自动依赖注入
            // Autofac
            builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
            {
                containerBuilder.RegisterModule(new AutofacRegister()); // Autofac
            });
            builder.Host.UseServiceProviderFactory(new Autofac.Extensions.DependencyInjection.AutofacServiceProviderFactory());

            // 自动依赖注入,手写的 如果不符合需求，可以注释，使用autofac
            // .NET Core 的原生 DI 容器中不允许作用域、瞬发注入在单例服务中
            // builder.Services.AddAutoInjection();
            #endregion

            // 添加依赖注入服务
            builder.Services.AddDependencyInject(builder.Configuration);
            // 注册JWT服务
            builder.Services.AddJwt(true);
            // 注册Http服务
            builder.Services.AddHttpContextAccessor();
           
            builder.Services.AddHangfireJobAgent();

            var app = builder.Build();
           
            app.UseHangfireJobAgent();

            // 自定义中间件
            app.UseMiddleware<CustomMiddleware>();
            // 注册生命周期方法
            app.RegisterHostApplicationLifetime();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(t => { SwaggerExtensions.BuildSwaggerUI(t, ""); });
            }

            app.UseCors("cors");
            // 中英文支持
            var supportedCultures = new List<CultureInfo>
            {
                new CultureInfo("en-US"),
                new CultureInfo("zh-CN"),
            };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("zh-CN"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            app.UseRouting();
            // app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseStaticFiles();
            app.Run();
        }
    }

}
