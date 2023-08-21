#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023 HP NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：WEIHAN
 * 公司名称：HP
 * 命名空间：HuanTian.Infrastructure.Dto
 * 唯一标识：abf2e9f3-cbba-4982-88fb-ff5bb2803dc8
 * 文件名：IdInput
 * 当前用户域：weihan
 * 
 * 创建者：wanglei
 * 创建时间：2023/5/25 15:26:05
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
using System.ComponentModel.DataAnnotations;

namespace HuanTian.Infrastructure
{
    public class IdInput
    {
        [Required(ErrorMessage ="Id必须提供")]
        public long[] Ids { get; set; }
    }
}
