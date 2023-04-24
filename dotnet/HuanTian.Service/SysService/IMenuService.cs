using HuanTian.Entities;

namespace HuanTian.Service
{
    /// <summary>
    /// 用户菜单服务
    /// </summary>
    public interface IMenuService
    {
        /// <summary>
        /// 获取用户菜单
        /// </summary>
        /// <returns></returns>
        public dynamic GetUserMenu();
    }
}
