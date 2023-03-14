using HuanTian.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Net;

namespace HuanTian.WebCore
{
    /// <summary>
    /// 异常处理
    /// </summary>
    public class HandlingExceptionFilter : IAsyncExceptionFilter
    {
        public HandlingExceptionFilter() 
        {
            
        }
        public async Task OnExceptionAsync(ExceptionContext context)
        {
            // 如果异常没有被处理则进行处理
            if (context.ExceptionHandled == false)
            {
                // 定义返回类型
                var result = new APIResult
                {
                    Code = ResultCodeEnum.NotSuccess,
                    Message = context.Exception.Message,
                    Result = null
                };
                context.Result = new ContentResult
                {
                    // 返回状态码设置为200，表示成功
                    StatusCode = (int)HttpStatusCode.OK,
                    // 设置返回格式
                    ContentType = "application/json;charset=utf-8",
                    Content = JsonConvert.SerializeObject(result)
                };
            }
            // 设置为true，表示异常已经被处理了
            context.ExceptionHandled = true;

            await Task.CompletedTask;

        }
    }
}
