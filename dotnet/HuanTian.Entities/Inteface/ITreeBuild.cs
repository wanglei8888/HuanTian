#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023  NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：DESKTOP-6BOHDBE
 * 公司名称：
 * 命名空间：HuanTian.Entities.Inteface
 * 唯一标识：d9c4f11d-da32-4dc7-8e59-e46a1d1b0470
 * 文件名：Class1
 * 当前用户域：DESKTOP-6BOHDBE
 * 
 * 创建者：wanglei
 * 电子邮箱：271976304@qq.com
 * 创建时间：2023/5/15 21:51:30
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

using System.Collections;


namespace HuanTian.Entities
{
    /// <summary>
    /// 树结构数据接口
    /// </summary>
    public interface ITreeBuild
    {
        /// <summary>
        /// 获取ID
        /// </summary>
        /// <returns></returns>
        public long GetId();
        /// <summary>
        /// 获取父ID
        /// </summary>
        /// <returns></returns>
        public long GetParentId();
        /// <summary>
        /// 设置子类
        /// </summary>
        /// <param name="children"></param>
        public void SetChildren(IList children);
    }
}