using System.Linq.Expressions;

namespace HuanTian.Infrastructure
{
    /// <summary>
    /// 实体仓储接口(ORM)
    /// </summary>
    /// <typeparam name="TEntity">实体表格</typeparam>
    public interface IRepository<TEntity> where TEntity : class, new()
    {
        #region 增删改查
        /// <summary>
        /// 只查询一条数据
        /// <para>不支持 where 条件</para>
        /// </summary>
        /// <param name="predicate">筛选条件</param>
        /// <returns></returns>
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 查询值是否存在
        /// <para>不支持 where 条件</para>
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 获取所有数据
        /// <para>使用 Where 筛选</para>
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> ToListAsync();
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
        /// 修改一条数据 
        /// </summary>
        /// <param name="entity"></param>
        Task<int> UpdateAsync(TEntity entity);
        /// <summary>
        /// 修改多条数据 
        /// </summary>
        /// <param name="entity"></param>
        Task<int> UpdateAsync(List<TEntity> entity);
        /// <summary>
        /// 删除一条数据 
        /// </summary>
        /// <param name="entity"></param>
        Task<int> DeleteAsync(TEntity entity);
        /// <summary>
        /// 删除一条数据根据ID 
        /// </summary>
        /// <param name="id"></param>
        Task<int> DeleteAsync(params long[] id);
        /// <summary>
        /// 删除一条数据根据lanmboda表达式
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<int> DeleteAsync(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 删除多条数据 
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
        /// 筛选条件
        /// </summary>
        /// <param name="sqlWhereExpression"></param>
        /// <returns></returns>
        IRepository<TEntity> Where(Expression<Func<TEntity, bool>> sqlWhereExpression);
        /// <summary>
        /// 是否,筛选条件 true执行 false不执行
        /// </summary>
        /// <param name="condition">判断条件</param>
        /// <param name="sqlWhereExpression">筛选条件</param>
        /// <returns></returns>
        IRepository<TEntity> WhereIf(bool condition, Expression<Func<TEntity, bool>> sqlWhereExpression);
        /// <summary>
        /// 加载表格实体
        /// <para>不支持跟 IRepository 仓储共用,在此仅提供调用链接</para>
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        IReposityoryInit<TEntity> InitTable(TEntity entity);
        /// <summary>
        /// 加载表格实体(集合)
        /// <para>不支持跟 IRepository 仓储共用,在此仅提供调用链接</para>
        /// </summary>
        /// <param name="entityList"></param>
        /// <returns></returns>
        IReposityoryInit<TEntity> InitTable(IEnumerable<TEntity> entityList);
        /// <summary>
        /// 忽略全局过滤器
        /// </summary>
        /// <returns></returns>
        IRepository<TEntity> IgnoreFilters();


    }
}
