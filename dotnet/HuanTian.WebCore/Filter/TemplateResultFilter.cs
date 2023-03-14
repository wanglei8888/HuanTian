using HuanTian.Common;
using Microsoft.AspNetCore.Mvc;
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
    public class TemplateResultFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            ActionExecutedContext action = await next(); //如果执行 下一个ActionFilter
            var newResult = new APIResult()
            {
                Result = ((ObjectResult)action.Result)?.Value,
                Status = true,
                Message = "",
                Code = ResultCodeEnum.Success,
                Timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
            };
            action.Result = new ObjectResult(newResult);
            action.HttpContext.Response.Headers.Add("Custom-Header", Guid.NewGuid().ToString());
        }

    }

}
