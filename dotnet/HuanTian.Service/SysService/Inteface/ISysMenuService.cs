namespace HuanTian.Service
{
    /// <summary>
    /// 用户菜单服务
    /// </summary>
    public interface ISysMenuService
    {
        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IEnumerable<SysMenuDO>> Get(SysMenuTypeInput input);
        /// <summary>
        /// 获取角色的菜单列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IEnumerable<SysMenuDO>> RoleMenu(SysRoleMenuTypeInput input);
        /// <summary>
        /// 获取用户菜单
        /// </summary>
        /// <returns></returns>
        public Task<List<SysMenuOutput>> GetUserMenu(SysUserMenyInput input);
    }
}
