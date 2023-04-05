#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023  NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：DESKTOP-4P8G8RH
 * 公司名称：
 * 命名空间：HuanTian.Service
 * 唯一标识：1c802320-32e2-493b-81f8-b5a82b66b275
 * 文件名：UserService
 * 当前用户域：DESKTOP-4P8G8RH
 * 
 * 创建者：wanglei
 * 电子邮箱：271976304@qq.com
 * 创建时间：2023/4/5 21:03:30
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
using HuanTian.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.JsonWebTokens;

namespace HuanTian.Service.SysService
{
    public class UserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly EfSqlContext _mySqlContext;
        private readonly IMneuService _mneuService;

        public UserService(
            ILogger<UserService> logger,
            EfSqlContext mySqlContext,
            IMneuService mneuService)
        {
            _logger = logger;
            _mySqlContext = mySqlContext;
            _mneuService = mneuService;
        }

        //[Authorize]
        [HttpGet]
        public async Task<IEnumerable<MenuOutput>> Info()
        {

            //var user = HttpContext.User.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sid)?.Value;
            //var menu = await _mneuService.GetUserMenu(1);
            //return menu;
            return null;
        }
    }
}
