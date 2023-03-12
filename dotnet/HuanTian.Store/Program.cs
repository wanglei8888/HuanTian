using Autofac;
using Autofac.Extensions.DependencyInjection;
using HuanTian.Common;
using HuanTian.EntityFrameworkCore.MySql;
using HuanTian.WebCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Huangtian.Store
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddSingleton(new Appsettings(builder.Configuration));
            builder.Services.AddControllers().AddInject();
            
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
           
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

            #region 全局过滤器  
            builder.Services.Configure<MvcOptions>(options =>
               {
                   options.Filters.Add<DataValidationFilter>();
                   options.Filters.Add<HandlingExceptionFilter>();
               });
            #endregion

            #region Sql注入
            // my sql
            var ConnectionStrings = Appsettings.Configuration["ConnectionStrings:MySqlConnection"];
            builder.Services.AddDbContext<EfSqlContext>(options => options.UseMySql(ConnectionStrings,
                ServerVersion.AutoDetect(ConnectionStrings)));

            // sql server
            //var ConnectionStrings = Appsettings._configuration["ConnectionStrings:SqlServerConnection"];
            //builder.Services.AddDbContext<EfSqlContext>(options => options.UseSqlServer(ConnectionStrings));
            #endregion

            #region AutoMapper
            builder.Services.AddAutoMapperService();
            #endregion

            #region Autofac
            builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
            {
                containerBuilder.RegisterModule<AutofacRegister>(); // Autofac
            });
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            #endregion

            #region JWT
            builder.Services.AddJwt();
            #endregion

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
