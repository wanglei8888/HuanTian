#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023  NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：DESKTOP-6BOHDBE
 * 公司名称：
 * 命名空间：HuanTian.Infrastructure
 * 唯一标识：fc22d1d8-dd65-47bb-97c2-80076a608899
 * 文件名：LanmodaHelper
 * 当前用户域：DESKTOP-6BOHDBE
 * 
 * 创建者：wanglei
 * 电子邮箱：271976304@qq.com
 * 创建时间：2023/5/20 13:59:18
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


using System.Linq.Expressions;

namespace HuanTian.Infrastructure
{
    /// <summary>
    /// 泛型帮助类
    /// </summary>
    public class TEntityHelper<TEntity>
    {
        private static readonly Dictionary<string, Func<TEntity, object>> _propertyAccessorsCache = new Dictionary<string, Func<TEntity, object>>();
        private static Dictionary<string, object> anonymousObject = new Dictionary<string, object>();
        /// <summary>
        /// 获取泛型的列值
        /// </summary>
        /// <param name="info"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static string GetEnutityColumn(TEntity info, string columnName)
        {
            try
            {
                if (!_propertyAccessorsCache.TryGetValue(columnName, out var func))
                {
                    var parameter = Expression.Parameter(typeof(TEntity), "info");
                    var propertyAccess = Expression.PropertyOrField(parameter, columnName);
                    var lambda = Expression.Lambda<Func<TEntity, object>>(Expression.Convert(propertyAccess, typeof(object)), parameter);
                    func = lambda.Compile();
                    _propertyAccessorsCache[columnName] = func;
                }
                var propValue = func(info);
                return propValue?.ToString() ?? "";
            }
            catch (Exception ex)
            {
                throw new FriendlyException($"{nameof(GetEnutityColumn)}方法,实体{typeof(TEntity).Name}中该列不存在{columnName}");
            }

        }
        /// <summary>
        /// 以左边为主表展示两个实体所有的属性
        /// </summary>
        /// <typeparam name="TEntityB"></typeparam>
        /// <param name="entityA"></param>
        /// <param name="entityB"></param>
        /// <returns></returns>
        public static object ConvertToAnonymousObject<TEntityB>(TEntity entityA, TEntityB entityB)
        {
            var aProperties = typeof(TEntity).GetProperties();
            var bProperties = typeof(TEntityB).GetProperties();
            Dictionary<string, object> anonymousObject = new Dictionary<string, object>();
            foreach (var property in aProperties)
            {
                var propertyName = property.Name;
                anonymousObject[propertyName] = property.GetValue(entityA);
            }

            foreach (var property in bProperties)
            {
                var propertyName = property.Name;
                if (anonymousObject.ContainsKey(propertyName))
                {
                    var entityName = typeof(TEntityB).Name.EntityRemovePrefixAndSuffix();
                    propertyName = entityName + propertyName;
                }
                anonymousObject[propertyName] = property.GetValue(entityB);
            }

            return anonymousObject;
        }
    }
}