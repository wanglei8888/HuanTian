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
using HuanTian.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

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
