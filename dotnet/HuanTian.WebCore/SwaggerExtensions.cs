#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023 Microsoft NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：MS-SOBOTFLKMEBA
 * 公司名称：Microsoft
 * 命名空间：HuanTian.WebCore
 * 唯一标识：eabc4168-21d1-4e65-961e-cd25f4b9e0cf
 * 文件名：SwaggerExtensions
 * 当前用户域：MS-SOBOTFLKMEBA
 * 
 * 创建者：wanglei
 * 电子邮箱：
 * 创建时间：2023/3/9 11:59:30
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
using HuanTian.Infrastructure;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using NPOI.SS.Formula.Functions;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Concurrent;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Xml.XPath;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Text;
using Microsoft.Extensions.DependencyModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using Org.BouncyCastle.Asn1.Ocsp;

namespace HuanTian.WebCore
{
    /// <summary>
    /// swagger 文档配置选项
    /// </summary>
    public static class SwaggerExtensions
    {
        /// <summary>
        /// 获取分组信息缓存集合
        /// </summary>
        private static readonly ConcurrentDictionary<string, SpecificationOpenApiInfo> GetGroupOpenApiInfoCached;
        /// <summary>
        /// 文档分组列表
        /// </summary>
        public static readonly IEnumerable<string> DocumentGroups;
        /// <summary>
        /// 规范化文档配置
        /// </summary>
        private static readonly SwaggerSettingsOptions _swaggerSettings;
        static SwaggerExtensions()
        {
            // 载入配置
            _swaggerSettings = App.GetConfig<SwaggerSettingsOptions>("SwaggerSettings", true);
            GetGroupOpenApiInfoCached = new ConcurrentDictionary<string, SpecificationOpenApiInfo>();
            DocumentGroups = ReadGroups();
        }
        /// <summary>
        /// 添加Swagger中间件
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static void BuildSwaggerService(SwaggerGenOptions swaggerGenOptions, Action<SwaggerGenOptions> configure = null)
        {
            // 创建分组文档
            CreateSwaggerDocs(swaggerGenOptions);

            // action排序
            // ConfigureActionSequence(swaggerGenOptions);

            // 加载注释文件
            LoadXmlComments(swaggerGenOptions);

            // congtrole控制器排序操作
            swaggerGenOptions.DocumentFilter<TagsOrderDocumentFilter>();

            // 增加鉴权认证
            swaggerGenOptions.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "Value:{token}",
                Name = App.Configuration["AppSettings:ApiHeard"],
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });
            swaggerGenOptions.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {{
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }, Scheme = "oauth2", Name = "Bearer", In = ParameterLocation.Header }, new List<string>()
                }

            });

            // 自定义配置
            configure?.Invoke(swaggerGenOptions);
        }


        /// <summary>
        /// Swagger UI 构建
        /// </summary>
        /// <param name="swaggerUIOptions"></param>
        /// <param name="routePrefix"></param>
        /// <param name="configure"></param>
        public static void BuildSwaggerUI(SwaggerUIOptions swaggerUIOptions, string routePrefix = default, Action<SwaggerUIOptions> configure = null)
        {
            // 配置分组终点路由
            CreateGroupEndpoint(swaggerUIOptions);

            // 配置文档标题
            swaggerUIOptions.DocumentTitle = _swaggerSettings.DocumentTitle;

            // 配置UI地址（处理二级虚拟目录）
            if (!string.IsNullOrEmpty(_swaggerSettings.RoutePrefix))
            {
               swaggerUIOptions.RoutePrefix = _swaggerSettings.RoutePrefix ?? routePrefix ?? "api";
            }
            
            // 文档展开设置
            swaggerUIOptions.DocExpansion(_swaggerSettings.DocExpansionState ?? DocExpansion.None);

            // 自定义 Swagger 首页
            CustomizeIndex(swaggerUIOptions);

            // 配置多语言和自动登录token
            // swaggerUIOptions.UseRequestInterceptor("function(request) { return defaultRequestInterceptor(request); }");
            // swaggerUIOptions.UseResponseInterceptor("function(response) { return defaultResponseInterceptor(response); }");

            // 自定义配置
            configure?.Invoke(swaggerUIOptions);
        }

        #region 详细代码

        /// <summary>
        /// 加载注释描述文件
        /// </summary>
        /// <param name="swaggerGenOptions">Swagger 生成器配置</param>
        private static void LoadXmlComments(SwaggerGenOptions swaggerGenOptions)
        {
            // 如果需要其他的程序集加载api swagger文档,可以在该数组中加，也可以通过扫描全局添加。
            var xmlComments = new string[] { "HuanTian.Service" };

            var members = new Dictionary<string, XElement>();

            // 支持注释完整特性
            foreach (var xmlComment in xmlComments)
            {
                var fileCount = Directory.GetFiles(AppContext.BaseDirectory, "*.xml");
                var assemblyXmlName = xmlComment.EndsWith(".xml") ? xmlComment : $"{xmlComment}.xml";
                foreach (string file in fileCount)
                {
                    if (File.Exists(file))
                    {
                        var xmlDoc = XDocument.Load(file);

                        #region 为了方便面向接口开发，备注不用接口、实现类写两遍 如果只有接口有备注就把接口的备注替换到实现类里 因为Swagger只显示实现类的备注
                        // 查找所有 member[name] 节点，且不包含 <inheritdoc /> 节点的注释
                        var memberNotInheritdocElementList = xmlDoc.XPathSelectElements("/doc/members/member[@name and not(inheritdoc)]").ToList();
                        for (int i = 0; i < memberNotInheritdocElementList.Count(); i++)
                        {
                            var name = memberNotInheritdocElementList[i].Attribute("name").Value;
                            // 去除空格参数的数据
                            var removeSpacesName = Regex.Replace(name, @"\([^)]*\)", "");
                            // 获取类名
                            var className = removeSpacesName.Split('.')[2];
                            if (className.StartsWith("I"))
                            {
                                // 获取完整的方法名
                                var serviceName = $"{removeSpacesName.Split('.')[0]}.{removeSpacesName.Split('.')[1]}.{className.Substring(1)}";
                                XElement? service = null;

                                // 如果是接口备注
                                if (name.StartsWith("T"))
                                {
                                    // 查询是否只有接口的备注
                                    service = memberNotInheritdocElementList.FirstOrDefault(t => t.Attribute("name").Value == serviceName);
                                    if (service == null)
                                    {
                                        memberNotInheritdocElementList[i].Attribute("name").Value = serviceName;
                                    }
                                    continue;
                                }

                                // 获取方法名
                                var methodName = removeSpacesName.Split('.')[3];
                                serviceName = serviceName + $".{methodName}";

                                // 获取方法参数
                                var match = Regex.Match(name, @"\((.*?)\)"); // 匹配括号内的任何字符，非贪婪模式
                                string parameters = "";
                                if (match.Success)
                                {
                                    parameters = match.Groups[1].Value;
                                    serviceName = serviceName + $"({parameters})";
                                }

                                // 查询是否只有接口-方法的备注
                                service = memberNotInheritdocElementList.FirstOrDefault(t => t.Attribute("name").Value == serviceName);
                                if (service == null)
                                {
                                    memberNotInheritdocElementList[i].Attribute("name").Value = serviceName;
                                }
                            }
                        } 
                        #endregion

                        // 创建一个新的 XDocument，将两个 XElement 列表合并到一起
                        XDocument newXmlDoc = new XDocument();

                        // 创建根元素
                        XElement rootElement = new XElement("doc");

                        // 添加子元素
                        var xmlName = Path.GetFileName(file);
                        xmlName = xmlName.Substring(0, xmlName.LastIndexOf('.'));

                        XElement assemblyElement = new XElement("assembly",
                            new XElement("name", xmlName));
                        XElement membersElement = new XElement("members", memberNotInheritdocElementList);

                        // 将元素添加到根元素中
                        rootElement.Add(assemblyElement);
                        rootElement.Add(membersElement);

                        // 将根元素添加到新的XDocument对象中
                        newXmlDoc.Add(rootElement);

                        swaggerGenOptions.IncludeXmlComments(() => new XPathDocument(newXmlDoc.CreateReader()), true);
                    }
                }
                
            }
        }
        /// <summary>
        ///  配置 Action 排序
        /// </summary>
        /// <param name="swaggerGenOptions"></param>
        private static void ConfigureActionSequence(SwaggerGenOptions swaggerGenOptions)
        {
            swaggerGenOptions.OrderActionsBy(apiDesc =>
            {
                var apiDescriptionSettings = apiDesc.CustomAttributes()
                                       .FirstOrDefault(u => u.GetType() == typeof(ApiDescriptionSettingsAttribute))
                                       as ApiDescriptionSettingsAttribute ?? new ApiDescriptionSettingsAttribute();

                return (int.MaxValue - apiDescriptionSettings.Order).ToString()
                                .PadLeft(int.MaxValue.ToString().Length, '0');
            });
        }

        /// <summary>
        /// 创建分组文档
        /// </summary>
        /// <param name="swaggerGenOptions">Swagger生成器对象</param>
        private static void CreateSwaggerDocs(SwaggerGenOptions swaggerGenOptions)
        {
            foreach (var group in DocumentGroups)
            {
                var groupOpenApiInfo = GetGroupOpenApiInfo(group) as OpenApiInfo;
                swaggerGenOptions.SwaggerDoc(group, groupOpenApiInfo);
            }
        }
        /// <summary>
        /// 读取所有分组信息
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<string> ReadGroups()
        {
            // 获取所有的控制器和动作方法
            var assemblies = AssemblyHelper.GetAssemblyList();
            // 扫描所有类的特性
            var attributeList = new List<ApiDescriptionSettingsAttribute>();
            foreach (var assembly in assemblies)
            {
                attributeList.AddRange(AttributeHelper.GetAttributeValues<ApiDescriptionSettingsAttribute>(assembly));
            }
            // 获取特性的分组名字
            var groupName = new List<string>();
            foreach (var item in attributeList)
            {
                groupName.Add(item.GroupName);
            }
            // 如果没有设置分组信息,则默认分组
            if (groupName.Count == 0)
            {
                groupName.Add("default");
            }
            return groupName;
        }
        /// <summary>
        /// 获取分组配置信息
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public static SpecificationOpenApiInfo GetGroupOpenApiInfo(string group)
        {
            return GetGroupOpenApiInfoCached.GetOrAdd(group, Function);

            // 本地函数
            static SpecificationOpenApiInfo Function(string group)
            {
                // 替换路由模板
                var routeTemplate = _swaggerSettings.RouteTemplate.Replace("{documentName}", Uri.EscapeDataString(group));
                if (!string.IsNullOrWhiteSpace(_swaggerSettings.ServerDir))
                {
                    routeTemplate = _swaggerSettings.ServerDir + "/" + routeTemplate;
                }

                // 处理虚拟目录问题
                var template = $"/{routeTemplate}";

                var groupInfo = _swaggerSettings.GroupOpenApiInfos.FirstOrDefault(u => u.Group == group);
                if (groupInfo != null)
                {
                    groupInfo.RouteTemplate = template;
                    groupInfo.Title ??= group;
                }
                else
                {
                    groupInfo = new SpecificationOpenApiInfo { Group = group, RouteTemplate = template };
                }

                return groupInfo;
            }
        }

        public static void CustomizeIndex(SwaggerUIOptions swaggerUIOptions)
        {
            // 加载目标程序集
            var thisAssembly = AssemblyHelper.GetAssembly("HuanTian.Store");
            // 判断是否启用 MiniProfile
            var customIndex = $"HuanTian.Store.wwwroot.swagger-index.html";
            swaggerUIOptions.IndexStream = () =>
            {
                StringBuilder htmlBuilder;
                // 自定义首页模板参数
                var indexArguments = new Dictionary<string, string>
                {
                    {"%(VirtualPath)","" }    // 解决二级虚拟目录 MiniProfiler 丢失问题
                };

                // 读取文件内容
                using (var stream = thisAssembly.GetManifestResourceStream(customIndex))
                {
                    using var reader = new StreamReader(stream);
                    htmlBuilder = new StringBuilder(reader.ReadToEnd());
                }

                // 替换模板参数
                foreach (var (template, value) in indexArguments)
                {
                    htmlBuilder.Replace(template, value);
                }

                // 返回新的内存流
                var byteArray = Encoding.UTF8.GetBytes(htmlBuilder.ToString());
                return new MemoryStream(byteArray);
            };
        }

        /// <summary>
        /// 配置分组终点路由
        /// </summary>
        /// <param name="swaggerUIOptions"></param>
        private static void CreateGroupEndpoint(SwaggerUIOptions swaggerUIOptions)
        {
            foreach (var group in DocumentGroups)
            {
                var groupOpenApiInfo = GetGroupOpenApiInfo(group);

                swaggerUIOptions.SwaggerEndpoint(groupOpenApiInfo.RouteTemplate, groupOpenApiInfo?.Title ?? group);

            }
        }
        #endregion
    }



    
}