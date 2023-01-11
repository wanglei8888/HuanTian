using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using HuanTian.Common;
using HuanTian.EntityFrameworkCore.MySql;
using HuanTian.WebCore;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

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

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddSingleton(new Appsettings(builder.Configuration));
            builder.Services.AddEndpointsApiExplorer();
            
            builder.Services.AddSwaggerGen(t => {
                t.SwaggerDoc("v1", new OpenApiInfo { Title = "皇天商店" });
            });

            #region Sql注入
            var ConnectionStrings = Appsettings.GetInfo("ConnectionStrings:MySqlConnection");
            builder.Services.AddDbContext<MySqlContext>(options => options.UseMySql(ConnectionStrings,
                ServerVersion.AutoDetect(ConnectionStrings)));
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
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = builder.Configuration["JWT:Issuer"],
                        ValidateAudience = true,
                        ValidAudience = builder.Configuration["JWT:Audience"],
                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"]))
                    };
                }); 
            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
