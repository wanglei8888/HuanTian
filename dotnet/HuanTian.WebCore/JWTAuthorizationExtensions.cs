#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023 Microsoft NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：MS-SOBOTFLKMEBA
 * 公司名称：Microsoft
 * 命名空间：HuanTian.WebCore
 * 唯一标识：6d9797c6-6a26-4e0d-abff-adac09a63a14
 * 文件名：JWTAuthorizationServiceCollectionExtensions
 * 当前用户域：MS-SOBOTFLKMEBA
 * 
 * 创建者：wanglei
 * 电子邮箱：
 * 创建时间：2023/3/9 10:20:38
 * 版本：V1.0.0
 * 描述：
 *
 * ----------------------------------------------------------------
 * 修改人：
 * 时间：
 * 修改说明：
 *
 * 版本：V1.0.1
 *----------------------------------------------------------------*/
#endregion << 版 本 注 释 >>
using HuanTian.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace HuanTian.WebCore
{
    public static class JwtAuthorizationExtensions
    {
        /// <summary>
        /// 添加 JWT 授权
        /// </summary>
        /// <param name="authenticationBuilder"></param>
        /// <param name="tokenValidationParameters">token 验证参数</param>
        /// <param name="jwtBearerConfigure"></param>
        /// <param name="enableGlobalAuthorize">启动全局授权</param>
        /// <returns></returns>
        public static IServiceCollection AddJwt(this IServiceCollection services, bool enableGlobalAuthorize = false)
        {
            // 添加授权
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer((Action<JwtBearerOptions>)(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        // 验证发布者
                        ValidateIssuer = true,
                        ValidIssuer = App.Configuration["JWTAuthentication:Issuer"],
                        // 验证接收者
                        ValidateAudience = true,
                        ValidAudience = App.Configuration["JWTAuthentication:Audience"],
                        // 验证是否过期
                        ValidateLifetime = true,
                        // 验证私钥
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes((string)App.Configuration["JWTAuthentication:SecretKey"]))

                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            // 设置jwt认证信息不需要带 Bearer
                            if (context.Request.Headers.TryGetValue(App.Configuration["AppSettings:ApiHeard"], out var token))
                            {
                                context.Token = token;
                            }
                            return Task.CompletedTask;
                        }
                    };
                }));

            //启用全局授权
            if (enableGlobalAuthorize)
            {
                services.Configure<MvcOptions>(options =>
                {
                    options.Filters.Add(new AuthorizeFilter());
                });
            }

            return services;
        }
    }
}
