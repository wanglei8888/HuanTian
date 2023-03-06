using Microsoft.Extensions.Configuration;
using System.Runtime;

namespace HuanTian.Common
{
    /// <summary>
    /// Appsetting操作类
    /// </summary>
    public class Appsettings
    {
        public static IConfiguration? _configuration { get; set; }

        public static IConfiguration Configuration => _configuration;
        public Appsettings(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        /// <summary>
        /// 获取键值信息
        /// </summary>
        /// <param name="path">例如ConnectionStrings:MySqlConnection</param>
        /// <returns></returns>
        public static string GetInfo(string path) 
        {
            return _configuration[path];
        }
    }
}