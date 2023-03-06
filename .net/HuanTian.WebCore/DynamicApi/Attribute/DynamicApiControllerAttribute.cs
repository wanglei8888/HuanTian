using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuanTian.WebCore
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class DynamicApiControllerAttribute : Attribute
    {
    }

    /// <summary>
    /// 动态Api控制器依赖接口
    /// </summary>
    public interface IDynamicApiController
    {
    }
}
