using HuanTian.Infrastructure;
using Microsoft.EntityFrameworkCore;
using SqlSugar;
using System.Linq;
using System.Linq.Expressions;

namespace HuanTian.EntityFrameworkCore
{
    /// <summary>
    /// EF仓储类
    /// <para>更多详细信息,请访问<a href="https://learn.microsoft.com/zh-cn/ef/core">官网</a></para>
    /// </summary>
    /// <typeparam name="TEntity">实体表格</typeparam>
    public class EfRepository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        private readonly EfSqlContext _db;
        private readonly bool _isAsc;
        private readonly bool _isIgnoreFilter;
        private readonly Expression<Func<TEntity, object>>? _orderByExpression;
        private readonly Expression<Func<TEntity, bool>>? _sqlWhereExpression;

        #region 构造参数
        public EfRepository(EfSqlContext dbContext)
        {
            _db = dbContext;
        }
        public EfRepository(EfSqlContext dbContext, Expression<Func<TEntity, object>> orderByExpression, bool isAsc, Expression<Func<TEntity, bool>> sqlWhereExpression, bool isIgnoreFilter)
             : this(dbContext)
        {
            _isAsc = isAsc;
            _orderByExpression = orderByExpression;
            _sqlWhereExpression = sqlWhereExpression;
            _isIgnoreFilter = isIgnoreFilter;
        }
        #endregion

        #region 增删改查
        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> value = _db.Set<TEntity>();
            if (_isIgnoreFilter)
            {
                value = value.IgnoreQueryFilters();
            }

            return await value.FirstOrDefaultAsync(predicate);
        }
        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> value = _db.Set<TEntity>();
            if (_isIgnoreFilter)
            {
                value = value.IgnoreQueryFilters();
            }

            return await value.AnyAsync(predicate);
        }

        public async Task<IEnumerable<TEntity>> ToListAsync()
        {
            IQueryable<TEntity> value = _db.Set<TEntity>();
            if (_sqlWhereExpression != null)
            {
                value = value.Where(_sqlWhereExpression);
            }

            if (_orderByExpression != null)
            {
                value = _isAsc ? value.OrderBy(_orderByExpression) : value.OrderByDescending(_orderByExpression);
            }

            if (_isIgnoreFilter)
            {
                value = value.IgnoreQueryFilters();
            }

            return await value.ToListAsync();
        }
        public async Task<PageData> ToPageListAsync(int pageNo, int pageSize)
        {
            IQueryable<TEntity> value = _db.Set<TEntity>();
            var pageData = new PageData();

            if (_sqlWhereExpression != null)
            {
                value = value.Where(_sqlWhereExpression);
            }

            pageData.TotalCount = await value.CountAsync();

            if (_orderByExpression != null)
            {
                value = _isAsc ? value.OrderBy(_orderByExpression) : value.OrderByDescending(_orderByExpression);
            }

            if (_isIgnoreFilter)
            {
                value = value.IgnoreQueryFilters();
            }
            pageData.Data = await value.Skip((pageNo - 1) * pageSize).Take(pageSize).ToListAsync();
            pageData.PageNo = pageNo;
            pageData.PageSize = pageSize;
            pageData.TotalPage = (int)Math.Ceiling((double)pageData.TotalCount / pageSize);

            return pageData;
        }

        public async Task<int> DeleteAsync(TEntity entity)
        {
            _db.Set<TEntity>().Remove(entity);
            return await _db.SaveChangesAsync();
        }
        public async Task<int> DeleteAsync(List<TEntity> entityList)
        {
            _db.Set<TEntity>().RemoveRange(entityList);
            return await _db.SaveChangesAsync();
        }
        public async Task<int> DeleteAsync(params long[] id)
        {
            var num = 0;
            // 单个执行
            foreach (var item in id)
            {
                var entity = await _db.Set<TEntity>().FindAsync(item);
                _db.Set<TEntity>().RemoveRange(entity);
                await _db.SaveChangesAsync();
                num++;
            }
            return num;
        }
        public async Task<int> DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var entities = _db.Set<TEntity>().Where(predicate.Compile());
            _db.Set<TEntity>().RemoveRange(entities);
            return await _db.SaveChangesAsync();
        }
        public async Task<int> AddAsync(TEntity entity)
        {
            await _db.Set<TEntity>().AddAsync(entity);
            return await _db.SaveChangesAsync();
        }
        public async Task<int> AddAsync(List<TEntity> entityList)
        {
            await _db.Set<TEntity>().AddRangeAsync(entityList);
            return await _db.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(TEntity entity)
        {
            _db.Set<TEntity>().Update(entity);
            return await _db.SaveChangesAsync();
        }
        public async Task<int> UpdateAsync(List<TEntity> entityList)
        {
            _db.Set<TEntity>().UpdateRange(entityList);
            return await _db.SaveChangesAsync();
        }
        #endregion

        #region 帮助方法

        public IRepository<TEntity> OrderBy(Expression<Func<TEntity, object>> orderByExpression, bool isAsc)
        {
            return new EfRepository<TEntity>(_db, orderByExpression, isAsc, _sqlWhereExpression, _isIgnoreFilter); ;
        }
        public IRepository<TEntity> Where(Expression<Func<TEntity, bool>> sqlWhereExpression)
        {
            // 合并 Expression
            var whereExpression = _sqlWhereExpression;
            if (whereExpression != null)
            {
                whereExpression = whereExpression.And<TEntity>(sqlWhereExpression, ExpressionType.Add);
            }
            else
            {
                whereExpression = sqlWhereExpression;
            }
            return new EfRepository<TEntity>(_db, _orderByExpression, _isAsc, whereExpression, _isIgnoreFilter);
        }
        public IRepository<TEntity> WhereIf(bool condition, Expression<Func<TEntity, bool>> sqlWhereExpression)
        {
            // 合并 Expression
            Expression<Func<TEntity, bool>>? whereExpression = _sqlWhereExpression;

            if (condition && sqlWhereExpression != null)
            {
                if (whereExpression != null)
                {
                    whereExpression = whereExpression.And<TEntity>(sqlWhereExpression, ExpressionType.Add);
                }
                else
                {
                    whereExpression = sqlWhereExpression;
                }
                return new EfRepository<TEntity>(_db, _orderByExpression, _isAsc, whereExpression, _isIgnoreFilter);
            }

            return this;

        }

        public IRepository<TEntity> IgnoreFilters()
        {
            return new EfRepository<TEntity>(_db, _orderByExpression, _isAsc, _sqlWhereExpression, true);
        }
        public IReposityoryInit<TEntity> InitTable(TEntity entity)
        {
            return new EfReposityoryInit<TEntity>(_db, new List<TEntity> { entity });
        }

        public IReposityoryInit<TEntity> InitTable(IEnumerable<TEntity> entityList)
        {
            return new EfReposityoryInit<TEntity>(_db, entityList);
        }
        #endregion
    }

}
