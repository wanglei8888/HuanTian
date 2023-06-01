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
 * 创建时间：2023/3/20 19:51:06
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
using System.Net;

namespace HuanTian.WebCore
{
    /// <summary>
    /// 结果过滤器,用于规范返回数据模板
    /// </summary>
    public class TemplateResultFilter : IAsyncResultFilter
    {
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var result = context.Result is EmptyResult ? null : context.Result; // 如果是空结果则返回null
            switch (result)
            {
                case FileResult:
                    break;
                default:
                    // 统一返回结果
                    context.Result = RequestHelper.ErroRequestEntity("success", HttpStatusCode.OK, result);
                    break;
            }

            context.HttpContext.Response.Headers.Add("Custom-Header", Guid.NewGuid().ToString());
            // 调用下一个中间件或过滤器
            var resultContext = await next();

        }
    }

}
