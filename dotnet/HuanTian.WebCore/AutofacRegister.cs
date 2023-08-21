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
using HuanTian.EntityFrameworkCore;
using HuanTian.SqlSugar;
using System.Reflection;

namespace HuanTian.WebCore
{
    public class AutofacRegister : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // 加载依赖注入
            RegisterTypes(builder);

            // 需要暴露接口所在的程序集
            var basePath = AppContext.BaseDirectory;

            // 加载目标程序集
            var Services = AssemblyHelper.GetAssemblyArray(); 

            // 扫描继承 ISingleton 接口的所有类 注入为单例
            builder.RegisterAssemblyTypes(Services)
                       .Where(t => typeof(ISingleton).IsAssignableFrom(t))
                       .AsImplementedInterfaces()
                       .SingleInstance();
            // 扫描继承 IScoped 接口的所有类 注入为作用域
            builder.RegisterAssemblyTypes(Services)
                       .Where(t => typeof(IScoped).IsAssignableFrom(t))
                       .AsImplementedInterfaces()
                       .InstancePerLifetimeScope();
            // 扫描继承 ITransient 接口的所有类 注入为瞬时
            builder.RegisterAssemblyTypes(Services)
                       .Where(t => typeof(ITransient).IsAssignableFrom(t))
                       .AsImplementedInterfaces()
                       .InstancePerDependency();
            //var container = builder.Build();
            //var registeredServices = container.ComponentRegistry.Registrations;
            //var container = builder.Build();

        }
        private void RegisterTypes(Autofac.ContainerBuilder builder)
        {

            // 注册生命周期为单例   Singleton 的类 builder.RegisterType(type).AsImplementedInterfaces().SingleInstance();
            // 注册生命周期为作用域 Scoped    的类 builder.RegisterType(type).AsImplementedInterfaces().InstancePerLifetimeScope();
            // 注册生命周期为瞬时   Transient 的类 builder.RegisterType(type).AsImplementedInterfaces().InstancePerDependency();
            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
            builder.RegisterType<StartupFilter>().As<IStartupFilter>().InstancePerDependency();
            builder.RegisterType<RabbitMQMessageQueue>().As<IMessageQueue>()
                .WithParameter("connectionString", App.Configuration["ConnectionStrings:RabbitMQ"]).InstancePerLifetimeScope();
            builder.RegisterType<RedisCache>().As<IRedisCache>()
                .WithParameter("connectionString", App.Configuration["ConnectionStrings:Redis"]).SingleInstance();

        }

    }
}