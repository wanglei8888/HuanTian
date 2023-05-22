using System.Linq.Expressions;

namespace HuanTian.Entities
{
    /// <summary>
    /// 仓储接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> where TEntity : class, new()
    {
        #region 增删改查
        /// <summary>
        /// 只查询一条数据
        /// </summary>
        /// <param name="predicate">筛选条件</param>
        /// <returns></returns>
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <param name="predicate">筛选条件+whereif</param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> ToListAsync(Expression<Func<TEntity, bool>> predicate = default);
        /// <summary>
        /// 分页获取所有数据 通过WhereIf筛选条件
        /// </summary>
        /// <param name="pageNo">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns>返回分页PageData实体</returns>
        Task<PageData> ToPageListAsync(int pageNo, int pageSize);
        /// <summary>
        /// 新增一条数据
        /// </summary>
        /// <returns></returns>
        Task<int> AddAsync(TEntity entity);
        /// <summary>
        /// 新增多条数据
        /// </summary>
        /// <returns></returns>
        Task<int> AddAsync(List<TEntity> entityList);
        /// <summary>
        /// 修改一条数据 (如果传入了InitTable将以InitTable为主)
        /// </summary>
        /// <param name="entity"></param>
        Task<int> UpdateAsync(TEntity entity);
        /// <summary>
        /// 修改多条数据 (如果传入了InitTable将以InitTable为主)
        /// </summary>
        /// <param name="entity"></param>
        Task<int> UpdateAsync(List<TEntity> entity);
        /// <summary>
        /// 删除一条数据 (如果传入了InitTable将以InitTable为主)
        /// </summary>
        /// <param name="entity"></param>
        Task<int> DeleteAsync(TEntity entity);
        /// <summary>
        /// 删除多条数据 (如果传入了InitTable将以InitTable为主)
        /// </summary>
        /// <param name="entity"></param>
        Task<int> DeleteAsync(List<TEntity> entity); 
        #endregion
        /// <summary>
        /// 数据排序
        /// </summary>
        /// <param name="orderByExpression">排序字段</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns></returns>
        IRepository<TEntity> OrderBy(Expression<Func<TEntity, object>> orderByExpression, bool isAsc = true);
        /// <summary>
        /// 是否,筛选条件 true执行 false不执行
        /// </summary>
        /// <param name="condition">判断条件</param>
        /// <param name="sqlWhereExpression">筛选条件</param>
        /// <returns></returns>
        IRepository<TEntity> WhereIf(bool condition, Expression<Func<TEntity, bool>> sqlWhereExpression);
        /// <summary>
        /// 加载表格实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        IReposityoryInit<TEntity> InitTable(TEntity entity);
        /// <summary>
        /// 加载表格实体(集合)
        /// </summary>
        /// <param name="entityList"></param>
        /// <returns></returns>
        IReposityoryInit<TEntity> InitTable(List<TEntity> entityList);


    }
}
