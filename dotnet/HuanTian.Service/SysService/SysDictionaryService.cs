#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023 HP NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：WEIHAN
 * 公司名称：HP
 * 命名空间：HuanTian.Service.SysService
 * 唯一标识：b96e7e23-a5e3-4848-8905-7de0be1f9f70
 * 文件名：SysDictionaryService
 * 当前用户域：weihan
 * 
 * 创建者：wanglei
 * 创建时间：2023/5/24 14:46:50
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

namespace HuanTian.Service.SysService
{
    /// <summary>
    /// 系统字典服务
    /// </summary>
    public class SysDictionaryService : IDynamicApiController
    {
        private readonly IRepository<SysDictionaryDO> _sysDictionary;
        public SysDictionaryService(IRepository<SysDictionaryDO> sysDictionary)
        {
            _sysDictionary = sysDictionary;
        }
        public async Task<IEnumerable<SysDictionaryDO>> Get([FromQuery]SysDictionaryInput input)
        { 
            var list =  await  _sysDictionary
                .WhereIf(!string.IsNullOrEmpty(input.Code),t=>t.Code == input.Code).ToListAsync();
            return list;
        }
        [HttpGet]
        public async Task<PageData> Page([FromQuery] SysDictionaryPageInput input)
        {
            var list = await _sysDictionary
                .WhereIf(!string.IsNullOrEmpty(input.Code), t => t.Code == input.Code)
                .ToPageListAsync(input.PageNo,input.PageSize);
            return list;
        }
    }
}
