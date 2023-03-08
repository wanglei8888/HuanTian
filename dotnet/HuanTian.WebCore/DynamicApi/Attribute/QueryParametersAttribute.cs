using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuanTian.WebCore
{
    /// <summary>
    /// 将 Action 所有参数 [FromQuery] 化
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class QueryParametersAttribute : Attribute
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public QueryParametersAttribute()
        {
        }
    }
}
