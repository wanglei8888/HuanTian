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

using EnumsNET;
using NPOI.HPSF;
using NPOI.SS.Formula.Functions;
using NPOI.Util;
using RazorEngine;
using RazorEngine.Templating;
using SqlSugar;
using System.Configuration;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Text.Encodings.Web;
using Yitter.IdGenerator;

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

        private readonly IRepository<SysCodeGenDO> _codeGen;
        private readonly IRepository<SysCodeGenDetailDO> _codeGenDetail;
        private readonly ISysMenuService _menuService;

        public SysCodeGenService(ISqlSugarClient db, IRepository<SysCodeGenDO> codeGen, IRepository<SysCodeGenDetailDO> codeGenDetail, ISysMenuService menuService)
        {
            _db = db;
            _codeGen = codeGen;
            _codeGenDetail = codeGenDetail;
            _menuService = menuService;
        }
        public async Task<IEnumerable<SysCodeGenDetailDO>> Get([FromQuery] SysCodeGenGetInput input)
        {
            // 查询数据
            var list = await _codeGenDetail
                .Where(t => t.MasterId == input.MasterId)
                .ToListAsync();
            return list;
        }
        [HttpGet]
        public async Task<PageData> Page([FromQuery] SysCodeGenTableInput input)
        {
            var list = await _codeGen
                .WhereIf(!string.IsNullOrEmpty(input.TableName), x => x.TableName == input.TableName)
                .WhereIf(!string.IsNullOrEmpty(input.Name), x => x.Name == input.Name)
                .ToPageListAsync(input.PageNo, input.PageSize);
            return list;
        }
        public async Task<int> Add(SysCodeGenFormInput input)
        {
            var entity = input.Adapt<SysCodeGenDO>();
            // 数据过滤
            var firtst = await _codeGen.FirstOrDefaultAsync(x => x.TableName == entity.TableName);
            if (firtst != null)
            {
                throw new Exception("该表格名字已经存在,请修改后再试");
            }
            var id = YitIdHelper.NextId();
            // 详细表数据
            var ignoreColumn = new string[] { "id", "create_by", "create_on", "update_by", "update_on", "deleted" }; //忽略基础数据
            var columnList = _db.DbMaintenance.GetColumnInfosByTableName($"{input.TableName}").Where(t => !ignoreColumn.Contains(t.DbColumnName));
            var detailList = new List<SysCodeGenDetailDO>();
            foreach (var (item, index) in columnList.Select((value, index) => (value, index)))
            {
                var model = new SysCodeGenDetailDO();
                model.DbColumnName = item.DbColumnName;
                model.DataType = item.DataType;
                model.ColumnDescription = item.ColumnDescription;
                switch (item.DataType)
                {
                    case "datetime":
                        model.FrontendType = "datetime";
                        break;
                    case "tinyint":
                        model.FrontendType = "radio";
                        break;
                    default:
                        model.FrontendType = "textBox";
                        break;
                }
                model.Order = index + 1;
                model.MasterId = id;
                detailList.Add(model);
            }
            // 保存数据
            var count = await _codeGen.InitTable(entity)
                .CallEntityMethod(t => t.CreateFunc())
                .CallEntityMethod(t => t.SetPropertyValue<SysCodeGenDO, long>(t => t.Id, id))
                .AddAsync();
            count += await _codeGenDetail.InitTable(detailList)
               .CallEntityMethod(t => t.CreateFunc())
               .AddAsync();
            return count;
        }
        public async Task<int> Update(SysCodeGenFormInput input)
        {
            var entity = input.Adapt<SysCodeGenDO>();
            // 数据过滤
            var firtst = await _codeGen.FirstOrDefaultAsync(x => x.TableName == entity.TableName && x.Id != input.Id);
            if (firtst != null)
            {
                throw new Exception("该表格名字已经存在,请修改后再试");
            }
            var count = await _codeGen.InitTable(entity)
                .UpdateAsync();
            return count;
        }
        public async Task<int> Delete(IdInput input)
        {
            var count = await _codeGen.DeleteAsync(long.Parse(input.Id));
            // 删除从表
            count += await _codeGenDetail.DeleteAsync(x => x.MasterId == long.Parse(input.Id));

            return count;
        }
        [HttpPut("Detail")]
        public async Task<int> DetailUpdate(SysCodeGenDetailFormInput input)
        {
            var checkGroup = input.Detail.GroupBy(x => x.DbColumnName).Count() != input.Detail.Count();
            if (checkGroup)
            {
                throw new Exception("表格列属性重复,请修改后再试");
            }
            var count = await _codeGenDetail.InitTable(input.Detail)
                .UpdateAsync();
            return count;
        }
        /// <summary>
        /// 获取所有表格信息
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DbTableInfo> GetTableList()
        {
            var ignoreTable = new string[] { "__EFMigrationsHistory", "sys_user", "sys_menu", "sys_dic" };
            var tableInfo = _db.DbMaintenance.GetTableInfoList()
                .Where(t => !ignoreTable.Contains(t.Name));
            return tableInfo;
        }
        /// <summary>
        /// 代码生成_本地项目
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> RunLocal(SysCodeGenInput input)
        {
            #region 过滤条件+获取数据
            // 获取表字段信息
            var masterInfo = await _codeGen.FirstOrDefaultAsync(t => t.Id == input.Id);
            var columnInfo = (await _codeGenDetail
                .Where(t => t.MasterId == masterInfo.Id)
                .ToListAsync()).ToList();
            var parentMenu = (await _menuService.Get(new SysMenuTypeInput() { Id = masterInfo.MenuId })).ToList()[0];
            if (columnInfo == null)
            {
                throw new Exception("表格列属性暂未录入,请修改后再试");
            }
            // 更换命名方式
            columnInfo.ForEach(item => { item.DbColumnName = item.DbColumnName.ToPascalCase(); });

            var applicationName = "HuanTian.Service";
            var tableName = masterInfo.TableName;

            // 获取表格信息
            var tableInfo = _db.DbMaintenance.GetTableInfoList().FirstOrDefault(t => t.Name == tableName);
            if (tableInfo == null)
            {
                throw new Exception("表格在数据库中不存在,请修改后再试");
            }
            // 获取模板信息
            var templatePathList = GetTemplatePathList(new SysCodeGenFileInput { ApplicationName = applicationName, TableName = masterInfo.TableName, FrontPath = parentMenu.Path });
            // 生成方式为生成到项目 && 不强制执行
            if (masterInfo.GenerationWay == GenerationWayEnum.GenToProj
                && input.Enforcement == false)
            {
                if (Directory.Exists(templatePathList[0].FilePath.GetParentPath()))
                {
                    throw new Exception("执行失败,该后端代码文件已经存在");
                }
                if (Directory.Exists(templatePathList[4].FilePath.GetParentPath()))
                {
                    throw new Exception("执行失败,该前端代码文件已经存在");
                }
            }
            #endregion

            #region 生成数据信息
            // 判断是否要生成菜单信息
            if (masterInfo.MenuId != 0)
            {
                var menuDo = new SysMenuDO();
                menuDo.ParentId = masterInfo.MenuId;
                menuDo.Name = masterInfo.Name;
                menuDo.Path = parentMenu.Path + $"/{masterInfo.TableName.ToCamelCase()}";
                menuDo.Title = parentMenu.Title + $".{masterInfo.TableName.ToCamelCase()}";
                menuDo.Icon = "none";
                //menuDo.KeepAlive = parentMenu.KeepAlive;
                menuDo.Show = true;
                menuDo.HideChildren = false;
                menuDo.Component = menuDo.Path.Substring(1) + $"/{masterInfo.TableName.ToCamelCase()}List";
                menuDo.MenuType = parentMenu.MenuType;
                menuDo.Order = 100;
                await _menuService.Add(menuDo);
            }
            var tableFieldCamelCase = columnInfo.Copy();
            tableFieldCamelCase.ForEach(t => { t.DbColumnName = t.DbColumnName.ToCamelCase(); });
            // 生成模板实体值
            var info = new SysCodeGenTemplateInput()
            {
                ClassName = tableName.ToPascalCase(),
                ClassNameLow = tableName.ToCamelCase(),
                EntityName = tableName.ToPascalCase() + "DO",
                EntityDescribe = tableInfo?.Description ?? "",
                NameSpace = applicationName,
                TableField = columnInfo,
                QueryLineCount = columnInfo.Where(t => t.QueryParameters).Count(),
                IsContailUpdate = false,
                TableFieldCount = columnInfo.Count(),
                TableFieldCamelCase = tableFieldCamelCase
            };

            #endregion

            #region 生成文件

            var fileList = new List<FileInfoDto>();
            foreach (var item in templatePathList)
            {
                var templateString = File.ReadAllText(item.TemplatePath);
                var templateResult = Engine.Razor.RunCompile(templateString, item.TemplatePath, typeof(SysCodeGenTemplateInput), info);

                // 生成到项目
                if (masterInfo.GenerationWay == GenerationWayEnum.GenToProj)
                {
                    if (!Directory.Exists(item.FilePath.GetParentPath()))
                    {
                        Directory.CreateDirectory(item.FilePath.GetParentPath());
                    }
                    File.WriteAllText(item.FilePath, templateResult, System.Text.Encoding.UTF8);
                }
                // 生成为文件返回
                else if (masterInfo.GenerationWay == GenerationWayEnum.GenToPack)
                {
                    // 文件名
                    var fileName = Path.GetFileNameWithoutExtension(item.FilePath);
                    // 文件后缀
                    var extension = Path.GetExtension(item.FilePath);
                    fileList.Add(new FileInfoDto(templateResult, fileName + extension));
                }
            }
            // 打包成zip压缩包文件返回
            if (masterInfo.GenerationWay == GenerationWayEnum.GenToPack && fileList.Any())
            {
                var bytes = StringsToZipBytes(fileList);
                string encodedFileName = UrlEncoder.Default.Encode($"{masterInfo.TableName}代码生成.zip");
                return new FileContentResult(bytes, "application/zip") { FileDownloadName = encodedFileName };
            }
            return RequestHelper.RequestInfo("success", HttpStatusCode.OK);
            #endregion
        }
        /// <summary>
        /// 获取模板文件路径集合
        /// </summary>
        /// <returns></returns>
        private List<SysCodeGenTemplateInfo> GetTemplatePathList(SysCodeGenFileInput input)
        {
            // 文件生成路径
            var tableName = input.TableName.ToPascalCase();
            var backendPath = Path.Combine(new DirectoryInfo(App.WebHostEnvironment.ContentRootPath).Parent.FullName, input.ApplicationName, tableName);
            var frontendPath = Path.Combine(new DirectoryInfo(App.WebHostEnvironment.ContentRootPath).Parent.Parent.FullName, "frontend", "src", "views", tableName.ToCamelCase());
            // 模板文件路径
            var templatePath = Path.Combine(App.WebHostEnvironment.WebRootPath, "Template", "CodeGen");
            // 模板文件集合
            var templatePathList = new List<SysCodeGenTemplateInfo>
            {
                new SysCodeGenTemplateInfo(Path.Combine(templatePath, "Service.cs.vm"),Path.Combine(backendPath, tableName + "Service.cs")),
                new SysCodeGenTemplateInfo(Path.Combine(templatePath, "IService.cs.vm"),Path.Combine(backendPath, "I" + tableName + "Service.cs")),
                new SysCodeGenTemplateInfo(Path.Combine(templatePath, "Input.cs.vm"),Path.Combine(backendPath, "Dto", tableName + "Input.cs")),
                new SysCodeGenTemplateInfo(Path.Combine(templatePath, "Output.cs.vm"), Path.Combine(backendPath, "Dto", tableName + "Output.cs")),
                new SysCodeGenTemplateInfo(Path.Combine(templatePath, "Frontend.vue.vm"),
                Path.Combine(frontendPath.GetParentPath(),input.FrontPath.Substring(1),tableName.ToCamelCase(), tableName.ToCamelCase() + "List.vue")),
                new SysCodeGenTemplateInfo(Path.Combine(templatePath, "FrontendModel.vue.vm"),
                Path.Combine(frontendPath.GetParentPath(),input.FrontPath.Substring(1),tableName.ToCamelCase(),"modules", tableName.ToCamelCase() + "Model.vue")),
            };
            return templatePathList;
        }
        /// <summary>
        /// 字符串转stream储存为zip压缩包
        /// </summary>
        /// <param name="fileList"></param>
        /// <returns></returns>
        private byte[] StringsToZipBytes(List<FileInfoDto> fileList)
        {
            var streams = new List<FileMemoryInfoDto>();
            foreach (var item in fileList)
            {
                var bytes = System.Text.Encoding.UTF8.GetBytes(item.FileValue);
                var stream = new MemoryStream(bytes);
                streams.Add(new FileMemoryInfoDto(stream, item.FileName));
            }
            return FileHelper.SteamToZip(streams);
        }
    }
}