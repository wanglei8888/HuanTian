﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace HuanTian.Interface
{
    public interface IRepository<TEntity> where TEntity : class, new()
    {
        /// <summary>
        /// 只查询一条数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate);
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
    }
}