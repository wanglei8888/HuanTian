#region << 版 本 注 释 >>
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
using HuanTian.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace HuanTian.WebCore
{
    /// <summary>
    /// 鉴权过滤器 监测是否符合权限的访问
    /// </summary>
    public class AuthenticationFilter : IAsyncAuthorizationFilter
    {
        private readonly IRedisCache _redisCache;
        private readonly ISysPermissionsService _sysPermService;
        public AuthenticationFilter(IRedisCache redisCache, ISysPermissionsService sysPermService)
        {
            _redisCache = redisCache;
            _sysPermService = sysPermService;
        }
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            // 判断方法是否允许匿名访问 AllowAnonymous
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata
                .Any(metadata => metadata.GetType() == typeof(AllowAnonymousAttribute));
            if (allowAnonymous)
            {
                // 方法允许匿名访问，直接放行
                return;
            }
            //// 判断是否为有效token
            //var isAuthenticated = context.HttpContext.User.Identity.IsAuthenticated;
            //if (isAuthenticated)
            //{
            //}
            //else
            //{
            //    context.Result = new UnauthorizedResult();
            //}

            //context.Result = RequestHelper.ErroRequestEntity("用户未授权,请登录后再操作", HttpStatusCode.Unauthorized);
            //context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

            // 检测用户的token是否为黑名单用户,如果是则返回401
            if (!allowAnonymous && App.HttpContext.Request.Headers.TryGetValue(App.Configuration["AppSettings:ApiHeard"] ?? "", out var token))
            {
                var userId = App.HttpContext.User.Claims.FirstOrDefault(u => u.Type == System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sid)?.Value;
                if ((await _redisCache.SetContainsAsync($"LoginUserInfoWhitelist", token.ToString())))
                {
                    context.Result = RequestHelper.RequestInfo("用户未授权,请登录后再操作", HttpStatusCode.Unauthorized);
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                }
            }
            // 去掉路由前缀获取路由
            var prefix = App.Configuration["DynamicApiControllerSettings:DefaultRoutePrefix"];
            var prefixLength = prefix?.Length == 0 ? 0 : prefix?.Length + 2;
            var path = context.HttpContext.Request.Path.Value.ToLower().Substring(prefixLength.Value);
            // 检测是否为有效请求 从缓存中获取用户路由与当前路由进行匹配
            var userRoute = (await _sysPermService.UserPermission(App.GetUserId())).Where(t=>t.Type == PermissionTypeEnum.Router);
            //if (!userRoute.Any(t => t.Code.ToLower() == path))
            //{
            //    context.Result = RequestHelper.RequestInfo("用户未授权页面,请修改后再操作", HttpStatusCode.Unauthorized);
            //    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            //}
        }
    }
}
