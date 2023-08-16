#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023  NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：DESKTOP-4P8G8RH
 * 公司名称：
 * 命名空间：HuanTian.Infrastructure
 * 唯一标识：2a1ce92f-45a9-43c3-8a69-df6f28b3bd16
 * 文件名：AssemblyHelper
 * 当前用户域：DESKTOP-4P8G8RH
 * 
 * 创建者：wanglei
 * 电子邮箱：271976304@qq.com
 * 创建时间：2023/4/10 22:33:47
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
using Microsoft.Extensions.DependencyModel;
using System.Reflection;

namespace HuanTian.Infrastructure
{
    /// <summary>
    /// Assembly 加载帮助类
    /// </summary>
    public static class AssemblyHelper
    {
        /// <summary>
        /// 获取单个Assembly类型
        /// </summary>
        /// <param name="assemblyName">名字</param>
        /// <param name="path">路径</param>
        /// <returns></returns>
        public static Assembly GetAssembly(string assemblyName,string path = default)
        {
            // 需要暴露接口所在的程序集
            var basePath = AppContext.BaseDirectory; 
            if (!string.IsNullOrEmpty(path))
            {
                basePath = path;
            }
            
            var assembly = Assembly.LoadFrom(Path.Combine(basePath, $"{assemblyName}.dll"));
            return assembly;
        }
        /// <summary>
        /// 获取项目启动目录下所有的Assembly类型
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Assembly> GetAssemblyList() 
        {
            var dependencyContext = DependencyContext.Default;
            var assembliesList = dependencyContext
                .RuntimeLibraries
                .Where(library => !library.Serviceable && library.Type != "package")
                .Select(library => Assembly.Load(library.Name));
            return assembliesList;
        }
        /// <summary>
        /// 获取项目启动目录下所有的Type类型
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Type> GetAssemblyAllTypeList()
        {
            var assemblyList = GetAssemblyList();
            var typeList = new List<Type>();
            foreach (var item in assemblyList)
            {
                typeList.AddRange(item.GetTypes());
            }
            return typeList;
        }
    }
}
