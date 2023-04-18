#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023  NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：DESKTOP-4P8G8RH
 * 公司名称：
 * 命名空间：HuanTian.Service
 * 唯一标识：d1e9cb75-3ee7-40a9-ad70-1850a42c3d35
 * 文件名：AuthService
 * 当前用户域：DESKTOP-4P8G8RH
 * 
 * 创建者：wanglei
 * 电子邮箱：271976304@qq.com
 * 创建时间：2023/4/5 20:59:47
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
using HuanTian.Entities;
using HuanTian.Infrastructure;
using HuanTian.WebCore;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HuanTian.Service
{
    /// <summary>
    /// 登陆服务
    /// </summary>
    public class AuthService: IAuthService,IDynamicApiController
    {
        private readonly IRepository<SysUserInfoDO> _sysUserInfo;
        private readonly IRedisCache _redisCache;
        public AuthService(
            IRepository<SysUserInfoDO> sysUserInfo,
            IRedisCache redisCache)  
        {
            _sysUserInfo = sysUserInfo;
            _redisCache = redisCache;
        }
        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<dynamic> Login(LoginInput input)
        {

            var userInfo = await _sysUserInfo.FirstOrDefaultAsync(t => t.UserName == input.UserName && t.Password == EncryptionHelper.SHA1(input.Password));
            if (userInfo == null) throw new Exception("用户账号密码错误");

            var output = new LoginOutput();
            output.Token = JWTHelper.GetToken(EncryptionHelper.Encrypt(userInfo.Id.ToString(),CommonConst.UserToken));

            return output;
        }
        /// <summary>
        /// 用户登出
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task Logout()
        {
            // 登出操作，利用Redis缓存进行白名单验证,防止失效Token依然使用
            if (App.HttpContext.Request.Headers.TryGetValue(App.Configuration["AppSettings:ApiHeard"], out var token))
            {
                var userId = App.HttpContext.User.Claims.FirstOrDefault(u => u.Type == System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sid)?.Value;
                await _redisCache.SetAsync($"LoginUserInfoWhitelist-{userId}-{token}", token, TimeSpan.FromHours(1));
            }
            
        }
    }
}
