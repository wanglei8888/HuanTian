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

using HuanTian.Entities;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace HuanTian.EntityFrameworkCore
{
    /// <summary>
    /// Ef init仓储
    /// </summary>
    public class EfReposityoryInit<TEntity> : IReposityoryInit<TEntity> where TEntity : class, new()
    {
        private IEnumerable<TEntity>? _entityList;
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
            _db.Set<TEntity>().UpdateRange(_entityList);
            return await _db.SaveChangesAsync();
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