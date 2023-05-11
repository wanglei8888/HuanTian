#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023  NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：DESKTOP-4P8G8RH
 * 公司名称：
 * 命名空间：HuanTian.Service.SysService
 * 唯一标识：ac64e7a2-f8dc-4e62-9f81-d43263981270
 * 文件名：RoleService
 * 当前用户域：DESKTOP-4P8G8RH
 * 
 * 创建者：wanglei
 * 电子邮箱：271976304@qq.com
 * 创建时间：2023/4/16 15:42:00
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
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HuanTian.Service
{
    /// <summary>
    /// 用户权限
    /// </summary>
    public class RoleService : IRoleService, IDynamicApiController
    {
        /// <summary>
        /// 获取用户权限
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public dynamic UserRole()
        {
            var jsonString = File.ReadAllText(Path.Combine(App.WebHostEnvironment.WebRootPath, "UserRole.json"));
            var role = JsonConvert.DeserializeObject<List<UserPermission>>(jsonString);
            var pageData = new PageData();
            pageData.Data = role;
            pageData.PageNo = 1;
            pageData.PageSize = 10;
            pageData.TotalCount = 5;
            return role;
        }
    }
}
