using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuanTian.Entities
{
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
}
