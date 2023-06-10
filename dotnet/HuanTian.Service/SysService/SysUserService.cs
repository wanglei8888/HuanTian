#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023  NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：DESKTOP-4P8G8RH
 * 公司名称：
 * 命名空间：HuanTian.Service
 * 唯一标识：1c802320-32e2-493b-81f8-b5a82b66b275
 * 文件名：SysUserService
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

using Newtonsoft.Json;
using SqlSugar.Extensions;

namespace HuanTian.Service
{
    /// <summary>
    /// 用户信息服务
    /// </summary>
    public class SysUserService : ISysUserService, IDynamicApiController
    {
        private readonly ILogger<SysUserService> _logger;
        private readonly IRepository<SysUserDO> _userInfo;

        public SysUserService(
            ILogger<SysUserService> logger,
            IRepository<SysUserDO> userInfo)
        {
            _logger = logger;
            _userInfo = userInfo;
        }
        /// <summary>
        /// 获取用户信息跟用户权限信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public dynamic Info()
        {
            var user = App.GetUserId().ToString();
            // 解密
            user = EncryptionHelper.Decrypt(user, CommonConst.UserToken);

            var jsonString = File.ReadAllText(Path.Combine(App.WebHostEnvironment.WebRootPath, "UserInfo.json"));
            var userInfo = JsonConvert.DeserializeObject<User_Test>(jsonString);
            
            return userInfo;
        }
        /// <summary>
        /// 查询用户列表信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<dynamic> Page([FromQuery] UserInput input)
        {
            var pageData = await _userInfo
                .WhereIf(!string.IsNullOrEmpty(input.Name),t => t.Name == input.Name)
                .WhereIf(!string.IsNullOrEmpty(input.Enable), t => t.Enable == input.Enable.ObjToBool())
                .ToPageListAsync(input.PageNo,input.PageSize);
            return pageData;
        }
    }
}
