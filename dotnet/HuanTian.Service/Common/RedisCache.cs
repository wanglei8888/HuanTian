#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023  NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：DESKTOP-4P8G8RH
 * 公司名称：
 * 命名空间：HuanTian.Service.Common
 * 唯一标识：080cda8e-bfed-4e5b-9a14-42eeb47302be
 * 文件名：RedisCacheHelper
 * 当前用户域：DESKTOP-4P8G8RH
 * 
 * 创建者：wanglei
 * 电子邮箱：271976304@qq.com
 * 创建时间：2023/4/16 11:58:50
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
using StackExchange.Redis;
using System.Text.Json;

namespace HuanTian.Service
{
    /// <summary>
    /// Redis缓存实现类
    /// </summary>
    public class RedisCache : IRedisCache
    {
        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase _database;

        public RedisCache(string connectionString)
        {
            _redis = ConnectionMultiplexer.Connect(connectionString);
            _database = _redis.GetDatabase();
        }

        #region String(字符串)
        public async Task<T> StringGetAsync<T>(string key)
        {
            var value = await _database.StringGetAsync(key);
            if (value.HasValue)
            {
                return JsonSerializer.Deserialize<T>(value);
            }
            return default;
        }


        public async Task<bool> StringDeleteAsync(string key)
        {
            return await _database.KeyDeleteAsync(key);
        }

        public async Task<bool> StringContainsAsync<T>(string key, T value)
        {
            var serializedValue = await _database.ListGetByIndexAsync("", 0);
            var redisValue = await _database.StringGetAsync(key);
            if (redisValue.HasValue)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> StringAddAsync<T>(string key, T value, TimeSpan? expiration)
        {
            var options = new JsonSerializerOptions { IgnoreNullValues = true };
            var serializedValue = JsonSerializer.Serialize(value, options);
            // 默认指定储存10分钟
            return await _database.StringSetAsync(key, serializedValue, expiration ?? App.Configuration["AppSettings:RedisExpirationTime"].ToTimeSpan());
        }
        #endregion

        #region Set(集合)

        public async Task<bool> SetAddAsync(string key, string values, TimeSpan? expiration = null)
        {
            var value = await _database.SetAddAsync(key, values);
            _database.KeyExpire(key, expiration ?? App.Configuration["AppSettings:RedisExpirationTime"].ToTimeSpan());
            return value;
        }

        public async Task<long> SetAddAsync(string key, string[] values, TimeSpan? expiration = null)
        {
            RedisValue[] redisValues = values.Select(x => (RedisValue)x).ToArray();
            var value =  await _database.SetAddAsync(key, redisValues);
            _database.KeyExpire(key, expiration ?? App.Configuration["AppSettings:RedisExpirationTime"].ToTimeSpan());
            return value;
        }

        public async Task<long> SetRemoveAsync(string key, params string[] values)
        {
            RedisValue[] redisValues = values.Select(x => (RedisValue)x).ToArray();
            return await _database.SetRemoveAsync(key, redisValues);
        }

        public async Task<IEnumerable<string>> SetGetAllAsync(string key)
        {
            var redisValues = await _database.SetMembersAsync(key);
            return redisValues.Select(x => (string)x);
        }

        public async Task<bool> SetContainsAsync(string key, string value)
        {
            RedisValue redisValue = value;
            return await _database.SetContainsAsync(key, redisValue);
        }

        public async Task<long> SetGetCountAsync(string key)
        {
            return await _database.SetLengthAsync(key);
        }
        #endregion

        /// <summary>
        /// 判断哈希中指定字段是否存在
        /// </summary>
        public async Task<bool> HashExistsAsync(string key, string field)
        {
            return await _database.HashExistsAsync(key, field);
        }

        /// <summary>
        /// 获取哈希中指定字段的值
        /// </summary>
        public async Task<string> HashGetAsync(string key, string field)
        {
            return await _database.HashGetAsync(key, field);
        }
        /// <summary>
        /// 获取哈希中指定字段的值
        /// </summary>
        public async Task<TEntity> HashGetAsync<TEntity>(string key, string field)
        {
            var value = await _database.HashGetAsync(key, field);
            if (value.IsNullOrEmpty)
            {
                return default;
            }
            return JsonSerializer.Deserialize<TEntity>(await _database.HashGetAsync(key, field));
        }
        /// <summary>
        /// 获取哈希中所有字段和值
        /// </summary>
        public async Task<HashEntry[]> HashGetAllAsync(string key)
        {
            return await _database.HashGetAllAsync(key);
        }

        /// <summary>
        /// 对哈希中指定字段的值进行增加
        /// </summary>
        public async Task<long> HashIncrementAsync(string key, string field, long value = 1)
        {
            return await _database.HashIncrementAsync(key, field, value);
        }

        /// <summary>
        /// 设置哈希中指定字段的值
        /// </summary>
        public async Task HashSetAsync(string key, string field, string value)
        {
            await _database.HashSetAsync(key, field, value);
        }

        /// <summary>
        /// 设置哈希中多个字段和值
        /// </summary>
        public async Task HashSetAsync(string key, HashEntry[] hashFields)
        {
            await _database.HashSetAsync(key, hashFields);
        }

        /// <summary>
        /// 删除哈希中指定字段
        /// </summary>
        public async Task<bool> HashDeleteAsync(string key, string field)
        {
            return await _database.HashDeleteAsync(key, field);
        }

        /// <summary>
        /// 删除哈希中多个字段
        /// </summary>
        public async Task<long> HashDeleteAsync(string key, RedisValue[] fields)
        {
            return await _database.HashDeleteAsync(key, fields);
        }

    }
}
