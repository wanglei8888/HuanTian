#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023  NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：DESKTOP-6BOHDBE
 * 公司名称：
 * 命名空间：HuanTian.Infrastructure.Filter.ErroFilter
 * 唯一标识：127bb166-2e26-41f8-9de2-12e28bb2f20e
 * 文件名：FriendlyException
 * 当前用户域：DESKTOP-6BOHDBE
 * 
 * 创建者：wanglei
 * 电子邮箱：271976304@qq.com
 * 创建时间：2023/5/21 11:43:24
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
    /// 友好报错机制
    /// </summary>
    public class FriendlyException : Exception
    {
        /// <summary>
        /// 友好报错机制
        /// </summary>
        /// <param name="message"></param>
        public FriendlyException(string message) : base(message)
        {

        }
    }
}