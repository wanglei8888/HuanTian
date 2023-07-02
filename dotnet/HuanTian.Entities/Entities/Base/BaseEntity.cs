using Microsoft.EntityFrameworkCore;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using Yitter.IdGenerator;

namespace HuanTian.Entities
{
    /// <summary>
    /// 表格基类
    /// </summary>
    public class BaseEntity
    {
        [Key]
        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }

        /// <summary>
        /// 软删除(是否已经删除)
        /// </summary>
        [Comment("软删除")]
        public virtual bool Deleted { get; set; } = false;
        /// <summary>
        /// 实体新增自动赋值
        /// </summary>
        public virtual void CreateFunc()
        {
            Id = YitIdHelper.NextId();
            Deleted = false;
        }
        /// <summary>
        /// 设置属性的值-大量数据请勿使用,性能较差
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="propertyExpression"></param>
        /// <param name="value"></param>
        public virtual void SetPropertyValue<T, TValue>(Expression<Func<T, TValue>> propertyExpression, TValue value)
        {
            if (propertyExpression.Body is MemberExpression memberExpression)
            {
                var propertyInfo = memberExpression.Member as System.Reflection.PropertyInfo;
                if (propertyInfo != null)
                {
                    propertyInfo.SetValue(this, value);
                }
            }
        }
    }
}
