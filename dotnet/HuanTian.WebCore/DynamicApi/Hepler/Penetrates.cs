using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;
using System.Reflection;

namespace HuanTian.WebCore
{
    internal static class Penetrates
    {
        /// <summary>
        /// 分组分隔符
        /// </summary>
        internal const string GroupSeparator = "@";

        /// <summary>
        /// 请求动词映射字典
        /// </summary>
        internal static ConcurrentDictionary<string, string> VerbToHttpMethods { get; private set; }

        /// <summary>
        /// 控制器排序集合
        /// </summary>
        internal static ConcurrentDictionary<string, (string, int)> ControllerOrderCollection { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        static Penetrates()
        {
            ControllerOrderCollection = new ConcurrentDictionary<string, (string, int)>();

            VerbToHttpMethods = new ConcurrentDictionary<string, string>
            {
                ["post"] = "POST",
                ["add"] = "POST",
                ["create"] = "POST",
                ["insert"] = "POST",
                ["submit"] = "POST",

                ["get"] = "GET",
                ["find"] = "GET",
                ["fetch"] = "GET",
                ["query"] = "GET",
                //["getlist"] = "GET",
                //["getall"] = "GET",

                ["put"] = "PUT",
                ["update"] = "PUT",

                ["delete"] = "DELETE",
                ["remove"] = "DELETE",
                ["clear"] = "DELETE",

                ["patch"] = "PATCH"
            };

            IsApiControllerCached = new ConcurrentDictionary<Type, bool>();
        }

        /// <summary>
        /// <see cref="IsApiController(Type)"/> 缓存集合
        /// </summary>
        private static readonly ConcurrentDictionary<Type, bool> IsApiControllerCached;

        /// <summary>
        /// 获取选项配置
        /// </summary>
        /// <param name="optionsType">选项类型</param>
        /// <returns></returns>
        internal static (OptionsSettingsAttribute, string) GetOptionsConfiguration(Type optionsType)
        {
            var optionsSettings = optionsType.GetCustomAttribute<OptionsSettingsAttribute>(false);

            // 默认后缀
            var defaultStuffx = nameof(HuanTian.WebCore);

            return (optionsSettings, optionsSettings switch
            {
                // // 没有贴 [OptionsSettings]，如果选项类以 `Options` 结尾，则移除，否则返回类名称
                null => optionsType.Name.EndsWith(defaultStuffx) ? optionsType.Name[0..^defaultStuffx.Length] : optionsType.Name,
                // 如果贴有 [OptionsSettings] 特性，但未指定 Path 参数，则直接返回类名，否则返回 Path
                _ => optionsSettings != null && string.IsNullOrWhiteSpace(optionsSettings.Path) ? optionsType.Name : optionsSettings.Path,
            });
        }

        /// <summary>
        /// 是否是Api控制器
        /// </summary>
        /// <param name="type">type</param>
        /// <returns></returns>
        internal static bool IsApiController(Type type)
        {
            return IsApiControllerCached.GetOrAdd(type, Function);

            // 本地静态方法
            static bool Function(Type type)
            {
                // 排除 OData 控制器
                if (type.Assembly.GetName().Name.StartsWith("Microsoft.AspNetCore.OData")) return false;

                // 不能是非公开、基元类型、值类型、抽象类、接口、泛型类
                if (!type.IsPublic || type.IsPrimitive || type.IsValueType || type.IsAbstract || type.IsInterface || type.IsGenericType) return false;

                // 继承 ControllerBase 或 实现 IDynamicApiController 的类型 或 贴了 [DynamicApiController] 特性
                if ((!typeof(Controller).IsAssignableFrom(type) && typeof(ControllerBase).IsAssignableFrom(type)) || typeof(IDynamicApiController).IsAssignableFrom(type) || type.IsDefined(typeof(DynamicApiControllerAttribute), true)) return true;

                return false;
            }
        }
    }
}
