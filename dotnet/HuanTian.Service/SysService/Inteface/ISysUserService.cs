#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023  NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：DESKTOP-4P8G8RH
 * 公司名称：
 * 命名空间：HuanTian.Service
 * 唯一标识：e02184b8-f57f-4b4f-a44e-e8d37fca16fb
 * 文件名：ISysUserService
 * 当前用户域：DESKTOP-4P8G8RH
 * 
 * 创建者：wanglei
 * 电子邮箱：271976304@qq.com
 * 创建时间：2023/4/5 21:03:18
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

namespace HuanTian.Service
{
    public interface ISysUserService
    {
        /// <summary>
        /// 获取当前登录用户的用户信息
        /// </summary>
        /// <returns></returns>
        Task<dynamic> Info();
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PageData> Page(SysUserInput input);
        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<int> Add(SysUserDO input);
        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<int> Update(SysUserDO input);
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<int> Delete(IdInput input);
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<IEnumerable<SysUserDO>> Get(SysUserInput input);
    }
}
