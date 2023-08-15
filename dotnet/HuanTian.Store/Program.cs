using Autofac;
using HuanTian.Infrastructure;
using HuanTian.Service;
using HuanTian.SqlSugar;
using HuanTian.WebCore;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

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
            builder.Services.AddControllers(options =>
            {
            }).AddInject();

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
            //开发环境不需要写入日志
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
            // .NET Core 的原生 DI 容器中不允许作用域注入在单例服务中
            //builder.Services.AddScoped(typeof(IRepository<>), typeof(SqlSugarRepository<>));
            //builder.Services.AddSingleton<IStartupFilter, StartupFilter>();
            //builder.Services.AddAutoInjection();
            #endregion

            // 注册Redis缓存服务
            builder.Services.AddSingleton<IRedisCache>(provider =>
                new RedisCache(builder.Configuration["ConnectionStrings:Redis"]));
            // 注册RabbitMQ服务
            builder.Services.AddScoped<IMessageQueue>(provider =>
               new RabbitMQMessageQueue(builder.Configuration["ConnectionStrings:RabbitMQ"]));
            //builder.Services.AddScoped<IQueryFilter, QueryFilter>();
            // 注册JWT服务
            builder.Services.AddJwt(true);
            // 注册Http服务
            builder.Services.AddHttpContextAccessor();

            var app = builder.Build();
            // 自定义中间件
            app.UseMiddleware<CustomMiddleware>();

            App.WebHostEnvironment = app.Environment; // 静态类存储

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(t => { SwaggerExtensions.BuildSwaggerUI(t, ""); });
            }

            app.UseCors("cors");
            app.UseRouting();
            app.UseHttpsRedirection();

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
