using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace HuanTian.WebCore
{
    public static class ObjectMapperServiceCollectionExtensions
    {
        /// <summary>
        /// 对象映射程序集名称
        /// </summary>
        public const string ASSEMBLY_NAME = "Furion.Extras.ObjectMapper.Mapster";

        /// <summary>
        /// 添加对象映射
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <returns></returns> 
        public static IServiceCollection AddObjectMapper(this IServiceCollection services)
        {
            // 判断是否安装了 Mapster 程序集
            var objectMapperAssembly = App.Assemblies.FirstOrDefault(u => u.GetName().Name.Equals(ASSEMBLY_NAME));
            if (objectMapperAssembly != null)
            {
                // 加载 ObjectMapper 拓展类型和拓展方法
                var objectMapperServiceCollectionExtensionsType = Reflect.GetType(objectMapperAssembly, $"Microsoft.Extensions.DependencyInjection.ObjectMapperServiceCollectionExtensions");
                var addObjectMapperMethod = objectMapperServiceCollectionExtensionsType
                    .GetMethods(BindingFlags.Public | BindingFlags.Static)
                    .First(u => u.Name == "AddObjectMapper");

                return addObjectMapperMethod.Invoke(null, new object[] { services, App.Assemblies.ToArray() }) as IServiceCollection;
            }

            return services;
        }
    }
}
