#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023  NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：DESKTOP-6BOHDBE
 * 公司名称：
 * 命名空间：HuanTian.EntityFrameworkCore
 * 唯一标识：961b1356-3c60-4705-8346-2daa3a15a4c2
 * 文件名：EfReposityoryInit
 * 当前用户域：DESKTOP-6BOHDBE
 * 
 * 创建者：wanglei
 * 电子邮箱：271976304@qq.com
 * 创建时间：2023/5/13 20:01:08
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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SqlSugar;
using System.Linq.Expressions;


namespace HuanTian.EntityFrameworkCore
{
    /// <summary>
    /// Ef init仓储
    /// </summary>
    public class EfReposityoryInit<TEntity> : IReposityoryInit<TEntity> where TEntity : class, new()
    {
        private readonly List<Expression<Func<TEntity, object>>> _ignoredColumns = new List<Expression<Func<TEntity, object>>>();
        private readonly IEnumerable<TEntity> _entityList;
        private readonly EfSqlContext _db;
        public EfReposityoryInit(EfSqlContext db)
        {
            _db = db;
        }
        public EfReposityoryInit(EfSqlContext db, IEnumerable<TEntity> entityList)
            : this(db)
        {
            _entityList = entityList;
        }
        public EfReposityoryInit(EfSqlContext db, IEnumerable<TEntity> entityList, List<Expression<Func<TEntity, object>>> ignoredColumns)
            : this(db, entityList)
        {
            _ignoredColumns = ignoredColumns;
        }

        public async Task<int> AddAsync()
        {
            await _db.Set<TEntity>().AddRangeAsync(_entityList);
            return await _db.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync()
        {
            _db.Set<TEntity>().RemoveRange(_entityList);
            return await _db.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync()
        {
            // 正常修改
            if (!_ignoredColumns.Any())
            {
                _db.Set<TEntity>().UpdateRange(_entityList);
                return await _db.SaveChangesAsync();
            }
            // 如果需要忽略列进行修改
            foreach (var entity in _entityList)
            {
                var entry = _db.Entry(entity);
                entry.State = EntityState.Modified;

                // 忽略需要忽略的属性
                foreach (var expression in _ignoredColumns)
                {
                    entry.Property(expression).IsModified = false;
                }
            }
            return await _db.SaveChangesAsync();
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
            var ignoredColumns = new List<Expression<Func<TEntity, object>>>();
            // 将若干个lambda表达式转换成成员表达式  new {a.name,a.pwd} 转换成 a.name,a.pwd
            if (expression.Body is NewExpression newExpression)
            {
                foreach (var argument in newExpression.Arguments)
                {
                    if (argument is MemberExpression memberExpression)
                    {
                        var lambdaExpression = Expression.Lambda<Func<TEntity, object>>(memberExpression, expression.Parameters);
                        ignoredColumns.Add(lambdaExpression);
                    }
                }
            }
            return new EfReposityoryInit<TEntity>(_db, _entityList, ignoredColumns);
        }
    }
}