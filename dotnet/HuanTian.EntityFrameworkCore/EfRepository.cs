////using HuanTian.Entities;
////using HuanTian.Infrastructure;
////using Microsoft.EntityFrameworkCore;
////using System.Linq;
////using System.Linq.Expressions;

////namespace HuanTian.EntityFrameworkCore
////{
////    /// <summary>
////    /// EF仓储类
////    /// </summary>
////    /// <typeparam name="TEntity"></typeparam>
////    public class EfRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntityBusiness, new()
////    {
////        private readonly EfSqlContext _dbContext;
////        private bool _isAsc;
////        private Expression<Func<TEntity, object>> _orderByExpression;
////        private Expression<Func<TEntity, bool>> _sqlWhereExpression;

////        public EfRepository(EfSqlContext dbContext)
////        {
////            _dbContext = dbContext;
////        }
////        public EfRepository(EfSqlContext dbContext, Expression<Func<TEntity, object>> orderByExpression, bool isAsc)
////             : this(dbContext)
////        {
////            _isAsc = isAsc;
////            _orderByExpression = orderByExpression;
////        }
////        public EfRepository(EfSqlContext dbContext, Expression<Func<TEntity, object>> orderByExpression, bool isAsc, Expression<Func<TEntity, bool>> sqlWhereExpression)
////             : this(dbContext, orderByExpression, isAsc)
////        {
////            _orderByExpression = orderByExpression;
////        }

////        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate) => await _dbContext.Set<TEntity>().FirstOrDefaultAsync(predicate);

//public async Task<IEnumerable<TEntity>> ToListAsync(Expression<Func<TEntity, bool>> predicate = default)
//{
//    IQueryable<TEntity> value = _dbContext.Set<TEntity>();

//    //            if (predicate != null)
//    //            {
//    //                value = value.Where(predicate);
//    //            }

//    //            return await value.ToListAsync();
//    //        }

//    public async Task<IEnumerable<TEntity>> ToListAsync(Expression<Func<TEntity, bool>> predicate, int pageNo, int pageSize)
//    {
//        IQueryable<TEntity> value = _dbContext.Set<TEntity>();

//        //            if (predicate != null)
//        //            {
//        //                value = value.Where(predicate);
//        //            }

//        //            if (_orderByExpression != null)
//        //            {
//        //                value = _isAsc ? value.OrderBy(_orderByExpression) : value.OrderByDescending(_orderByExpression);
//        //            }

//        //            return await value.Skip((pageNo - 1) * pageSize).Take(pageSize).ToListAsync();
//        //        }

//        public async Task<PageData> ToPageListAsync(Expression<Func<TEntity, bool>> predicate, int pageNo, int pageSize)
//        {
//            IQueryable<TEntity> value = _dbContext.Set<TEntity>();
//            var pageData = new PageData();

//            //            if (predicate != null)
//            //            {
//            //                value = value.Where(predicate);
//            //            }

//            //            pageData.TotalCount = await value.CountAsync();

//            //            if (_orderByExpression != null)
//            //            {
//            //                value = _isAsc ? value.OrderBy(_orderByExpression) : value.OrderByDescending(_orderByExpression);
//            //            }

//            //            pageData.Data = await value.Skip((pageNo - 1) * pageSize).Take(pageSize).ToListAsync();
//            //            pageData.PageNo = pageNo;
//            //            pageData.PageSize = pageSize;
//            //            pageData.TotalPage = (int)Math.Ceiling((double)pageData.TotalCount / pageSize);

//            //            return pageData;
//            //        }
//            //        public void DeleteAsync(TEntity entity)
//            //        {
//            //            _dbContext.Set<TEntity>().Remove(entity);
//            //        }

//            //        public async Task AddAsync(TEntity entity)
//            //        {
//            //            await _dbContext.Set<TEntity>().AddAsync(entity);
//            //        }

//            //        public void UpdateAsync(TEntity entity)
//            //        {
//            //            _dbContext.Set<TEntity>().Update(entity);
//            //        }

//            //        public IRepository<TEntity> OrderBy(Expression<Func<TEntity, object>> orderByExpression, bool isAsc)
//            //        {
//            //            return new EfRepository<TEntity>(_dbContext, orderByExpression, isAsc); ;
//            //        }

//            public async Task<PageData> ToPageListAsync(int pageNo, int pageSize)
//            {
//                IQueryable<TEntity> value = _dbContext.Set<TEntity>();
//                var pageData = new PageData();

////            pageData.TotalCount = await value.CountAsync();

////            if (_orderByExpression != null)
////            {
////                value = _isAsc ? value.OrderBy(_orderByExpression) : value.OrderByDescending(_orderByExpression);
////            }

////            pageData.Data = await value.Skip((pageNo - 1) * pageSize).Take(pageSize).ToListAsync();
////            pageData.PageNo = pageNo;
////            pageData.PageSize = pageSize;
////            pageData.TotalPage = (int)Math.Ceiling((double)pageData.TotalCount / pageSize);

////            return pageData;
////        }

////        public IRepository<TEntity> WhereIf(bool condition, Expression<Func<TEntity, bool>> sqlWhereExpression)
////        {
////            // 合并 Expression
////            Expression<Func<TEntity, bool>>? whereExpression = _sqlWhereExpression;

////            if (condition && sqlWhereExpression != null)
////            {
////                if (whereExpression != null)
////                {
////                    whereExpression = whereExpression.And<TEntity>(sqlWhereExpression, ExpressionType.Add);
////                }
////                else
////                {
////                    whereExpression = sqlWhereExpression;
////                }
////                return new EfRepository<TEntity>(_dbContext, _orderByExpression, _isAsc, whereExpression);
////            }

////            return this;

////        }
////    }

////}
