using Autofac;
using Autofac.Core;
using HuanTian.Infrastructure;
using HuanTian.EntityFrameworkCore;
using HuanTian.Service;
using HuanTian.WebCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Drawing.Printing;

namespace HuanTian.Test
{
    public class GenerateTest
    {
        private readonly IServiceProvider _scopeFactory;
        private readonly string _templatePath = AppContext.BaseDirectory.Replace("HuanTian.Test\\bin\\Debug\\net6.0\\", "HuanTian.Store\\wwwroot");
        public GenerateTest()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new AutofacRegister());
            var container = builder.Build();
            //_scopeFactory = container.Resolve<IServiceProvider>();
            //_generateFilesService = container.Resolve<IGenerateFilesService>();

        }

        /// <summary>
        /// Excel导出测试asdasd中国
        /// </summary>
        [Fact]
        public void GenerateExcel()
        {
            IGenerateFilesService _generateFilesService = new GenerateFilesService();
            var list = new List<SysMenuDO>();
            var model = new SysMenuDO { Id = 1, Path = "test", Component = "caigou", Icon = "yasuo" ,Title = "中国人第一"};
            //var model2 = new SysMenuDO { RoleId = 2, Path = "test2", Component = "caigou2", Icon = "yasuo2" };
            list.Add(model);
            var templateName = Path.Combine(_templatePath, "Template", "ExcelTemplate.xlsx");
            var byet = _generateFilesService.RenderTemplateExcel(templateName, list);
            FileStream fileStream = new("test.xlsx", FileMode.Create);
            fileStream.Write(byet, 0, byet.Length);
        }
    }
}