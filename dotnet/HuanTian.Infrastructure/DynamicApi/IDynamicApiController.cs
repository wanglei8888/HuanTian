#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023 HP NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：WEIHAN
 * 公司名称：HP
 * 命名空间：HuanTian.Infrastructure.DynamicApi
 * 唯一标识：a30100ba-c882-4012-801a-10e590671f8e
 * 文件名：IDynamicApiController
 * 当前用户域：weihan
 * 
 * 创建者：wanglei
 * 创建时间：2023/4/6 15:08:02
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

namespace HuanTian.Infrastructure
{
    /// <summary>
    /// 动态Api控制器依赖接口
    /// </summary>
    public interface IDynamicApiController { }
    /// <summary>
    /// 瞬时依赖注入
    /// </summary>
    public interface ITransient { }
    /// <summary>
    /// 作用域依赖注入
    /// </summary>
    public interface IScoped { }
    /// <summary>
    /// 单例依赖注入
    /// </summary>
    public interface ISingleton { }
}
