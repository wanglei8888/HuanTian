using Microsoft.Extensions.DependencyInjection;


namespace HuanTian.WebCore
{
    /// <summary>
    /// 应用服务集合拓展类（由框架内部调用）
    /// </summary>
    public static class AppServiceCollectionExtensions
    {
        /// <summary>
        /// Mvc 注入基础配置（带Swagger）
        /// </summary>
        /// <param name="mvcBuilder">Mvc构建器</param>
        /// <param name="configure"></param>
        /// <returns>IMvcBuilder</returns>
        public static IMvcBuilder AddInject(this IMvcBuilder mvcBuilder)
        {

            //// 项目引用不在的时候,无法显示API的时候用上此句,加载未引用的包
            // var dependencyContext = DependencyContext.Default;
            // var assemblies = dependencyContext
            //     .RuntimeLibraries
            //     .Where(library => !library.Serviceable && library.Type != "package")
            //     .Select(library => Assembly.Load(library.Name));
            // var basePath = AppContext.BaseDirectory.Replace("HuanTian.Store", "HuanTian.Service");
            // var Services = Assembly.LoadFrom(Path.Combine(basePath, "HuanTian.Service.dll"));
            // mvcBuilder.AddApplicationParts(Services);

            mvcBuilder.Services.AddDynamicApiControllers();
            mvcBuilder.Services.AddSwaggerService();
            return mvcBuilder;
        }
    }
}
