using HuanTian.Entities;
using System.Linq.Expressions;

namespace HuanTian.Infrastructure
{
    public interface IRepository<TEntity> where TEntity : class, new()
    {
        /// <summary>
        /// 只查询一条数据
        /// </summary>
        /// <param name="predicate">筛选条件</param>
        /// <returns></returns>
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <param name="predicate">筛选条件</param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = default);
        /// <summary>
        /// 分页获取所有数据 返回原始数据
        /// </summary>
        /// <param name="predicate">筛选条件</param>
        /// <param name="pageNo">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns>返回原始数据</returns>
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, int pageNo, int pageSize);
        /// <summary>
        /// 分页获取所有数据 有筛选条件
        /// </summary>
        /// <param name="predicate">筛选条件</param>
        /// <param name="pageNo">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns>返回分页PageData实体</returns>
        Task<PageData> GetAllToPageAsync(Expression<Func<TEntity, bool>> predicate, int pageNo, int pageSize);
        /// <summary>
        /// 分页获取所有数据 无筛选条件通过WhereIf
        /// </summary>
        /// <param name="pageNo">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns>返回分页PageData实体</returns>
        Task<PageData> GetAllToPageAsync(int pageNo, int pageSize);
        /// <summary>
        /// 新增一条数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task AddAsync(TEntity entity);
        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <param name="entity"></param>
        void UpdateAsync(TEntity entity);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="entity"></param>
        void DeleteAsync(TEntity entity);
        /// <summary>
        /// 数据排序
        /// </summary>
        /// <param name="orderByExpression">排序字段</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns></returns>
        IRepository<TEntity> OrderBy(Expression<Func<TEntity, object>> orderByExpression, bool isAsc);
        /// <summary>
        /// 是否,筛选条件 true执行 false不执行
        /// </summary>
        /// <param name="condition">判断条件</param>
        /// <param name="sqlWhereExpression">筛选条件</param>
        /// <returns></returns>
        IRepository<TEntity> WhereIf(bool condition, Expression<Func<TEntity, bool>> sqlWhereExpression);
        //// 原生 SqlSugar 操作
        //ISqlSugarClient SqlSugarClient { get; }
    }
}
