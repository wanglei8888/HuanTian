#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023  NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：DESKTOP-4P8G8RH
 * 公司名称：
 * 命名空间：HuanTian.Infrastructure.Inteface
 * 唯一标识：964c9200-b19a-461e-9a1c-c549760cbaad
 * 文件名：IRedisCache
 * 当前用户域：DESKTOP-4P8G8RH
 * 
 * 创建者：wanglei
 * 电子邮箱：271976304@qq.com
 * 创建时间：2023/4/16 11:56:25
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

namespace HuanTian.Entities
{
    /// <summary>
    /// Redis缓存接口
    /// </summary>
    public interface IRedisCache
    {
        /// <summary>
        /// String(字符串)获取缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<T> StringGetAsync<T>(string key);
        /// <summary>
        /// String(字符串)获取缓存键是否存在
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<bool> StringContainsAsync<T>(string key, T value);
        /// <summary>
        /// String(字符串)插入一个字符串类型的键值对
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="value">键值</param>
        /// <param name="expiration">过期时间(默认10分钟)</param>
        /// <returns>是否插入成功</returns>
        Task<bool> StringAddAsync<T>(string key, T value, TimeSpan? expiration = null);
        /// <summary>
        /// String(字符串)删除缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<bool> StringDeleteAsync(string key);
        /// <summary>
        /// Set(集合)将一个元素添加到集合中
        /// </summary>
        /// <param name="key">集合键名</param>
        /// <param name="values">元素</param>
        /// <returns>成功添加的元素数量</returns>
        Task<bool> SetAddAsync(string key, string values, TimeSpan? expiration = null);
        /// <summary>
        /// Set(集合)将一个或多个元素添加到集合中
        /// </summary>
        /// <param name="key">集合键名</param>
        /// <param name="values">元素</param>
        /// <returns>成功添加的元素数量</returns>
        Task<long> SetAddAsync(string key, string[] values, TimeSpan? expiration = null);
        /// <summary>
        /// Set(集合)移除集合中的一个或多个元素
        /// </summary>
        /// <param name="key">集合键名</param>
        /// <param name="values">元素</param>
        /// <returns>成功移除的元素数量</returns>
        Task<long> SetRemoveAsync(string key, params string[] values);
        /// <summary>
        /// Set(集合)获取集合中的所有元素
        /// </summary>
        /// <param name="key">集合键名</param>
        /// <returns>集合中的所有元素</returns>
        Task<IEnumerable<string>> SetGetAllAsync(string key);
        /// <summary>
        /// Set(集合)判断集合中是否包含指定元素
        /// </summary>
        /// <param name="key">集合键名</param>
        /// <param name="value">元素</param>
        /// <returns>是否包含</returns>
        Task<bool> SetContainsAsync(string key, string value);
        /// <summary>
        /// 获取集合中元素的数量
        /// </summary>
        /// <param name="key">集合键名</param>
        /// <returns>元素数量</returns>
        Task<long> SetGetCountAsync(string key);
    }
}
