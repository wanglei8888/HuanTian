namespace HuanTian.Service
{
    /// <summary>
    /// 用户菜单服务
    /// </summary>
    public interface ISysMenuService
    {
        /// <summary>
        /// 获取用户菜单
        /// </summary>
        /// <returns></returns>
        public Task<List<SysMenuOutput>> GetUserMenu();
    }
}
