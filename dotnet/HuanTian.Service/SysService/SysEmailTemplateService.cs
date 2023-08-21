#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 文件名：SysEmailTemplateInput
 * 代码生成文件
 * 创建时间：2023-08-07 17:04:33
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


using HuanTian.Service.SysService.Dto;
using Microsoft.IdentityModel.Tokens;

namespace HuanTian.Service;

/// <summary>
/// 系统邮箱模板表服务
/// </summary>
public class SysEmailTemplateService : ISysEmailTemplateService, IDynamicApiController, IScoped
{
    private readonly IRepository<SysEmailTemplateDO> _sysEmailTemplate;
    public SysEmailTemplateService(IRepository<SysEmailTemplateDO> sysEmailTemplate)
    {
        _sysEmailTemplate = sysEmailTemplate;
    }

    [HttpGet]
    public async Task<PageData> Page([FromQuery] SysEmailTemplateInput input)
    {
        var list = await _sysEmailTemplate
            .WhereIf(!string.IsNullOrEmpty(input.Name), t => t.Name.Contains(input.Name))
            .ToPageListAsync(input.PageNo, input.PageSize);
        return list;
    }
    public async Task<int> Add(SysEmailTemplateFormInput input)
    {
        // 过滤
        if ((await _sysEmailTemplate.FirstOrDefaultAsync(t => t.Name == input.Name)) != null)
        {
            throw new Exception("模板名称已存在，请修改后再试");
        }
        var entity = input.Adapt<SysEmailTemplateDO>();
        SaveHtmlToFile(input);
        var count = await _sysEmailTemplate.InitTable(entity)
            .CallEntityMethod(t => t.CreateFunc())
            .AddAsync();
        return count;
    }
    public async Task<int> Update(SysEmailTemplateFormInput input)
    {
        // 过滤
        if ((await _sysEmailTemplate.Where(t => t.Name == input.Name).ToListAsync()).Count() > 1)
        {
            throw new Exception("模板名称已存在，请修改后再试");
        }
        var entity = input.Adapt<SysEmailTemplateDO>();
        SaveHtmlToFile(input);
        var count = await _sysEmailTemplate.InitTable(entity)
            .CallEntityMethod(t => t.UpdateFunc())
            .UpdateAsync();
        return count;
    }
    public async Task<int> Delete(IdInput input)
    {
        // 删除模板文件
        var collection = await _sysEmailTemplate.Where(t => input.Ids.Contains(t.Id)).ToListAsync();
        foreach (var item in collection)
        {
            var filePath = Path.Combine(App.WebHostEnvironment.WebRootPath, "Template", "Email", item.Name + ".html");
            // 删除模板文件
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
        var count = await _sysEmailTemplate.DeleteAsync(input.Ids);
        return count;
    }
    public async Task<IEnumerable<SysEmailTemplateDO>> Get([FromQuery] SysEmailTemplateInput input)
    {
        var list = await _sysEmailTemplate
            .WhereIf(input.Id != 0, t => t.Id == input.Id)
            .WhereIf(!string.IsNullOrEmpty(input.Name), t => t.Name == input.Name)
            .ToListAsync();
        return list;
    }
    [HttpGet]
    public async Task<string> GetTemplate([FromQuery] SysEmailTemplateInput input)
    {
        var templateName = input.Name;
        if (string.IsNullOrEmpty(templateName))
        {
            var emailInfo = await _sysEmailTemplate
            .FirstOrDefaultAsync(t => t.Id == input.Id);
            templateName = emailInfo?.Name;
        }
        var filePath = Path.Combine(App.WebHostEnvironment.WebRootPath, "Template", "Email", templateName + ".html");
        try
        {
            string htmlContent = File.ReadAllText(filePath);
            return htmlContent;
        }
        catch (FileNotFoundException)
        {
            throw new Exception("File not found.");
        }
        catch (IOException)
        {
            throw new Exception("Error reading the file.");
        }
    }
    /// <summary>
    /// 保存HTML代码到文件
    /// </summary>
    /// <param name="input"></param>
    /// <exception cref="ArgumentException"></exception>
    private void SaveHtmlToFile(SysEmailTemplateFormInput input)
    {
        try
        {
            // 检查HTML代码和文件路径是否为空
            if (string.IsNullOrEmpty(input.EmailHtml))
            {
                throw new ArgumentException("HTML代码不能为空");
            }
            var filePath = Path.Combine(App.WebHostEnvironment.WebRootPath, "Template", "Email", input.Name + ".html");
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentException("文件路径不能为空");
            }

            // 确保文件夹存在
            string directoryPath = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            // 写入HTML代码到文件
            File.WriteAllText(filePath, input.EmailHtml);

            Console.WriteLine("HTML代码已成功保存为文件：" + filePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine("保存HTML代码出错：" + ex.Message);
        }
    }
}

