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

        public EfRepository(EfSqlContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbContext.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            return await _dbContext.Set<TEntity>().Where(predicate).ToListAsync();
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
    }

}
