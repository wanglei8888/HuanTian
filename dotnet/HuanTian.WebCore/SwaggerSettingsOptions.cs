#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023  NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：DESKTOP-4P8G8RH
 * 公司名称：
 * 命名空间：HuanTian.WebCore
 * 唯一标识：eee0a5ee-e28a-4322-8d48-754fe2058e2c
 * 文件名：SwaggerSettingsOptions
 * 当前用户域：DESKTOP-4P8G8RH
 * 
 * 创建者：wanglei
 * 电子邮箱：271976304@qq.com
 * 创建时间：2023/4/9 22:39:50
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
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace HuanTian.WebCore
{
    public class SwaggerSettingsOptions
    {
        /// <summary>
        /// 文档标题
        /// </summary>
        public string DocumentTitle { get; set; }

        /// <summary>
        /// 默认分组名
        /// </summary>
        public string DefaultGroupName { get; set; }

        /// <summary>
        /// 启用授权支持
        /// </summary>
        public bool? EnableAuthorized { get; set; }

        /// <summary>
        /// 格式化为V2版本
        /// </summary>
        public bool? FormatAsV2 { get; set; }

        /// <summary>
        /// 配置规范化文档地址
        /// </summary>
        public string RoutePrefix { get; set; }

        /// <summary>
        /// 文档展开设置
        /// </summary>
        public DocExpansion? DocExpansionState { get; set; }

        /// <summary>
        /// XML 描述文件
        /// </summary>
        public string[] XmlComments { get; set; }

        /// <summary>
        /// 分组信息
        /// </summary>
        public SpecificationOpenApiInfo[] GroupOpenApiInfos { get; set; }

        /// <summary>
        /// 配置 Servers
        /// </summary>
        public OpenApiServer[] Servers { get; set; }

        /// <summary>
        /// 隐藏 Servers
        /// </summary>
        public bool? HideServers { get; set; }

        /// <summary>
        /// 默认 swagger.json 路由模板
        /// </summary>
        public string RouteTemplate { get; set; } = "swagger/{documentName}/swagger.json";

        /// <summary>
        /// 配置安装第三方包的分组名
        /// </summary>
        public string[] PackagesGroups { get; set; }

        /// <summary>
        /// 启用枚举 Schema 筛选器
        /// </summary>
        public bool? EnableEnumSchemaFilter { get; set; }

        /// <summary>
        /// 启用标签排序筛选器
        /// </summary>
        public bool? EnableTagsOrderDocumentFilter { get; set; }

        /// <summary>
        /// 服务目录（修正 IIS 创建 Application 问题）
        /// </summary>
        public string ServerDir { get; set; }

        /// <summary>
        /// 配置规范化文档登录信息
        /// </summary>
        public SpecificationLoginInfo LoginInfo { get; set; }

        /// <summary>
        /// 启用 All Groups 功能
        /// </summary>
        public bool? EnableAllGroups { get; set; }

    }
    /// <summary>
    /// swagger 授权登录配置信息
    /// </summary>
    public sealed class SpecificationLoginInfo
    {
        /// <summary>
        /// 是否启用授权控制
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// 检查登录地址
        /// </summary>
        public string CheckUrl { get; set; }

        /// <summary>
        /// 提交登录地址
        /// </summary>
        public string SubmitUrl { get; set; }
    }
    /// <summary>
    /// 规范化文档开放接口信息
    /// </summary>
    public sealed class SpecificationOpenApiInfo : OpenApiInfo
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SpecificationOpenApiInfo()
        {
            Version = "1.0.0";
        }

        /// <summary>
        /// 分组私有字段
        /// </summary>
        private string _group;

        /// <summary>
        /// 所属组
        /// </summary>
        public string Group
        {
            get => _group;
            set
            {
                _group = value;
                Title ??= string.Join(' ', _group.SplitCamelCase());
            }
        }

        /// <summary>
        /// 排序
        /// </summary>
        public int? Order { get; set; }

        /// <summary>
        /// 是否可见
        /// </summary>
        public bool? Visible { get; set; }

        /// <summary>
        /// 路由模板
        /// </summary>
        public string RouteTemplate { get; internal set; }
    }
    /// <summary>
    /// Swagger 接口类排序
    /// </summary>
    public class TagsOrderDocumentFilter : IDocumentFilter
    {
        /// <summary>
        /// 配置拦截
        /// </summary>
        /// <param name="swaggerDoc"></param>
        /// <param name="context"></param>
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            swaggerDoc.Tags = Penetrates.ControllerOrderCollection.OrderByDescending(u => u.Value.Item2)
                .ThenBy(u => u.Key)
                .Select(c => new OpenApiTag
                {
                    Name = c.Value.Item1,
                    Description = swaggerDoc.Tags.FirstOrDefault(m => m.Name == c.Key)?.Description
                }).ToList();
        }
    }
}
