#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023  NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：DESKTOP-6BOHDBE
 * 公司名称：
 * 命名空间：HuanTian.WebCore
 * 唯一标识：a8801f8a-fe18-4e29-a71a-209fef9e9301
 * 文件名：RequestHelper
 * 当前用户域：DESKTOP-6BOHDBE
 * 
 * 创建者：wanglei
 * 电子邮箱：271976304@qq.com
 * 创建时间：2023/5/27 17:29:06
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

using Mapster;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;


namespace HuanTian.Infrastructure
{
    /// <summary>
    /// Api 请求返回参数
    /// </summary>
    public class RequestHelper
    {
        /// <summary>
        /// 请求返回信息 返回实体
        /// </summary>
        /// <param name="erroMessage">信息</param>
        /// <param name="code">状态码</param>
        /// <returns>Entity Value</returns>
        public static ObjectResult RequestInfo(string message, HttpStatusCode code, dynamic result = default)
        {
            return new ObjectResult( new APIResult
            {
                Code = code,
                Message = message,
                Timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                Result = (result as ObjectResult)?.Value
            });
        }
    }
}