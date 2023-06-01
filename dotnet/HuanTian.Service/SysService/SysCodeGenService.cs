#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023  NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：DESKTOP-6BOHDBE
 * 公司名称：
 * 命名空间：HuanTian.Service.CodeGen
 * 唯一标识：78bed421-c8b9-4922-90d2-19b0789f750a
 * 文件名：SysCodeGenService
 * 当前用户域：DESKTOP-6BOHDBE
 * 
 * 创建者：wanglei
 * 电子邮箱：271976304@qq.com
 * 创建时间：2023/5/25 21:54:07
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

using RazorEngine;
using RazorEngine.Templating;
using SqlSugar;

namespace HuanTian.Service
{
    /// <summary>
    /// 自动代码生成服务
    /// </summary>
    public class SysCodeGenService : IDynamicApiController
    {
        /// <summary>
        /// 原生sqlSugar对象    
        /// </summary>
        private readonly ISqlSugarClient _db;

        public SysCodeGenService(ISqlSugarClient db)
        {
            _db = db;
        }    
        /// <summary>
        /// 代码生成_本地项目
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task RunLocal(SysCodeGenInput input)
        {
            // 获取模板信息
            var templatePathList = GetTemplatePathList(input);
            if (Directory.Exists(templatePathList[0].FilePath.GetParentPath()) && input.Enforcement == false)
            {
                throw new Exception("执行失败,该代码已经存在了,如需强制执行请将Enforcement设为true!");
            }
            // 获取表字段信息
            var columnInfo = await _db.Queryable<SysCodeGenDO>()
                .Where(x => x.TableName == input.TableName).ToListAsync();
            if (columnInfo == null)
            {
                throw new Exception("表格列属性暂未录入,请修改后再试");
            }
            // 获取表格信息
            var tableInfo = _db.DbMaintenance.GetTableInfoList(false).FirstOrDefault(t => t.Name == input.TableName);
            if (tableInfo == null)
            {
                throw new Exception("表格数据库中不存在,请修改后再试");
            }
            // 更换命名方式
            columnInfo.ForEach(item => { item.DbColumnName = item.DbColumnName.ToPascalCase(); });

            var info = new SysCodeGenTemplateInput()
            {
                ClassName = input.TableName.ToPascalCase(),
                ClassNameLow = input.TableName.ToCamelCase(),
                EntityName = input.TableName.ToPascalCase() + "DO",
                EntityDescribe = tableInfo?.Description ?? "",
                NameSpace = input.ApplicationName,
                TableField = columnInfo
            };
            
            foreach (var item in templatePathList)
            {
                var templateString = File.ReadAllText(item.TemplatePath);
                var templateResult = Engine.Razor.RunCompile(templateString, Guid.NewGuid().ToString(), typeof(SysCodeGenTemplateInput), info);
                
                if (!Directory.Exists(item.FilePath.GetParentPath()))
                {
                    Directory.CreateDirectory(item.FilePath.GetParentPath());
                }
                File.WriteAllText(item.FilePath, templateResult, System.Text.Encoding.UTF8);
            }

        }

        /// <summary>
        /// 获取模板文件路径集合
        /// </summary>
        /// <returns></returns>
        [NonAction]
        private List<SysCodeGenTemplateInfo> GetTemplatePathList(SysCodeGenInput input)
        {
            // 文件生成路径
            var tableName = input.TableName.ToPascalCase();
            var backendPath = Path.Combine(new DirectoryInfo(App.WebHostEnvironment.ContentRootPath).Parent.FullName, input.ApplicationName,tableName);
            // 模板文件路径
            var templatePath = Path.Combine(App.WebHostEnvironment.WebRootPath, "Template", "CodeGen");
            // 模板文件集合
            var templatePathList = new List<SysCodeGenTemplateInfo>
            {
                new SysCodeGenTemplateInfo(Path.Combine(templatePath, "Service.cs.vm"),Path.Combine(backendPath, tableName + "Service.cs")),
                new SysCodeGenTemplateInfo(Path.Combine(templatePath, "IService.cs.vm"),Path.Combine(backendPath, "I" + tableName + "Service.cs")),
                new SysCodeGenTemplateInfo(Path.Combine(templatePath, "Input.cs.vm"),Path.Combine(backendPath, "Dto", tableName + "Input.cs")),
                new SysCodeGenTemplateInfo(Path.Combine(templatePath, "Output.cs.vm"), Path.Combine(backendPath, "Dto", tableName + "Output.cs")),
            };
            return templatePathList;
        }

    }
}