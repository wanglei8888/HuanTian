#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023  NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：DESKTOP-6BOHDBE
 * 公司名称：
 * 命名空间：HuanTian.SqlSugar
 * 唯一标识：786fcc8e-90b3-46db-aac8-4c8373196d7f
 * 文件名：SqlSugarReposityoryInit
 * 当前用户域：DESKTOP-6BOHDBE
 * 
 * 创建者：wanglei
 * 电子邮箱：271976304@qq.com
 * 创建时间：2023/5/13 18:11:22
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

using HuanTian.Entities;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace HuanTian.SqlSugar
{
    /// <summary>
    /// SqlSugar Init仓储
    /// </summary>
    public class SqlSugarReposityoryInit<TEntity> : IReposityoryInit<TEntity> where TEntity : class, new()
    {
        private List<TEntity>? _entityList;

        private readonly ISqlSugarClient _db;
        public SqlSugarReposityoryInit(ISqlSugarClient db)
        {
            _db = db;
        }
        public SqlSugarReposityoryInit(ISqlSugarClient db, List<TEntity> entityList)
            :this(db)
        {
            _entityList = entityList;
        }

        public async Task<int> AddAsync()
        {
            return await _db.Insertable(_entityList).ExecuteCommandAsync();
        }

        public async Task<int> DeleteAsync()
        {
            return await _db.Deleteable(_entityList).ExecuteCommandAsync();
        }

        public async Task<int> UpdateAsync()
        {
            return await _db.Updateable(_entityList).ExecuteCommandAsync();
        }

        public IReposityoryInit<TEntity> CallEntityMethod(Expression<Action<TEntity>> method)
        {
            if (!(method.Body is MethodCallExpression callExpresion))
            {
                throw new ArgumentException("Expression must be a method call.", nameof(method));
            }
            if (_entityList == null)
            {
                throw new ArgumentException("使用CallEntityMethod必须先调用InitTable", nameof(method));
            }
            foreach (var item in _entityList)
            {
                callExpresion.Method.Invoke(item, null);
            }

            return this;
        }
    }
}