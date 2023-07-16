#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023  NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：DESKTOP-4P8G8RH
 * 公司名称：
 * 命名空间：HuanTian.Infrastructure
 * 唯一标识：8758aa41-de4d-4f84-aa65-16cf84c6cf15
 * 文件名：AttributeHelper
 * 当前用户域：DESKTOP-4P8G8RH
 * 
 * 创建者：wanglei
 * 电子邮箱：271976304@qq.com
 * 创建时间：2023/4/9 18:30:07
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
using System.Reflection;

namespace HuanTian.Infrastructure
{
    /// <summary>
    /// 特性 帮助类
    /// </summary>
    public static class AttributeHelper
    {
        /// <summary>
        /// 获取程序集下所有的特性值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static IEnumerable<T> GetAttributeValues<T>(Assembly assembly) where T : Attribute
        {
            foreach (Type type in assembly.GetTypes())
            {
                T attribute = type.GetCustomAttribute<T>();
                if (attribute != null)
                {
                    yield return attribute;
                }
            }
        }
    }
}
