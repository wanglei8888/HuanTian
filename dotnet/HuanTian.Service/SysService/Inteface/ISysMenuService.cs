namespace HuanTian.Service
{
    /// <summary>
    /// 用户菜单服务
    /// </summary>
    public interface ISysMenuService
    {
        /// <summary>
        /// 获取菜单-树结构数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IEnumerable<SysMenuTreeOutput>> Tree([FromQuery] SysMenuTypeInput input);
        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IEnumerable<SysMenuDO>> Get(SysMenuTypeInput input);
        /// <summary>
        /// 新增菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<int> Add(SysMenuDO input);
        /// <summary>
        /// 获取角色的菜单列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IEnumerable<SysMenuDO>> RoleMenu(SysRoleMenuTypeInput input);
        /// <summary>
        /// 获取用户菜单返回前端格式
        /// </summary>
        /// <returns></returns>
        Task<List<SysMenuOutput>> GetUserMenuOutput(SysUserMenuInput input);
        /// <summary>
        /// 获取用户菜单信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IEnumerable<SysMenuDO>> GetUserMenuInfo(long input);
    }
}
