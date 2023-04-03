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

        public ISqlSugarClient SqlSugarClient => _db;

        public SqlSugarRepository(ISqlSugarClient db)
        {
            _db = db;
        }

        public void Insert(TEntity entity)
        {
            _db.Insertable(entity).ExecuteCommand();
        }

        public Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _db.Queryable<TEntity>().Where(predicate).ToListAsync();
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
    }
}