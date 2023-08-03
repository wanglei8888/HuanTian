#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023  NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：DESKTOP-6BOHDBE
 * 公司名称：
 * 命名空间：HuanTian.Infrastructure.Inteface
 * 唯一标识：072049b8-9b6f-4ce0-ae12-12648c894435
 * 文件名：IReposityoryInit
 * 当前用户域：DESKTOP-6BOHDBE
 * 
 * 创建者：wanglei
 * 电子邮箱：271976304@qq.com
 * 创建时间：2023/5/13 17:49:27
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
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace HuanTian.Infrastructure
{
    /// <summary>
    /// IReposityoryInit 的摘要说明
    /// </summary>
    public interface IReposityoryInit<TEntity> where TEntity : class, new()
    {
        /// <summary>
        /// 调用实体方法
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        IReposityoryInit<TEntity> CallEntityMethod(Expression<Action<TEntity>> method);
        /// <summary>
        /// 忽略指定列
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        IReposityoryInit<TEntity> IgnoreColumns(Expression<Func<TEntity, object>> expression);
        /// <summary>
        /// 新增数据
        /// </summary>
        /// <returns></returns>
        Task<int> AddAsync();
        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="entity"></param>
        Task<int> UpdateAsync();
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="entity"></param>
        Task<int> DeleteAsync();
    }
}