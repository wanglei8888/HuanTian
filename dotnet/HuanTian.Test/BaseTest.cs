#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023 HP NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：WEIHAN
 * 公司名称：HP
 * 命名空间：HuanTian.Test
 * 唯一标识：3d1a3f31-3da8-421b-ae60-02495f4dcd89
 * 文件名：BaseTest
 * 当前用户域：weihan
 * 
 * 创建者：wanglei
 * 电子邮箱：271976304@qq.com
 * 创建时间：2023/9/12 16:09:02
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
using Huangtian.Store;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace HuanTian.Test
{

    /// <summary>
    /// 测试基础类 
    /// </summary>
    public class BaseTest
    {
        protected TestServer Server { get; }
        protected HttpClient Client { get; }
        protected IServiceProvider ServiceProvider { get; }
        public BaseTest()
        {
            var application = new WebApplicationFactory<Program>();
            Client = application.CreateClient();
            Server = application.Server;
            ServiceProvider = Server.Services;
        }
        public T GetService<T>()
        {
            return ServiceProvider.GetService<T>();
        }
    }
}