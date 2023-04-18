﻿#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023  NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：DESKTOP-4P8G8RH
 * 公司名称：
 * 命名空间：HuanTian.WebCore.Filter
 * 唯一标识：9787950f-94b1-4cd9-9d33-7925870c3dac
 * 文件名：Class1
 * 当前用户域：DESKTOP-4P8G8RH
 * 
 * 创建者：wanglei
 * 电子邮箱：271976304@qq.com
 * 创建时间：2023/4/15 20:58:56
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
using Microsoft.AspNetCore.Mvc.Filters;

namespace HuanTian.WebCore
{
    /// <summary>
    /// 鉴权过滤器 监测是否符合权限的访问
    /// </summary>
    public class AuthenticationFilter : IAsyncAuthorizationFilter 
    {
        public Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            //// 判断方法是否允许匿名访问 AllowAnonymous
            //var allowAnonymous = context.ActionDescriptor.EndpointMetadata
            //    .Any(metadata => metadata.GetType() == typeof(AllowAnonymousAttribute));
            //if (allowAnonymous)
            //{
            //    // 方法允许匿名访问，直接放行
            //    return;
            //}
            //// 判断是否为有效token
            //var isAuthenticated = context.HttpContext.User.Identity.IsAuthenticated;
            //if (isAuthenticated)
            //{

            //}
            //else
            //{
            //    context.Result = new UnauthorizedResult();
            //}

            var asd = context.HttpContext.User.Claims.FirstOrDefault(u => u.Type == System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sid)?.Value;
            return Task.CompletedTask;
        }
    }
}