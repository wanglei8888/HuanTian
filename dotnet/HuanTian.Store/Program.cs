﻿using Autofac;
using Autofac.Extensions.DependencyInjection;
using HuanTian.Common;
using HuanTian.EntityFrameworkCore;
using HuanTian.Interface;
using HuanTian.SqlSugar;
using HuanTian.WebCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;

using System.Reflection;

namespace Huangtian.Store
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //依赖注入集合
            builder.Services.AddInject(builder.Configuration);
            //动态Congtrole注入
            builder.Services.AddControllers().AddInject(Assembly.GetExecutingAssembly().GetName().Name);

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
                   options.Filters.Add(new AuthorizeFilter());
                   //options.Filters.Add<IStartupFilter, StartupFilter>();
               });
            #endregion

            #region 数据库注入

            var dbType = SqlSugar.DbType.MySql;
            // EntityFrameworkCore
            builder.Services.AddEntityFrameworkService(dbType);
            // SqlSugar
            builder.Services.AddSqlSugarService(dbType);

            #endregion
            
            #region Autofac
            builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
            {
                containerBuilder.RegisterModule<AutofacRegister>(); // Autofac
            });
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            #endregion

            builder.Services.AddAutoMapperService();

            builder.Services.AddJwt();

            var app = builder.Build();

            //自定义中间件
            app.UseMiddleware<CustomMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
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

            app.Run();
        }
    }
}
