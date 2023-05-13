using HuanTian.Entities;
using HuanTian.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace HuanTian.EntityFrameworkCore
{
    /// <summary>
    /// EF仓储类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class EfRepository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        private readonly EfSqlContext _db;
        private bool _isAsc;
        private Expression<Func<TEntity, object>>? _orderByExpression;
        private Expression<Func<TEntity, bool>>? _sqlWhereExpression;

        #region 构造参数
        public EfRepository(EfSqlContext dbContext)
        {
            _db = dbContext;
        }
        public EfRepository(EfSqlContext dbContext, Expression<Func<TEntity, object>> orderByExpression, bool isAsc)
             : this(dbContext)
        {
            _isAsc = isAsc;
            _orderByExpression = orderByExpression;
        }
        public EfRepository(EfSqlContext dbContext, Expression<Func<TEntity, object>> orderByExpression, bool isAsc, Expression<Func<TEntity, bool>> sqlWhereExpression)
             : this(dbContext, orderByExpression, isAsc)
        {
            _sqlWhereExpression = sqlWhereExpression;
        }
        #endregion

        #region 增删改查
        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate) => await _db.Set<TEntity>().FirstOrDefaultAsync(predicate);

        public async Task<IEnumerable<TEntity>> ToListAsync(Expression<Func<TEntity, bool>> predicate = default)
        {
            IQueryable<TEntity> value = _db.Set<TEntity>();

            if (predicate != null)
            {
                value = value.Where(predicate);
            }

            return await value.ToListAsync();
        }
        public async Task<PageData> ToPageListAsync(Expression<Func<TEntity, bool>> predicate, int pageNo, int pageSize)
        {
            IQueryable<TEntity> value = _db.Set<TEntity>();
            var pageData = new PageData();

            if (predicate != null)
            {
                value = value.Where(predicate);
            }

            pageData.TotalCount = await value.CountAsync();

            if (_orderByExpression != null)
            {
                value = _isAsc ? value.OrderBy(_orderByExpression) : value.OrderByDescending(_orderByExpression);
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
            return new EfRepository<TEntity>(_db, orderByExpression, isAsc); ;
        }

        public async Task<PageData> ToPageListAsync(int pageNo, int pageSize)
        {
            IQueryable<TEntity> value = _db.Set<TEntity>();
            var pageData = new PageData();

            pageData.TotalCount = await value.CountAsync();

            if (_orderByExpression != null)
            {
                value = _isAsc ? value.OrderBy(_orderByExpression) : value.OrderByDescending(_orderByExpression);
            }

            pageData.Data = await value.Skip((pageNo - 1) * pageSize).Take(pageSize).ToListAsync();
            pageData.PageNo = pageNo;
            pageData.PageSize = pageSize;
            pageData.TotalPage = (int)Math.Ceiling((double)pageData.TotalCount / pageSize);

            return pageData;
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
                return new EfRepository<TEntity>(_db, _orderByExpression, _isAsc, whereExpression);
            }

            return this;

        }

        public IReposityoryInit<TEntity> InitTable(TEntity entity)
        {
            return new EfReposityoryInit<TEntity>(_db, new List<TEntity> { entity });
        }

        public IReposityoryInit<TEntity> InitTable(List<TEntity> entityList)
        {
            return new EfReposityoryInit<TEntity>(_db, entityList);
        }
        #endregion
    }

}
