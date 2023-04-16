#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023  NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：DESKTOP-4P8G8RH
 * 公司名称：
 * 命名空间：HuanTian.Entities.DtoModel
 * 唯一标识：d21e8bb6-df78-4131-aa2e-163acbf29aa3
 * 文件名：PagingEntity
 * 当前用户域：DESKTOP-4P8G8RH
 * 
 * 创建者：wanglei
 * 电子邮箱：271976304@qq.com
 * 创建时间：2023/4/16 17:02:28
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuanTian.Entities.DtoModel
{
    /// <summary>
    /// 分页返回对象
    /// </summary>
    public class PagingEntity
    {
        /// <summary>
        /// 数据值
        /// </summary>
        public object Data { get; set; }
        /// <summary>
        /// 每页条数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageNo { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPage { get; set; }

        /// <summary>
        /// 总条数
        /// </summary>
        public int TotalCount { get; set; }

    }
}
