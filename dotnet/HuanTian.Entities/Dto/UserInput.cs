#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023 HP NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：WEIHAN
 * 公司名称：HP
 * 命名空间：HuanTian.Service.SysService.Dto
 * 唯一标识：c4465ca7-cded-4134-b663-eb824d5be6b4
 * 文件名：UserInput
 * 当前用户域：weihan
 * 
 * 创建者：wanglei
 * 创建时间：2023/4/28 17:31:20
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
    public class UserInput : PageInput
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Enable { get; set; }
        public int PageSize { get; set; }
        public int PageNo { get; set; }
    }
}
