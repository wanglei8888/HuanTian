using Microsoft.EntityFrameworkCore;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using Yitter.IdGenerator;

namespace HuanTian.Infrastructure
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
        /// 设定属性值，支持多列与符号隔开
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="propertyExpression"></param>
        public virtual void SetValue<TValue>(Expression<Func<TValue, bool>> propertyExpression)
        {
            // 切割lamboda表达式
            var conditions = new List<Expression>();
            if (propertyExpression.Body is BinaryExpression binaryExpression1)
            {
                ExtractConditionsRecursively(binaryExpression1, conditions);
            }
            // 循环设置属性值
            foreach (var condition in conditions)
            {
                if (condition is BinaryExpression binaryExpression)
                {
                    if (binaryExpression.Left is MemberExpression memberExpression)
                    {
                        var propertyValue = Expression.Lambda(binaryExpression.Right).Compile().DynamicInvoke();
                        var propertyInfo = memberExpression.Member as PropertyInfo;
                        if (propertyInfo != null)
                        {
                            // Assuming 'this' refers to the current instance of the class
                            propertyInfo.SetValue(this, propertyValue);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 切割lamboda表达式
        /// </summary>
        /// <param name="binaryExpression"></param>
        /// <param name="conditions"></param>
        private void ExtractConditionsRecursively(BinaryExpression binaryExpression, List<Expression> conditions)
        {
            if (binaryExpression == null)
            {
                return;
            }
            if (binaryExpression.Left is MemberExpression memberExpression)
            {
                conditions.Add(binaryExpression);
            }

            if (binaryExpression.Left is BinaryExpression leftBinaryExpression)
            {
                ExtractConditionsRecursively(leftBinaryExpression, conditions);
            }

            if (binaryExpression.Right is BinaryExpression rightBinaryExpression)
            {
                ExtractConditionsRecursively(rightBinaryExpression, conditions);
            }
        }
    }
}
