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
        /// Excel导出测试asdasd中国
        /// </summary>
        [Fact]
        public void GenerateExcel()
        {
            IGenerateFilesService _generateFilesService = new GenerateFilesService();
            var list = new List<SysMenuDO>();
            var model = new SysMenuDO { Id = 1, Path = "test", Component = "caigou", Icon = "yasuo", Title = "中国人第一" };
            list.Add(model);
            var templateName = Path.Combine(_templatePath, "Template", "ExcelTemplate.xlsx");
            var byet = _generateFilesService.RenderTemplateExcel(templateName, list);
            FileStream fileStream = new("test.xlsx", FileMode.Create);
            fileStream.Write(byet, 0, byet.Length);
        }
    }
}