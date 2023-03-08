using AutoMapper;
using AutoMapper.Internal;
using HuanTian.DtoModel;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HuanTian.WebCore
{
    public static class AutoMapperExtension
    {
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
