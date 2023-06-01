#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023  NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：DESKTOP-4P8G8RH
 * 公司名称：
 * 命名空间：HuanTian.Entities.DtoModel
 * 唯一标识：67682b04-f2d0-4d5e-a354-d9c886cb9ad3
 * 文件名：TestData
 * 当前用户域：DESKTOP-4P8G8RH
 * 
 * 创建者：wanglei
 * 电子邮箱：271976304@qq.com
 * 创建时间：2023/4/16 16:56:46
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

namespace HuanTian.Entities
{
    public class Permission
    {
        /// <summary>
        /// 权限名称
        /// </summary>
        public string PermissionName { get; set; }
        public string Actions { get; set; }
        /// <summary>
        /// 动作列表
        /// </summary>
        public List<ActionEntity> actionEntitySet { get; set; }
    }

    public class ActionEntity
    {
        /// <summary>
        /// 动作
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Describe { get; set; }

        /// <summary>
        /// 默认选中
        /// </summary>
        public bool DefaultCheck { get; set; }
    }

    public class Role
    {
        /// <summary>
        /// ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Describe { get; set; }
        /// <summary>
        /// 启用
        /// </summary>
        public bool Enable { get; set; }

        /// <summary>
        /// 权限列表
        /// </summary>
        public List<Permission> Permissions { get; set; }
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
        public Role_Test Role { get; set; }
    }

    public class Role_Test
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

    public class RoleActionEntity_Test
    {
        /// <summary>
        /// 动作名称
        /// </summary>
        public string action { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string describe { get; set; }

        /// <summary>
        /// 默认选中
        /// </summary>
        public bool defaultCheck { get; set; }
    }
    public class ProjectItem_Test
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 封面图片链接
        /// </summary>
        public string Cover { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdatedAt { get; set; }
    }
    /// <summary>
    /// 日志实体类
    /// </summary>
    public class ActivityData_Test
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 用户信息
        /// </summary>
        public User_Test_2 User { get; set; }

        /// <summary>
        /// 项目信息
        /// </summary>
        public Project_Test Project { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public string Time { get; set; }
    }

    /// <summary>
    /// 用户信息实体类
    /// </summary>
    public class User_Test_2
    {
        /// <summary>
        /// 昵称
        /// </summary>
        public string Nickname { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }
    }
    public class Team_Test
    { 
        public string Id { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
    }
    /// <summary>
    /// 项目信息实体类
    /// </summary>
    public class Project_Test
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 操作
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// 事件
        /// </summary>
        public string Event { get; set; }
    }
    public class RadarDataItem_Test
    {
        public string Item { get; set; }
        public int People { get; set; }
        public int Group { get; set; }
        public int Department { get; set; }
    }
    public class BoxInfoOutput
    {
        /// <summary>
        /// 料号
        /// </summary>
        public string PartNumber { get; set; }
        public string Version { get; set; }
        public string ProductName { get; set; }
        public string Quantity { get; set; }
        public string ProductionDate { get; set; }
        public string Supplier { get; set; }
        /// <summary>
        /// 二维码
        /// </summary>
        public string QRCode { get; set; }
    }
}
