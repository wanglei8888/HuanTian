﻿#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023  NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：DESKTOP-4P8G8RH
 * 公司名称：
 * 命名空间：HuanTian.WebCore.Filter
 * 唯一标识：7f0fbe36-3390-4781-907f-989f63ef0514
 * 文件名：testclass
 * 当前用户域：DESKTOP-4P8G8RH
 * 
 * 创建者：wanglei
 * 电子邮箱：271976304@qq.com
 * 创建时间：2023/4/18 20:42:38
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
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace HuanTian.WebCore
{
    /// <summary>
    /// 操作过滤器
    /// </summary>
    public class AsyncActionFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // 获取请求所带的参数
            var parameters = context.ActionArguments;

            // web api 请求参数验证
            // 获取所有带有RequiredAttribute的属性
            var properties = context.ActionDescriptor.Parameters
            .SelectMany(p => p.ParameterType.GetProperties())
            .Where(p => p.CustomAttributes.Any(a => a.AttributeType == typeof(RequiredAttribute)));
            foreach (var property in properties)
            {
                // 如果参数中贴有RequiredAttribute且未提供参数
                if (parameters.Count == 0)
                {
                    var requiredAttribute = property.GetCustomAttribute<RequiredAttribute>();
                    if (requiredAttribute != null)
                    {
                        throw new ArgumentException(requiredAttribute.ErrorMessage);
                    }
                }
                var errors = context.ModelState[property.Name]?.Errors
                .Select(e => e.ErrorMessage)
                .ToArray();
                if (errors != null && errors.Any()) { throw new ArgumentException(errors[0]); }
            }
            // 调用下一个中间件或过滤器
            var result = await next();

        }
    }
}
