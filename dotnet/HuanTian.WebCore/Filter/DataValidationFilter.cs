using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuanTian.WebCore
{
    /// <summary>
    /// 数据验证拦截器
    /// </summary>
    public class DataValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //Console.WriteLine("MyActionFilter 1:开始执行");
            ActionExecutedContext r = await next(); //如果执行 下一个ActionFilter
            //if (r.Exception != null)
            //{
            //    Console.WriteLine("MyActionFilter 1:执行失败");
            //}
            //else
            //{
            //    Console.WriteLine("MyActionFilter 1:执行成功");
            //}
        }

    }

}
