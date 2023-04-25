﻿using HuanTian.Entities;
using HuanTian.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HuanTian.EntityFrameworkCore
{
    /// <summary>
    /// EF仓储类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class EfRepository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        private readonly EfSqlContext _dbContext;
        private bool _isAsc;
        private Expression<Func<TEntity, object>> _orderByExpression;

        public EfRepository(EfSqlContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate) => await _dbContext.Set<TEntity>().FirstOrDefaultAsync(predicate);

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = default)
        {
            IQueryable<TEntity> value = _dbContext.Set<TEntity>();

            if (predicate != null)
            {
                value = value.Where(predicate);
            }

            return await value.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, int pageNo, int pageSize)
        {
            IQueryable<TEntity> value = _dbContext.Set<TEntity>();

            if (predicate != null)
            {
                value = value.Where(predicate);
            }

            if (_orderByExpression != null)
            {
                value = _isAsc ? value.OrderBy(_orderByExpression) : value.OrderByDescending(_orderByExpression);
            }

            return await value.Skip((pageNo - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<PageData> GetAllToPageAsync(Expression<Func<TEntity, bool>> predicate, int pageNo, int pageSize)
        {
            IQueryable<TEntity> value = _dbContext.Set<TEntity>();
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
        public void DeleteAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }

        public async Task AddAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
        }

        public void UpdateAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
        }

        public IRepository<TEntity> OrderBy(Expression<Func<TEntity, object>> orderByExpression, bool isAsc)
        {
            _isAsc = isAsc;
            _orderByExpression = orderByExpression;
            return this;
        }
    }

}
