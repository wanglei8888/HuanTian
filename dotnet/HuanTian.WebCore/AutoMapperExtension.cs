#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023 Microsoft NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：MS-SOBOTFLKMEBA
 * 公司名称：Microsoft
 * 命名空间：HuanTian.WebCore
 * 唯一标识：eabc4168-21d1-4e65-961e-cd25f4b9e0cf
 * 文件名：AutoMapperExtension
 * 当前用户域：MS-SOBOTFLKMEBA
 * 
 * 创建者：wanglei
 * 电子邮箱：
 * 创建时间：2023/2/25 17:51:30
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
using AutoMapper;
using AutoMapper.Internal;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace HuanTian.WebCore
{
    public static class AutoMapperExtension
    {
        /// <summary>
        /// AutoMapper拓展
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddAutoMapperService(this IServiceCollection services)
        {
            //这里会通过反射自动注入的，先临时这样

            var profileList = GetClassByBaseClassesAndInterfaces("HuanTian.DtoModel", typeof(Profile));
            //profileList.Add(typeof(LoginProfile));
            services.AddAutoMapper(profileList.ToArray());

            #region 依赖注入-第二种方式注入在Stantup
            //services.AddAutoMapper(Assembly.Load("HuanTian.DtoModel")
            //    .DefinedTypes.Where(t => typeof(Profile).GetTypeInfo().IsAssignableFrom(t.AsType()))
            //    .Select(t => t.AsType()).ToArray());
            #endregion 
            return services;
        }

        public static List<Type> GetClassByBaseClassesAndInterfaces(string assemblyFile, Type type)
        {
            Assembly assembly = Assembly.Load(assemblyFile);

            List<Type> resList = new List<Type>();

            List<Type> typeList = assembly.GetTypes().Where(m => m.IsClass).ToList();
            foreach (var t in typeList)
            {
                var data = t.BaseClassesAndInterfaces();
                if (data.Contains(type))
                {
                    resList.Add(t);
                }

            }
            return resList;
        }
    }
}
