using Autofac;
using Autofac.Core;
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

            var IServices = Assembly.LoadFrom(Path.Combine(basePath, "HuanTian.Interface.dll"));
            var Services = Assembly.LoadFrom(Path.Combine(basePath, "HuanTian.Service.dll"));

            container.RegisterAssemblyTypes(IServices, Services)

            .Where(t => t.Name.EndsWith("Service")) //|| t.Name.EndsWith("Work"))
            .AsImplementedInterfaces()
            .EnableInterfaceInterceptors();
        }
    }
}