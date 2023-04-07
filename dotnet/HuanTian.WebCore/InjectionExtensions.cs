#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023 Microsoft NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：MS-SOBOTFLKMEBA
 * 公司名称：Microsoft
 * 命名空间：HuanTian.WebCore
 * 唯一标识：eabc4168-21d1-4e65-961e-cd25f4b9e0cf
 * 文件名：InjectionExtensions
 * 当前用户域：MS-SOBOTFLKMEBA
 * 
 * 创建者：wanglei
 * 电子邮箱：
 * 创建时间：2023/4/2 12:53:30
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
using HuanTian.SqlSugar;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HuanTian.WebCore
{
	public static class InjectionExtensions
	{
		/// <summary>
		/// 依赖注入集合
		/// </summary>
		/// <param name="services"></param>
		/// <param name="configure"></param>
		/// <returns>IServiceCollection</returns>
		public static IServiceCollection AddInject(this IServiceCollection services, IConfiguration configuration)
		{
			InternalApp.Configuration = configuration;
			InternalApp.InternalServices = services;


			return services;
		}
	}
}
