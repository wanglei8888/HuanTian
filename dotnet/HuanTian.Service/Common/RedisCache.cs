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
using HuanTian.Infrastructure;
using StackExchange.Redis;
using System.Text.Json;

namespace HuanTian.Service
{
    public class RedisCache : IRedisCache
    {
        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase _database;

        public RedisCache(string connectionString)
        {
            _redis = ConnectionMultiplexer.Connect(connectionString);
            _database = _redis.GetDatabase();
        }
        public async Task<T> GetAsync<T>(string key)
        {
            var value = await _database.StringGetAsync(key);
            if (value.HasValue)
            {
                return JsonSerializer.Deserialize<T>(value);
            }
            return default;
        }
        
        public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null)
        {
            var options = new JsonSerializerOptions { IgnoreNullValues = true };
            var serializedValue = JsonSerializer.Serialize(value, options);
            // 默认指定储存10分钟
            await _database.StringSetAsync(key, serializedValue, expiration ?? TimeSpan.FromMinutes(10));
        }

        public async Task<bool> DeleteAsync(string key)
        {
            return await _database.KeyDeleteAsync(key);
        }

        public async Task<bool> ListGetAsync<T>(string key, T value)
        {
            var serializedValue = await _database.ListGetByIndexAsync("", 0);
            var redisValue = await _database.StringGetAsync(key);
            if (redisValue.HasValue)
            {
                return true;
            }
            return false;
        }
    }
}
