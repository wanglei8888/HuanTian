using HuanTian.EntityFrameworkCore;
using HuanTian.Infrastructure;
using HuanTian.Service;

namespace HuanTian.Test
{
    public class GenerateTest : BaseTest
    {
        private readonly string _templatePath = AppContext.BaseDirectory.Replace("HuanTian.Test\\bin\\Debug\\net6.0\\", "HuanTian.Store\\wwwroot");
        public GenerateTest()
        {

        }

        /// <summary>
        /// Excel导出测试
        /// </summary>
        [Fact]
        public void GenerateExcel()
        {
            var _generateFilesService = GetService<IGenerateFilesService>();
            var model = new SysMenuDO { Id = 1, Name = "test", Title = "中国人第一" };
            var templateName = Path.Combine(_templatePath, "Template", "ExcelTemplate.xlsx");
            var byet = _generateFilesService.RenderTemplateExcel(templateName, model);
            FileStream fileStream = new("test.xlsx", FileMode.Create);
            fileStream.Write(byet, 0, byet.Length);
        }
        /// <summary>
        /// 行转列测试
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task RowToColumn()
        {
            var data = await GetService<IRepository<SysLogErrorDO>>().ToListAsync();
            var data1 = data.ToPivotArray(x => x.Level,
                x => new { x.Path, x.Level },
                x => x.Any() ? x.Count() : 0);
            var jsonData = data1.ToJsonString();
        }
    }
}