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
using HuanTian.WebCore;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HuanTian.Service.SysService
{
    /// <summary>
    /// 用户权限
    /// </summary>
    public class RoleService: IRoleService,IDynamicApiController
    {
        [HttpGet]
        public dynamic Get()
        {
            var jsonString = "{\"id\":\"1\",\"name\":\"管理员\",\"describe\":\"拥有所有权限\",\"status\":1,\"creatorId\":\"system\",\"createTime\":\"2022-03-01\",\"deleted\":0,\"permissions\":[{\"roleId\":\"admin\",\"permissionId\":\"comment\",\"permissionName\":\"评论管理\",\"actions\":\"[{\\\"action\\\":\\\"add\\\",\\\"defaultCheck\\\":false,\\\"describe\\\":\\\"新增\\\"},{\\\"action\\\":\\\"query\\\",\\\"defaultCheck\\\":false,\\\"describe\\\":\\\"查询\\\"},{\\\"action\\\":\\\"get\\\",\\\"defaultCheck\\\":false,\\\"describe\\\":\\\"详情\\\"},{\\\"action\\\":\\\"edit\\\",\\\"defaultCheck\\\":false,\\\"describe\\\":\\\"修改\\\"},{\\\"action\\\":\\\"delete\\\",\\\"defaultCheck\\\":false,\\\"describe\\\":\\\"删除\\\"}]\",\"actionList\":[\"delete\",\"edit\"]}]}";
            var role = JsonConvert.DeserializeObject<RolePermissionDto>(jsonString);
            return role;
        }
    }
}
