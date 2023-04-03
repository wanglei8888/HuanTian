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
            mvcBuilder.Services.AddDynamicApiControllers(); 
            mvcBuilder.Services.AddSwaggerService();
            return mvcBuilder;
        }
    }
}
