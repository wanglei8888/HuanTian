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
    public async Task<IEnumerable<SysDeptTreeOutput>> Tree([FromQuery] SysDeptInput input)
    {
        var list  = await _sysDept
            .WhereIf(input.ParentId != 0, t => t.ParentId == input.ParentId)
            .WhereIf(!string.IsNullOrEmpty(input.Name), t => t.Name.Contains(input.Name))
            .WhereIf(!string.IsNullOrEmpty(input.Describe), t => t.Describe == input.Describe)
            .WhereIf(!string.IsNullOrEmpty(input.Enable), t => t.Enable == Convert.ToBoolean(input.Enable))
            .ToListAsync();
        var tree = TreeHelper.DoTreeBuild(list.Adapt<List<SysDeptTreeOutput>>());
        return tree;
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
        var count = await _sysDept.DeleteAsync(input.Ids);
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

