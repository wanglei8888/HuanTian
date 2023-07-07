#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023 HP NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：WEIHAN
 * 公司名称：HP
 * 命名空间：HuanTian.Entities.DtoModel
 * 唯一标识：ef53cd7c-a9c6-4175-9e06-e15a8cda6fcd
 * 文件名：PageInput
 * 当前用户域：weihan
 * 
 * 创建者：wanglei
 * 创建时间：2023/4/28 17:42:49
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
    /// 分页入参
    /// </summary>
    public interface PageInput
    {
        /// <summary>
        /// 每页条数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageNo { get; set; }
    }
}
