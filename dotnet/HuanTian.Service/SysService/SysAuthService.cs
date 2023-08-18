#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023  NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：DESKTOP-4P8G8RH
 * 公司名称：
 * 命名空间：HuanTian.Service
 * 唯一标识：d1e9cb75-3ee7-40a9-ad70-1850a42c3d35
 * 文件名：SysAuthService
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
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace HuanTian.Service
{
    /// <summary>
    /// 登陆服务
    /// </summary>
    public class SysAuthService : ISysAuthService, IDynamicApiController
    {
        private readonly IRepository<SysUserDO> _sysUserInfo;
        private readonly IRedisCache _redisCache;
        public SysAuthService(
            IRepository<SysUserDO> sysUserInfo,
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
            if (userInfo == null || userInfo.Deleted)
                throw new Exception("账号密码错误,请修改后再试");
            if (!userInfo.Enable)
                throw new Exception("用户已被禁用,无法登陆,请联系系统管理员");
            // 储存Jwt数据
            var claims = new[]
            {
                new Claim(JwtClaimConst.UserId,EncryptionHelper.Encrypt(userInfo.Id.ToString(),CommonConst.UserToken)),
                new Claim(JwtClaimConst.TenantId,EncryptionHelper.Encrypt(userInfo.TenantId.ToString(),CommonConst.TenantToken)),
            };

            var output = new LoginOutput()
            {
                Token = JWTHelper.GetToken(claims)
            };
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
            // 登出操作，利用Redis缓存进行黑名单验证,防止失效Token依然使用
            if (App.HttpContext.Request.Headers.TryGetValue(App.Configuration["AppSettings:ApiHeard"], out var token))
            {
                await _redisCache.SetAddAsync($"LoginUserInfoWhitelist", token.ToString());
            }

        }
    }
}
