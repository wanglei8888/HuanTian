using Microsoft.Extensions.Configuration;

namespace HuanTian.Common
{
    /// <summary>
    /// Appsetting操作类
    /// </summary>
    public class Appsettings
    {
        public static IConfiguration? _configuration { get; set; }
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