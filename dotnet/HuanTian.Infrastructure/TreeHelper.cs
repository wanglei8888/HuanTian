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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HuanTian.Infrastructure
{
    /// <summary>
    /// TreeHelper 的摘要说明
    /// </summary>
    public class TreeHelper<TEntity> where TEntity : ITreeBuild
    {
        //public void BuildTree(List<TEntity> tree)
        //{
        //    foreach (var item in tree)
        //    {
        //        item.get
        //    }
        //}
    }
    public interface ITreeBuild
    {
        public long GetId();

        public long GetParentId();


    }
}