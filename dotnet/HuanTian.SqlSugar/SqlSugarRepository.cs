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
        private Expression<Func<TEntity, object>> _orderByExpression;
        private Expression<Func<TEntity, bool>> _sqlWhereExpression;
        private TEntity _table;
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
        public SqlSugarRepository(ISqlSugarClient db, Expression<Func<TEntity, object>> orderByExpression, bool isAsc, Expression<Func<TEntity, bool>> sqlWhereExpression, TEntity table)
             : this(db, orderByExpression, isAsc, sqlWhereExpression)
        {
            _table = table;
        }

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
                value = _isAsc ? value.OrderBy(_orderByExpression,OrderByType.Desc) : value.OrderByDescending(_orderByExpression);
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

        public async Task<int> AddAsync(TEntity? entity = default)
        {
            if (entity == null)
            {
                entity = _table;
            }
            if (entity == null)
            {
                throw new ArgumentException("AddAsync(TEntity)、InitTable(TEntity)必须使用一个", nameof(entity));
            }
            return  await _db.Insertable(entity).ExecuteCommandAsync();
        }

        public void UpdateAsync(TEntity entity)
        {
            _db.Updateable(entity).ExecuteCommand();
        }

        public void DeleteAsync(TEntity entity)
        {
            _db.Deleteable<TEntity>().ExecuteCommand();
        }

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

        public IRepository<TEntity> CallEntityMethod(Expression<Action<TEntity>> method)
        {
            if (!(method.Body is MethodCallExpression callExpresion))
            {
                throw new ArgumentException("Expression must be a method call.", nameof(method));
            }
            if (_table == null)
            {
                throw new ArgumentException("使用CallEntityMethod必须先调用InitTable", nameof(method));
            }

            callExpresion.Method.Invoke(_table, null);
            return this;
        }

        public IRepository<TEntity> InitTable(TEntity table)
        {
            _table = table;
            return new SqlSugarRepository<TEntity>(_db, _orderByExpression, _isAsc, _sqlWhereExpression,table);
        }

    }
}