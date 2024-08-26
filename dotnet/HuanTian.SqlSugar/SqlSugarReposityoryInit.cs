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

using HuanTian.Infrastructure;
using Microsoft.Extensions.Localization;
using SqlSugar;
using System.Linq.Expressions;

namespace HuanTian.SqlSugar
{
    /// <summary>
    /// SqlSugar Init仓储
    /// <para>更多详细信息,请访问<a href="https://www.donet5.com/Home/Doc">官网</a></para>
    /// </summary>
    /// <typeparam name="TEntity">实体表格</typeparam>
    public class SqlSugarReposityoryInit<TEntity> : IReposityoryInit<TEntity> where TEntity : class, new()
    {
        private readonly IEnumerable<TEntity> _entityList;
        private readonly Expression<Func<TEntity, object>> _ignoredColumns;
        private readonly ISqlSugarClient _db;
        public SqlSugarReposityoryInit(ISqlSugarClient db)
        {
            _db = db;
        }
        public SqlSugarReposityoryInit(ISqlSugarClient db, IEnumerable<TEntity> entityList)
            : this(db)
        {
            _entityList = entityList;
        }
        public SqlSugarReposityoryInit(ISqlSugarClient db, IEnumerable<TEntity> entityList, Expression<Func<TEntity, object>> ignoredColumns)
            : this(db, entityList)
        {
            _ignoredColumns = ignoredColumns;
        }

        public async Task<int> AddAsync()
        {
            return await _db.Insertable(_entityList.ToList()).ExecuteCommandAsync();
        }

        public async Task<int> DeleteAsync()
        {
            return await _db.Deleteable(_entityList.ToList()).ExecuteCommandAsync();
        }

        public async Task<int> UpdateAsync()
        {
            var updateable = _db.Updateable(_entityList.ToList());
            if (_ignoredColumns != null)
            {
                updateable = updateable.IgnoreColumns(_ignoredColumns);
            }
            return await updateable.ExecuteCommandAsync();
        }

        public IReposityoryInit<TEntity> CallEntityMethod(Expression<Action<TEntity>> method)
        {
            if (!(method.Body is MethodCallExpression callExpresion))
            {
                throw new ArgumentException(App.I18n.GetString("表达式必须是方法调用"));
            }
            if (_entityList == null)
            {
                throw new ArgumentException(App.I18n.GetString("使用CallEntityMethod必须先调用InitTable"));
            }
            var action = method.Compile();
            foreach (var item in _entityList)
            {
                if (item != null)
                {
                    action.Invoke(item);
                }
            }
            return this;
        }

        public IReposityoryInit<TEntity> IgnoreColumns(Expression<Func<TEntity, object>> expression)
        {
            // SqlSugar忽略列支持多个属性,所以储存就行了
            var newExpression = expression.Body as NewExpression;
            if (newExpression == null)
            {
                throw new ArgumentException("Expression must be a NewExpression.", nameof(expression));
            }
            return new SqlSugarReposityoryInit<TEntity>(_db, _entityList, expression);
        }

    }
}