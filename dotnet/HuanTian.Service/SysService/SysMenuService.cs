namespace HuanTian.Service
{
    /// <summary>
    /// 菜单服务
    /// </summary>
    public class SysMenuService : ISysMenuService, IDynamicApiController, IScoped
    {
        private readonly IRepository<SysMenuDO> _menu;
        public SysMenuService(IRepository<SysMenuDO> menu)
        {
            _menu = menu;
        }
        public async Task<List<SysMenuOutput>> GetUserMenu()
        {
            var allMenu = await _menu.ToListAsync();
            var menuInfo = allMenu.Adapt<List<SysMenuOutput>>();
            return menuInfo;
        }
        [HttpGet]
        public async Task<List<SysMenuTreeOutput>> Page([FromQuery] SysMenuInput input)
        {
            var allMenu = await _menu.ToListAsync();
            // 构建树结构
            var tree = new List<SysMenuTreeOutput>();
            BuildTree(allMenu.ToList(), tree);
            return tree;
        }
        [NonAction]
        // 写一个系统菜单的递归构成树结构
        private List<SysMenuTreeOutput> BuildTree(List<SysMenuDO> allMenu,List<SysMenuTreeOutput> tree, long parentId = 0)
        {
            
            foreach (var item in allMenu)
            {
                if (item.ParentId == parentId)
                {
                    var treeNode = new SysMenuTreeOutput();
                    treeNode = item.Adapt<SysMenuTreeOutput>();
                    treeNode.Children = new List<SysMenuTreeOutput>();
                    // 递归获取子节点
                    BuildTree(allMenu, treeNode.Children, item.Id);
                    // 添加到当前节点的子节点列表中
                    tree.Add(treeNode);
                }
            }
            return tree;
        }


    }
}