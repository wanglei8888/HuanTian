#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023 Microsoft NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：MS-SOBOTFLKMEBA
 * 公司名称：Microsoft
 * 命名空间：HuanTian.WebCore
 * 唯一标识：eabc4168-21d1-4e65-961e-cd25f4b9e0cf
 * 文件名：CustomMiddleware
 * 当前用户域：MS-SOBOTFLKMEBA
 * 
 * 创建者：wanglei
 * 电子邮箱：
 * 创建时间：2023/3/25 10:51:30
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
        public async Task Invoke(HttpContext context, IServiceProvider serviceProvider)
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
