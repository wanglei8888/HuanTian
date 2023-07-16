#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023  NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：DESKTOP-6BOHDBE
 * 公司名称：
 * 命名空间：HuanTian.Infrastructure
 * 唯一标识：6baea456-26b7-46f9-8fba-774214bfc7c7
 * 文件名：TreeHelper
 * 当前用户域：DESKTOP-6BOHDBE
 * 
 * 创建者：wanglei
 * 电子邮箱：271976304@qq.com
 * 创建时间：2023/5/15 19:56:39
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


using HuanTian.Infrastructure;

namespace HuanTian.Infrastructure
{
    /// <summary>
    /// 构建树
    /// </summary>
    public static class TreeHelper<TEntity> where TEntity : ITreeBuild
    {
        /// <summary>
        /// 构造树节点
        /// </summary>
        /// <param name="nodes"></param>
        /// <returns></returns>
        public static List<TEntity> DoTreeBuild(List<TEntity> nodes)
        {
            nodes.ForEach(u => BuildChildNodes(nodes, u, new List<TEntity>()));

            var results = new List<TEntity>();
            nodes.ForEach(u =>
            {
                if (0 == u.GetParentId())
                    results.Add(u);
            });
            return results;
        }
        /// <summary>
        /// 构造子节点集合
        /// </summary>
        /// <param name="totalNodes"></param>
        /// <param name="node"></param>
        /// <param name="childNodeLists"></param>
        private static void BuildChildNodes(List<TEntity> totalNodes, TEntity node, List<TEntity> childNodeLists)
        {
            var nodeSubLists = new List<TEntity>();
            totalNodes.ForEach(u =>
            {
                if (u.GetParentId().Equals(node.GetId()))
                    nodeSubLists.Add(u);
            });
            nodeSubLists.ForEach(u => BuildChildNodes(totalNodes, u, new List<TEntity>()));
            childNodeLists.AddRange(nodeSubLists);
            node.SetChildren(childNodeLists);
        }
    }
}