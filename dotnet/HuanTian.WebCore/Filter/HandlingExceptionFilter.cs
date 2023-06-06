#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023 Microsoft NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：MS-SOBOTFLKMEBA
 * 公司名称：Microsoft
 * 命名空间：HuanTian.WebCore
 * 唯一标识：eabc4168-21d1-4e65-961e-cd25f4b9e0cf
 * 文件名：TemplateResultFilter
 * 当前用户域：MS-SOBOTFLKMEBA
 * 
 * 创建者：wanglei
 * 电子邮箱：
 * 创建时间：2023/3/18 16:31:25
 * 版本：V1.0.0
 * 描述：
 *
 * ----------------------------------------------------------------
 * 修改人：
 * 时间：
 * 修改说明：
 *
 * 版本：V1.0.1
 *----------------------------------------------------------------*/
#endregion << 版 本 注 释 >>
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
                switch (context.Exception)
                {
                    // 友好报错机制 状态码为200
                    case FriendlyException:
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                        break;
                    default:
                        // 默认状态码返回500
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }
                context.Result = RequestHelper.ErroRequestEntity(context.Exception.Message, HttpStatusCode.InternalServerError);
            }
            // 设置为true，表示异常已经被处理了
            context.ExceptionHandled = true;
            await Task.CompletedTask;

        }
    }
}
