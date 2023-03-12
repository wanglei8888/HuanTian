using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuanTian.WebCore
{
    /// <summary>
    /// 自定义中间件，功能暂未实现
    /// </summary>
    public class CustomMiddleware
    {
        private readonly RequestDelegate _requestDelegate;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="requestDelegate">请求委托</param>
        public CustomMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;

        }

        /// <summary>
        /// 回调
        /// </summary>
        /// <param name="context">Http内容</param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            //try
            //{
            await _requestDelegate(context);
            //}
            //catch (Exception ex)
            //{
            //    context.Response.StatusCode = 500;

            //    //_log.Error("服务器错误:" + ex.Message);
            //    //await ResponseHelper.HandleExceptionAsync(500, "服务器错误:" + ex.Message);
            //}
        }
    }
}
