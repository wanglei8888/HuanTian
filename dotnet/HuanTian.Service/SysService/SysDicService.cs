#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023 HP NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：WEIHAN
 * 公司名称：HP
 * 命名空间：HuanTian.Service.SysService
 * 唯一标识：b96e7e23-a5e3-4848-8905-7de0be1f9f70
 * 文件名：SysDicService
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


using HuanTian.Infrastructure;
using System.Linq;

namespace HuanTian.Service
{
    /// <summary>
    /// 系统字典服务
    /// </summary>
    public class SysDicService : IDynamicApiController
    {
        private readonly IRepository<SysDicDetailDO> _sysDicDetail;
        private readonly IRepository<SysDicDO> _sysDic;
        public SysDicService(IRepository<SysDicDetailDO> sysDicDetail, IRepository<SysDicDO> sysDic)
        {
            _sysDicDetail = sysDicDetail;
            _sysDic = sysDic;
        }
        public async Task<IEnumerable<SysDicDetailDO>> Get([FromQuery] SysDicInput input)
        {
            long masterId = 0;
            if (!string.IsNullOrEmpty(input.Code))
            {
                var dicMaster = await _sysDic.FirstOrDefaultAsync(t => t.Code == input.Code);
                masterId = dicMaster.Id;
            }
            var list = await _sysDicDetail
                .WhereIf(!string.IsNullOrEmpty(input.Name), t => t.Name == input.Name)
                .WhereIf(!string.IsNullOrEmpty(input.Code),t => t.MasterId == masterId)
                .OrderBy(t => t.Order)
                .ToListAsync();
            return list;
        }
        /// <summary>
        /// 获取主表数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<SysDicDO>> GetMasterList([FromQuery] SysDicInput input)
        {
            var list = await _sysDic
                .WhereIf(!string.IsNullOrEmpty(input.Name), t => t.Name == input.Name)
                .WhereIf(!string.IsNullOrEmpty(input.Code), t => t.Code == input.Code)
                .ToListAsync();
            return list;
        }
        [HttpGet]
        public async Task<PageData> Page([FromQuery] SysDicPageInput input)
        {
            var list = await _sysDic
                .WhereIf(!string.IsNullOrEmpty(input.Name), t => t.Name == input.Name)
                .WhereIf(!string.IsNullOrEmpty(input.Code), t => t.Code == input.Code)
                .ToPageListAsync(input.PageNo, input.PageSize);
            return list;
        }
        public async Task<int> Add(SysDicFormInput input)
        {
            var entity = input.Adapt<SysDicDO>();
            var info = await _sysDic.FirstOrDefaultAsync(t => t.Code == input.Code);
            if (info != null)
            {
                throw new Exception("字典编码已存在");
            }
            var count = await _sysDic.InitTable(entity)
                .CallEntityMethod(t => t.CreateFunc())
                .AddAsync();
            return count;
        }
        public async Task<int> Update(SysDicFormInput input)
        {
            var entity = input.Adapt<SysDicDO>();
            var count = await _sysDic.InitTable(entity)
                .UpdateAsync();
            return count;
        }
        public async Task<int> Delete(IdInput input)
        {
            var count = await _sysDic.DeleteAsync(input.Id.Split(',').Adapt<long[]>());
            // 删除从表数据
            count += await _sysDicDetail.DeleteAsync(t => t.MasterId == long.Parse(input.Id));

            return count;
        }
        /// <summary>
        /// 字典详情新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPost("Detail")]
        public async Task<int> DetailAdd(SysDicDetaiLFormInput input)
        {
            var count = 0;
            // 数据过滤
            bool valueDuplicate = input.SysDicDetail.GroupBy(m => m.Value).Count() != input.SysDicDetail.Count();
            bool nameDuplicate = input.SysDicDetail.GroupBy(m => m.Name).Count() != input.SysDicDetail.Count();

            if (valueDuplicate)
            {
                throw new Exception("存在相同字典值,请修改后再试");
            }
            if (nameDuplicate)
            {
                throw new Exception("存在相同字典名称,请修改后再试");
            }
            // 删除数据
            await _sysDicDetail.DeleteAsync(t=> t.MasterId == input.MasterId);
            // 添加数据
            if (input.SysDicDetail.Any())
            {
                count += await _sysDicDetail.InitTable(input.SysDicDetail)
                .CallEntityMethod(t => t.CreateFunc())
                .CallEntityMethod(t => t.SetPropertyValue<SysDicDetailDO, long>(q => q.MasterId, input.MasterId))
                .AddAsync();
            }
            return count;
        }
    }
}
