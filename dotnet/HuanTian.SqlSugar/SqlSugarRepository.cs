using HuanTian.Entities;
using HuanTian.Infrastructure;
using SqlSugar;
using System.Linq.Expressions;

namespace HuanTian.SqlSugar
{
    /// <summary>
    /// SqlSugar仓储实现
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class SqlSugarRepository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        private readonly ISqlSugarClient _db;
        private bool _isAsc;
        private Expression<Func<TEntity, object>>? _orderByExpression;
        private Expression<Func<TEntity, bool>>? _sqlWhereExpression;

        #region 构造函数
        public SqlSugarRepository(ISqlSugarClient db)
        {
            _db = db;
        }
        public SqlSugarRepository(ISqlSugarClient db, Expression<Func<TEntity, object>> orderByExpression, bool isAsc)
            : this(db)
        {
            _orderByExpression = orderByExpression;
            _isAsc = isAsc;
        }
        public SqlSugarRepository(ISqlSugarClient db, Expression<Func<TEntity, object>> orderByExpression, bool isAsc, Expression<Func<TEntity, bool>> sqlWhereExpression)
             : this(db, orderByExpression, isAsc)
        {
            _sqlWhereExpression = sqlWhereExpression;
        }
        #endregion

        #region 增删改查
        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _db.Queryable<TEntity>().FirstAsync(predicate);
        }

        public async Task<IEnumerable<TEntity>> ToListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var value = _db.Queryable<TEntity>();

            if (predicate != null)
            {
                value = value.Where(predicate);
            }

            if (_orderByExpression != null)
            {
                value = _isAsc ? value.OrderBy(_orderByExpression, OrderByType.Desc) : value.OrderByDescending(_orderByExpression);
            }

            return await value.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> ToListAsync(Expression<Func<TEntity, bool>> predicate, int pageNo, int pageSize)
        {
            var value = _db.Queryable<TEntity>();

            if (predicate != null)
            {
                value = value.Where(predicate);
            }

            if (_sqlWhereExpression != null)
            {
                value.Where(_sqlWhereExpression);
            }

            if (_orderByExpression != null)
            {
                value = _isAsc ? value.OrderBy(_orderByExpression) : value.OrderByDescending(_orderByExpression);
            }

            return await value.ToPageListAsync(pageNo, pageSize);
        }

        public async Task<PageData> ToPageListAsync(Expression<Func<TEntity, bool>> predicate, int pageNo, int pageSize)
        {
            var value = _db.Queryable<TEntity>();
            var pageData = new PageData();
            if (predicate != null)
            {
                value = value.Where(predicate);
            }

            if (_sqlWhereExpression != null)
            {
                value.Where(_sqlWhereExpression);
            }

            if (_orderByExpression != null)
            {
                value = _isAsc ? value.OrderBy(_orderByExpression) : value.OrderByDescending(_orderByExpression);
            }

            RefAsync<int> total = 0;
            pageData.Data = await value.ToPageListAsync(pageNo, pageSize, total);
            pageData.TotalCount = total;
            pageData.PageNo = pageNo;
            pageData.PageSize = pageSize;
            pageData.TotalPage = (int)Math.Ceiling((double)total / pageSize);

            return pageData;
        }

        public async Task<PageData> ToPageListAsync(int pageNo, int pageSize)
        {
            var value = _db.Queryable<TEntity>();
            var pageData = new PageData();

            if (_sqlWhereExpression != null)
            {
                value.Where(_sqlWhereExpression);
            }

            if (_orderByExpression != null)
            {
                value = _isAsc ? value.OrderBy(_orderByExpression) : value.OrderByDescending(_orderByExpression);
            }

            RefAsync<int> total = 0;
            pageData.Data = await value.ToPageListAsync(pageNo, pageSize, total);
            pageData.TotalCount = total;
            pageData.PageNo = pageNo;
            pageData.PageSize = pageSize;
            pageData.TotalPage = (int)Math.Ceiling((double)total / pageSize);

            return pageData;
        }

        public async Task<int> DeleteAsync(TEntity entity)
        {
            return await _db.Deleteable<TEntity>(entity).ExecuteCommandAsync();
        }
        public async Task<int> DeleteAsync(List<TEntity> entityList)
        {
            return await _db.Deleteable<TEntity>(entityList).ExecuteCommandAsync();
        }

        public async Task<int> AddAsync(TEntity entity)
        {
            return await _db.Insertable(entity).ExecuteCommandAsync();
        }
        public async Task<int> AddAsync(List<TEntity> entityList)
        {
            return await _db.Insertable(entityList).ExecuteCommandAsync();
        }

        public async Task<int> UpdateAsync(TEntity entity)
        {
            return await _db.Updateable(entity).ExecuteCommandAsync();
        }
        public async Task<int> UpdateAsync(List<TEntity> entityList)
        {
            return await _db.Updateable(entityList).ExecuteCommandAsync();
        }
        #endregion

        #region 帮助方法
        public IRepository<TEntity> OrderBy(Expression<Func<TEntity, object>> orderByExpression, bool isAsc)
        {
            return new SqlSugarRepository<TEntity>(_db, orderByExpression, isAsc); ;
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
                return new SqlSugarRepository<TEntity>(_db, _orderByExpression, _isAsc, whereExpression);
            }

            return this;

        }

        public IReposityoryInit<TEntity> InitTable(TEntity entity)
        {
            return new SqlSugarReposityoryInit<TEntity>(_db, new List<TEntity>() { entity });
        }

        public IReposityoryInit<TEntity> InitTable(List<TEntity> entityList)
        {
            return new SqlSugarReposityoryInit<TEntity>(_db, entityList);
        }
        #endregion

    }
}