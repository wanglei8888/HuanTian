using System.Reflection;
using System.Text;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using HuanTian.Common;
using HuanTian.EntityFrameworkCore.MySql;
using HuanTian.EntityFrameworkCore.SqlServer;
using HuanTian.WebCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NPOI.HPSF;

namespace Huangtian.Store
{
    #region test
    //public class Program
    //{
    //    public static void Main(string[] args)
    //    {

    //        var builder = WebApplication.CreateBuilder(args);

    //        // Add services to the container.

    //        builder.Services.AddControllers();
    //        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    //        builder.Services.AddEndpointsApiExplorer();
    //        builder.Services.AddSwaggerGen();

    //        #region Appsettings注入
    //        builder.Services.AddSingleton(new Appsettings(builder.Configuration));
    //        #endregion

    //        var ConnectionStrings = Appsettings.GetInfo("ConnectionStrings:MySqlConnection");
    //        builder.Services.AddDbContext<MySqlContext>(options => options.UseMySql(ConnectionStrings,
    //            ServerVersion.AutoDetect(ConnectionStrings)));
    //        //services.AddControllers();

    //        builder.Services.AddAutoMapperService();

    //        var startup = new Startup();
    //        startup.ConfigureServices(builder.Services);
    //        CreateHostBuilder(args).Build().Run();
    //        var app = builder.Build();
    //        startup.Configure(app, builder.Environment);
    //        // Configure the HTTP request pipeline.
    //        if (app.Environment.IsDevelopment())
    //        {
    //            app.UseSwagger();
    //            app.UseSwaggerUI();
    //        }

    //        app.UseHttpsRedirection();

    //        app.UseAuthorization();

    //        app.MapControllers();

    //        app.Run();
    //    }

    //    public static IHostBuilder CreateHostBuilder(string[] args) =>
    //        Host.CreateDefaultBuilder(args)
    //            .ConfigureWebHostDefaults(webBuilder =>
    //            {
    //                webBuilder.UseStartup<Startup>();
    //            })
    //        .UseServiceProviderFactory(new AutofacServiceProviderFactory());
    //}
    #endregion
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
           
            builder.Services.AddSwaggerGen(t =>
            {
                t.SwaggerDoc("v1", new OpenApiInfo { Title = "皇天商店" });
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                t.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
                t.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Value: Bearer {token}",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                t.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {{
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }, Scheme = "oauth2", Name = "Bearer", In = ParameterLocation.Header }, new List<string>()
                    }

                    });
            });

            //配置跨域服务
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("cors", p =>
                {
                    p.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();

                });
            });

            #region Sql注入
            // my sql
            var ConnectionStrings = Appsettings._configuration["ConnectionStrings:MySqlConnection"];
            builder.Services.AddDbContext<MySqlContext>(options => options.UseMySql(ConnectionStrings,
                ServerVersion.AutoDetect(ConnectionStrings)));

            // sql server
            // var ConnectionStrings = Appsettings._configuration["ConnectionStrings:SqlServerConnection"];
            // builder.Services.AddDbContext<SqlServerContext>(options => options.UseSqlServer(ConnectionStrings));
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

            //builder.Host.ConfigureLogging(loggingBuilder =>
            //{
            //    loggingBuilder.ClearProviders();
            //    loggingBuilder.AddConsole();
            //    //一行代码即可引入
            //    loggingBuilder.AddFile();
            //});

            var app = builder.Build();

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

            //app.MapControllers();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.Run();
        }
    }
}
