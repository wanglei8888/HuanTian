using HuanTian.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyModel;
using System.Linq;
using System.Reflection;
using System.Runtime;

namespace HuanTian.WebCore
{
    /// <summary>
    /// Appsetting操作类
    /// </summary>
    public static class App
    {
        /// <summary>
        /// 应用有效程序集
        /// </summary>
        public static readonly IEnumerable<Assembly> Assemblies;

        /// <summary>
        /// 全局配置选项
        /// </summary>
        public static IConfiguration Configuration => new ConfigurationBuilder().Build();

        /// <summary>
        /// 外部程序集
        /// </summary>
        public static IEnumerable<Assembly> ExternalAssemblies;

        /// <summary>
        /// 私有设置，避免重复解析
        /// </summary>
        public static AppSettingsOptions _settings;

        /// <summary>
        /// 应用全局配置
        /// </summary>
        public static AppSettingsOptions Settings => _settings ??= GetConfig<AppSettingsOptions>("AppSettings", true);

        public static IConfiguration? _configuration { get; set; }
        static App()
        {
            var assObject = GetAssemblies();
            Assemblies = assObject.Assemblies;
        }

        public static TOptions GetConfig<TOptions>(string path, bool loadPostConfigure = false)
        {
            var options = Appsettings._configuration.GetSection(path).Get<TOptions>();

            // 加载默认选项配置
            if (loadPostConfigure)
            {
                var postConfigure = typeof(TOptions).GetMethod("PostConfigure");
                if (postConfigure != null)
                {
                    options ??= Activator.CreateInstance<TOptions>();
                    postConfigure.Invoke(options, new object[] { options, _configuration });
                }
            }

            return options;
        }
        private static (IEnumerable<Assembly> Assemblies, IEnumerable<Assembly> ExternalAssemblies) GetAssemblies()
        {
            // 需排除的程序集后缀
            var excludeAssemblyNames = new string[] {
                "Database.Migrations"
            };
            // 读取应用配置
            var supportPackageNamePrefixs = Settings.SupportPackageNamePrefixs ?? Array.Empty<string>();

            IEnumerable<Assembly> scanAssemblies;
                
            // 获取入口程序集
            var entryAssembly = Assembly.GetEntryAssembly();

            // 非独立发布/非单文件发布
            if (!string.IsNullOrWhiteSpace(entryAssembly.Location))
            {
                var dependencyContext = DependencyContext.Default;

                // 读取项目程序集或 Furion 官方发布的包，或手动添加引用的dll，或配置特定的包前缀
                scanAssemblies = dependencyContext.RuntimeLibraries
                    .Where(u =>
                      (u.Type == "project" && !excludeAssemblyNames.Any(j => u.Name.EndsWith(j))) ||
                      (u.Type == "package" || supportPackageNamePrefixs.Any(p => u.Name.StartsWith(p))) ||
                      (Settings.EnabledReferenceAssemblyScan == true && u.Type == "reference"))    // 判断是否启用引用程序集扫描
               .Select(u => Reflect.GetAssembly(u.Name));
            }
            // 独立发布/单文件发布
            else
            {
                IEnumerable<Assembly> fixedSingleFileAssemblies = new[] { entryAssembly };

                // 扫描实现 ISingleFilePublish 接口的类型
                var singleFilePublishType = entryAssembly.GetTypes()
                                                    .FirstOrDefault(u => u.IsClass && !u.IsInterface && !u.IsAbstract && typeof(ISingleFilePublish).IsAssignableFrom(u));
                if (singleFilePublishType != null)
                {
                    var singleFilePublish = Activator.CreateInstance(singleFilePublishType) as ISingleFilePublish;

                    // 加载用户自定义配置单文件所需程序集
                    var nativeAssemblies = singleFilePublish.IncludeAssemblies();
                    var loadAssemblies = singleFilePublish.IncludeAssemblyNames()
                                                    .Select(u => Reflect.GetAssembly(u));

                    fixedSingleFileAssemblies = fixedSingleFileAssemblies.Concat(nativeAssemblies)
                                                                .Concat(loadAssemblies);

                    // 解决 Furion.Extras.ObjectMapper.Mapster 程序集不能加载问题
                    try
                    {
                        if (!fixedSingleFileAssemblies.Any(u => u.GetName().Name.Equals(ObjectMapperServiceCollectionExtensions.ASSEMBLY_NAME)))
                        {
                            fixedSingleFileAssemblies = fixedSingleFileAssemblies.Concat(new[] {
                            Reflect.GetAssembly(ObjectMapperServiceCollectionExtensions.ASSEMBLY_NAME) });
                        }
                    }
                    catch { }
                }
                else
                {
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Red;
                    // 提示没有正确配置单文件配置
                    Console.WriteLine(TP.Wrapper("Deploy Console"
                        , "Single file deploy error."
                        , "##Exception## Single file deployment configuration error."
                        , "##Documentation## https://furion.baiqian.ltd/docs/singlefile"));
                    Console.ResetColor();
                }

                // 通过 AppDomain.CurrentDomain 扫描，默认为延迟加载，正常只能扫描到 Furion 和 入口程序集（启动层）
                scanAssemblies = AppDomain.CurrentDomain.GetAssemblies()
                                        .Where(ass =>
                                                // 排除 System，Microsoft，netstandard 开头的程序集
                                                !ass.FullName.StartsWith(nameof(System))
                                                && !ass.FullName.StartsWith(nameof(Microsoft))
                                                && !ass.FullName.StartsWith("netstandard"))
                                        .Concat(fixedSingleFileAssemblies)
                                        .Distinct();
            }

            IEnumerable<Assembly> externalAssemblies = Array.Empty<Assembly>();

            // 加载 `appsetting.json` 配置的外部程序集
            if (Settings.ExternalAssemblies != null && Settings.ExternalAssemblies.Any())
            {
                foreach (var externalAssembly in Settings.ExternalAssemblies)
                {
                    // 加载外部程序集
                    var assemblyFileFullPath = Path.Combine(AppContext.BaseDirectory
                        , externalAssembly.EndsWith(".dll") ? externalAssembly : $"{externalAssembly}.dll");

                    // 根据路径加载程序集
                    var loadedAssembly = Reflect.LoadAssembly(assemblyFileFullPath);
                    if (loadedAssembly == default) continue;
                    var assembly = new[] { loadedAssembly };

                    // 合并程序集
                    scanAssemblies = scanAssemblies.Concat(assembly);
                    externalAssemblies = externalAssemblies.Concat(assembly);
                }
            }

            // 处理排除的程序集
            if (Settings.ExcludeAssemblies != null && Settings.ExcludeAssemblies.Any())
            {
                scanAssemblies = scanAssemblies.Where(ass => !Settings.ExcludeAssemblies.Contains(ass.GetName().Name, StringComparer.OrdinalIgnoreCase));
            }

            return (scanAssemblies, externalAssemblies);
        }

        /// <summary>
        /// 处理获取对象异常问题
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="action">获取对象委托</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>T</returns>
        private static T CatchOrDefault<T>(Func<T> action, T defaultValue = null)
            where T : class
        {
            try
            {
                return action();
            }
            catch
            {
                return defaultValue ?? null;
            }
        }


    }
}