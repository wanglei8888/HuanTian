namespace HuanTian.WebCore
{
    public class AppSettingsOptions
    {
        /// <summary>
        /// 集成 MiniProfiler 组件
        /// </summary>
        public bool? InjectMiniProfiler { get; set; }

        /// <summary>
        /// 是否启用规范化文档
        /// </summary>
        public bool? InjectSpecificationDocument { get; set; }

        /// <summary>
        /// 是否启用引用程序集扫描
        /// </summary>
        public bool? EnabledReferenceAssemblyScan { get; set; }

        /// <summary>
        /// 外部程序集
        /// </summary>
        /// <remarks>扫描 dll 文件，如果是单文件发布，需拷贝放在根目录下</remarks>
        public string[] ExternalAssemblies { get; set; }

        /// <summary>
        /// 排除扫描的程序集
        /// </summary>
        public string[] ExcludeAssemblies { get; set; }

        /// <summary>
        /// 是否打印数据库连接信息到 MiniProfiler 中
        /// </summary>
        public bool? PrintDbConnectionInfo { get; set; }

        /// <summary>
        /// 是否输出原始 Sql 执行日志（ADO.NET）
        /// </summary>
        public bool? OutputOriginalSqlExecuteLog { get; set; }

        /// <summary>
        /// 配置支持的包前缀名
        /// </summary>
        public string[] SupportPackageNamePrefixs { get; set; }

        /// <summary>
        /// 【部署】二级虚拟目录
        /// </summary>
        public string VirtualPath { get; set; }

    }
}
