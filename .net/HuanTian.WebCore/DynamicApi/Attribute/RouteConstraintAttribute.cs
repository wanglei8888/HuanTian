using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuanTian.WebCore
{
    /// <summary>
    /// 接口参数约束
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public class RouteConstraintAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="constraint"></param>
        public RouteConstraintAttribute(string constraint)
        {
            Constraint = constraint;
        }

        /// <summary>
        /// 约束表达式
        /// </summary>
        public string Constraint { get; set; }
    }
}
