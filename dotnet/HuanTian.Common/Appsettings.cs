using Microsoft.Extensions.Configuration;
using System.Runtime;

namespace HuanTian.Common
{
    /// <summary>
    /// Appsetting操作类
    /// </summary>
    public class Appsettings
    {
        /// <summary>
        /// 获取键值信息
        /// </summary>
        public static IConfiguration Configuration { get; set; }
        public Appsettings(IConfiguration configuration)
        {
            Configuration = configuration;
        }
    }
}