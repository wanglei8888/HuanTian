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

namespace HuanTian.Entities.DtoModel
{
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
        public User_Test User { get; set; }

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
    public class User_Test
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
}
