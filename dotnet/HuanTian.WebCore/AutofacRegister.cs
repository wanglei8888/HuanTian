#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023 Microsoft NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：MS-SOBOTFLKMEBA
 * 公司名称：Microsoft
 * 命名空间：HuanTian.WebCore
 * 唯一标识：eabc4168-21d1-4e65-961e-cd25f4b9e0cf
 * 文件名：AutofacRegister
 * 当前用户域：MS-SOBOTFLKMEBA
 * 
 * 创建者：wanglei
 * 电子邮箱：
 * 创建时间：2023/2/26 18:51:41
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
using Autofac;
using Autofac.Extras.DynamicProxy;
using System.Reflection;

namespace HuanTian.WebCore
{
    public class AutofacRegister : Autofac.Module
    {
        protected override void Load(ContainerBuilder container)
        {
            // 需要暴露接口所在的程序集
            var basePath = AppContext.BaseDirectory;

            // var IServices = Assembly.LoadFrom(Path.Combine(basePath, "HuanTian.Interface.dll"));
            var Services = Assembly.LoadFrom(Path.Combine(basePath, "HuanTian.Service.dll"));

            container.RegisterAssemblyTypes(Services)

            .Where(t => t.Name.EndsWith("Service")) //|| t.Name.EndsWith("Work"))
            .AsImplementedInterfaces()
            .EnableInterfaceInterceptors();
        }
    }
}