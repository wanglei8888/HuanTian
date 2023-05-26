#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023  NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：DESKTOP-6BOHDBE
 * 公司名称：
 * 命名空间：HuanTian.Entities.Enum
 * 唯一标识：2c9b3daf-23f3-414e-adae-0126d438b2b4
 * 文件名：TimeUnit
 * 当前用户域：DESKTOP-6BOHDBE
 * 
 * 创建者：wanglei
 * 电子邮箱：271976304@qq.com
 * 创建时间：2023/5/26 23:58:41
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


namespace HuanTian.Entities
{
    /// <summary>
    /// 时间单位枚举
    /// </summary>
    public enum TimeUnit
    {
        Minute = 1,
        Hour = 60,
        Day = 1440,
        Week = 10080,
        Month = 43200,
        Year = 525600
    }
}