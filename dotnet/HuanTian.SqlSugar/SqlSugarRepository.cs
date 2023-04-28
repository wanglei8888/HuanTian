using HuanTian.Entities;
using HuanTian.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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

        public SqlSugarRepository(ISqlSugarClient db)
        {
            _db = db;
        }
        public SqlSugarRepository(ISqlSugarClient db, Expression<Func<TEntity, object>> orderByExpression, bool isAsc)
        {
            _db = db;
            _orderByExpression = orderByExpression;
            _isAsc = isAsc;
        }

        public void Insert(TEntity entity)
        {
            _db.Insertable(entity).ExecuteCommand();
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _db.Queryable<TEntity>().FirstAsync(predicate);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var value = _db.Queryable<TEntity>();

            if (predicate != null)
            {
                value = value.Where(predicate);
            }

            if (_orderByExpression != null)
            {
                value = _isAsc ? value.OrderBy(_orderByExpression) : value.OrderByDescending(_orderByExpression);
            }

            return await value.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, int pageNo, int pageSize)
        {
            var value = _db.Queryable<TEntity>();

            if (predicate != null)
            {
                value = value.Where(predicate);
            }

            if (_orderByExpression != null)
            {
                value = _isAsc ? value.OrderBy(_orderByExpression) : value.OrderByDescending(_orderByExpression);
            }

            return await value.ToPageListAsync(pageNo, pageSize);
        }

        public async Task<PageData> GetAllToPageAsync(Expression<Func<TEntity, bool>> predicate, int pageNo, int pageSize)
        {
            var value = _db.Queryable<TEntity>();
            var pageData = new PageData();
            if (predicate != null)
            {
                value = value.Where(predicate);
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
        public async Task AddAsync(TEntity entity)
        {
            _db.Insertable(entity).ExecuteCommand();
            await Task.CompletedTask;
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
    }
}