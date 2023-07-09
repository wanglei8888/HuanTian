#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 文件名：SysDeptInput
 * 代码生成文件
 * 创建时间：2023-07-07 16:56:56
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

using NPOI.Util;

namespace HuanTian.Service;

/// <summary>
/// 系统部门表服务
/// </summary>
public class SysDeptService : ISysDeptService, IDynamicApiController, IScoped
{
    private readonly IRepository<SysDeptDO> _sysDept;
    public SysDeptService(IRepository<SysDeptDO> sysDept)
    {
        _sysDept = sysDept;
    }

    [HttpGet]
    public async Task<PageData> Page([FromQuery] SysDeptInput input)
    {
        var list  = await _sysDept
            .WhereIf(input.ParentId != 0, t => t.ParentId == input.ParentId)
            .WhereIf(!string.IsNullOrEmpty(input.Name), t => t.Name == input.Name)
            .WhereIf(!string.IsNullOrEmpty(input.Describe), t => t.Describe == input.Describe)
            .WhereIf(!string.IsNullOrEmpty(input.Enable), t => t.Enable == Convert.ToBoolean(input.Enable))
            .ToPageListAsync(input.PageNo,input.PageSize);
       
        // 查询所属的父级ID
        var data = list.Data.Adapt<List<SysDeptOutput>>();
        var parentIds = data.Select(t => t.ParentId).Distinct().ToArray();

        var parentList = await _sysDept.Where(t => parentIds.Contains(t.Id))
            .ToListAsync();
        var result = from child in data
                     join parent in parentList on child.ParentId equals parent.Id into parentGroup
                     from parent in parentGroup.DefaultIfEmpty()
                     select new SysDeptOutput
                     {
                         Id = child.Id,
                         Name = child.Name,
                         Describe = child.Describe,
                         Enable = child.Enable,
                         ParentId = child.ParentId,
                         ParentName = parent?.Name
                     };
        data.ForEach(t =>
        {
            t.ParentName = parentList.ToList().FirstOrDefault(p => p.Id == t.ParentId)?.Name;
        });

        list.Data = data;
        return list;
    }
    public async Task<int> Add(SysDeptFormInput input)
    {
        var entity = input.Adapt<SysDeptDO>();
        var count = await _sysDept.InitTable(entity)
            .CallEntityMethod(t => t.CreateFunc())
            .AddAsync();
        return count;
    }
    public async Task<int> Update(SysDeptFormInput input)
    {
        var entity = input.Adapt<SysDeptDO>();
        var count = await _sysDept.InitTable(entity)
            .UpdateAsync();
        return count;
    }
    public async Task<int> Delete(IdInput input)
    {
        var count = await _sysDept.DeleteAsync(input.Id.Split(',').Adapt<long[]>());
        return count;
    }
    public async Task<IEnumerable<SysDeptDO>> Get([FromQuery] SysDeptInput input)
    {
        var list  = await _sysDept
            .WhereIf(input.Id != 0, t => t.Id == input.Id)
            .WhereIf(input.ParentId != 0, t => t.ParentId == input.ParentId)
            .WhereIf(!string.IsNullOrEmpty(input.Name), t => t.Name == input.Name)
            .WhereIf(!string.IsNullOrEmpty(input.Describe), t => t.Describe == input.Describe)
            .WhereIf(!string.IsNullOrEmpty(input.Enable), t => t.Enable == Convert.ToBoolean(input.Enable))
            .ToListAsync();
        return list;
    }
}

