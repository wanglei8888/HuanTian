#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023 HP NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：WEIHAN
 * 公司名称：HP
 * 命名空间：HuanTian.Service.SysService.Dto
 * 唯一标识：1279abe7-6c68-4e09-b4a5-e26762fb12ae
 * 文件名：SysDictionaryInput
 * 当前用户域：weihan
 * 
 * 创建者：wanglei
 * 创建时间：2023/5/24 14:50:14
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
    public class SysDictionaryInput
    {
        public string Code { get; set; }
    }
    public class SysDictionaryPageInput : PageInput
    {
        public string Code { get; set; }
    }
}
