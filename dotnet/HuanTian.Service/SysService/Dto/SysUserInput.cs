#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023  NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：WANGXIAOPANG
 * 公司名称：
 * 命名空间：HuanTian.Service.SysService.Dto
 * 唯一标识：c26b9da3-888f-4a46-bb66-cbbb56ea7ed1
 * 文件名：SysUserInput
 * 当前用户域：WANGXIAOPANG
 * 
 * 创建者：wanglei
 * 电子邮箱：271976304@qq.com
 * 创建时间：2023/7/15 23:49:18
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

namespace HuanTian.Service
{
    /// <summary>
    /// SysUserOutput
    /// </summary>
    public class SysUserOutput : SysUserDO
    {
        /// <summary>
        /// 权限列表
        /// </summary>
        public IEnumerable<Permission> Role { get; set; }
        /// <summary>
        /// 应用列表
        /// </summary>
        public IEnumerable<SysAppDO> App { get; set; }
        /// <summary>
        /// 菜单列表
        /// </summary>
        public IEnumerable<SysMenuOutput> Menu { get; set; }
    }
    public class UserRole
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 角色描述
        /// </summary>
        public string Describe { get; set; }

        /// <summary>
        /// 角色状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 创建者ID
        /// </summary>
        public string CreatorId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public long CreateTime { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public int Deleted { get; set; }

        /// <summary>
        /// 角色权限列表
        /// </summary>
        public List<Permission_Test> Permissions { get; set; }
    }

    public class User_Test
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// 用户状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string Telephone { get; set; }

        /// <summary>
        /// 最后登录IP
        /// </summary>
        public string LastLoginIp { get; set; }

        /// <summary>
        /// 最后登录时间
        /// </summary>
        public long LastLoginTime { get; set; }

        /// <summary>
        /// 创建者ID
        /// </summary>
        public string CreatorId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public long CreateTime { get; set; }

        /// <summary>
        /// 商户代码
        /// </summary>
        public string MerchantCode { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public int Deleted { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public string RoleId { get; set; }

        /// <summary>
        /// 用户角色
        /// </summary>
        public UserRole Role { get; set; }
    }

    public class Permission_Test
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        public string roleId { get; set; }

        /// <summary>
        /// 权限ID
        /// </summary>
        public string permissionId { get; set; }

        /// <summary>
        /// 权限名称
        /// </summary>
        public string permissionName { get; set; }

        /// <summary>
        /// 动作列表JSON
        /// </summary>
        public string actions { get; set; }

        /// <summary>
        /// 动作实体集合
        /// </summary>
        public List<RoleActionEntity_Test> actionEntitySet { get; set; }

        /// <summary>
        /// 动作列表
        /// </summary>
        public string actionList { get; set; }

        /// <summary>
        /// 数据访问
        /// </summary>
        public string dataAccess { get; set; }

    }
}
