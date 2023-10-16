#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023  NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：DESKTOP-4P8G8RH
 * 公司名称：
 * 命名空间：HuanTian.Service
 * 唯一标识：9657fe8f-ecca-445a-818e-bd6d1639a163
 * 文件名：TestData
 * 当前用户域：DESKTOP-4P8G8RH
 * 
 * 创建者：wanglei
 * 电子邮箱：271976304@qq.com
 * 创建时间：2023/4/16 16:49:53
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
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using System.Globalization;

namespace HuanTian.Service
{
    /// <summary>
    /// 前端静态数据访问接口 为节约时间暂时数据都是静态的,后期再考虑写入数据库
    /// </summary>
    [Route("api/")]
    public class TestData : IDynamicApiController
    {
        private readonly IStringLocalizer _localizer;
        private readonly IRepository<SysMenuDO> _sysMenu;
        private readonly IGenerateFilesService _generateFilesService;
        public TestData(IRepository<SysMenuDO> sysMenu, IGenerateFilesService generateFilesService, IStringLocalizer localizer)
        {
            _sysMenu = sysMenu;
            _generateFilesService = generateFilesService;
            _localizer = localizer;
        }
        /// <summary>
        /// 多语言测试
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public dynamic LanguageTest()
        {
            var regionCode = CultureInfo.CurrentCulture.Name;
            var value = _localizer.GetString("中文注释").Value + "--"+ App.I18n.GetString("中文注释");
            // 支持URL http://localhost:8080/api/TestData/LanguageTest?ui-culture=zh-CN
            // Cookie 在当前网页中添加一个Cookie，名称为.AspNetCore.Culture，值为c=zh-CN|uic=zh-CN，输出结果为中文
            // Header 添加头部信息  accept-language :  en-US
            return value;
        }
        [HttpGet("list/search/projects")]
        public dynamic GetProjects()
        {
            string jsonString = "[{\"id\":1,\"cover\":\"https://gw.alipayobjects.com/zos/rmsportal/WdGqmHpayyMjiEhcKoVE.png\",\"title\":\"Alipay\",\"description\":\"那是一种内在的东西， 他们到达不了，也无法触及的\",\"status\":1,\"updatedAt\":\"2018-07-26 00:00:00\"},{\"id\":2,\"cover\":\"https://gw.alipayobjects.com/zos/rmsportal/zOsKZmFRdUtvpqCImOVY.png\",\"title\":\"Angular\",\"description\":\"希望是一个好东西，也许是最好的，好东西是不会消亡的\",\"status\":1,\"updatedAt\":\"2018-07-26 00:00:00\"},{\"id\":3,\"cover\":\"https://gw.alipayobjects.com/zos/rmsportal/dURIMkkrRFpPgTuzkwnB.png\",\"title\":\"Ant Design\",\"description\":\"城镇中有那么多的酒馆，她却偏偏走进了我的酒馆\",\"status\":1,\"updatedAt\":\"2018-07-26 00:00:00\"},{\"id\":4,\"cover\":\"https://gw.alipayobjects.com/zos/rmsportal/sfjbOqnsXXJgNCjCzDBL.png\",\"title\":\"Ant Design Pro\",\"description\":\"那时候我只会想自己想要什么，从不想自己拥有什么\",\"status\":1,\"updatedAt\":\"2018-07-26 00:00:00\"},{\"id\":5,\"cover\":\"https://gw.alipayobjects.com/zos/rmsportal/siCrBXXhmvTQGWPNLBow.png\",\"title\":\"Bootstrap\",\"description\":\"凛冬将至\",\"status\":1,\"updatedAt\":\"2018-07-26 00:00:00\"},{\"id\":6,\"cover\":\"https://gw.alipayobjects.com/zos/rmsportal/ComBAopevLwENQdKWiIn.png\",\"title\":\"Vue\",\"description\":\"生命就像一盒巧克力，结果往往出人意料\",\"status\":1,\"updatedAt\":\"2018-07-26 00:00:00\"}]";
            var role = JsonConvert.DeserializeObject<List<ProjectItem_Test>>(jsonString);
            var page = new PageData() { Data = role, PageNo = 0, PageSize = 10, TotalPage = 6, TotalCount = 57 };
            return page;
        }
        [HttpGet("workplace/activity")]
        public dynamic GetActivity()
        {
            string jsonString = "[{\"id\":1,\"user\":{\"nickname\":\"@name\",\"avatar\":\"https://gw.alipayobjects.com/zos/rmsportal/BiazfanxmamNRoxxVxka.png\"},\"project\":{\"name\":\"白鹭酱油开发组\",\"action\":\"更新\",\"event\":\"番组计划\"},\"time\":\"2018-08-23 14:47:00\"},{\"id\":1,\"user\":{\"nickname\":\"蓝莓酱\",\"avatar\":\"https://gw.alipayobjects.com/zos/rmsportal/jZUIxmJycoymBprLOUbT.png\"},\"project\":{\"name\":\"白鹭酱油开发组\",\"action\":\"更新\",\"event\":\"番组计划\"},\"time\":\"2018-08-23 09:35:37\"},{\"id\":1,\"user\":{\"nickname\":\"@name\",\"avatar\":\"https://gw.alipayobjects.com/zos/rmsportal/BiazfanxmamNRoxxVxka.png\"},\"project\":{\"name\":\"白鹭酱油开发组\",\"action\":\"创建\",\"event\":\"番组计划\"},\"time\":\"2017-05-27 00:00:00\"},{\"id\":1,\"user\":{\"nickname\":\"曲丽丽\",\"avatar\":\"https://gw.alipayobjects.com/zos/rmsportal/jZUIxmJycoymBprLOUbT.png\"},\"project\":{\"name\":\"高逼格设计天团\",\"action\":\"更新\",\"event\":\"六月迭代\"},\"time\":\"2018-08-23 14:47:00\"},{\"id\":1,\"user\":{\"nickname\":\"@name\",\"avatar\":\"https://gw.alipayobjects.com/zos/rmsportal/jZUIxmJycoymBprLOUbT.png\"},\"project\":{\"name\":\"高逼格设计天团\",\"action\":\"created\",\"event\":\"六月迭代\"},\"time\":\"2018-08-23 14:47:00\"},{\"id\":1,\"user\":{\"nickname\":\"曲丽丽\",\"avatar\":\"https://gw.alipayobjects.com/zos/rmsportal/BiazfanxmamNRoxxVxka.png\"},\"project\":{\"name\":\"高逼格设计天团\",\"action\":\"created\",\"event\":\"六月迭代\"},\"time\":\"2018-08-23 14:47:00\"}]";
            var activity = JsonConvert.DeserializeObject<List<ActivityData_Test>>(jsonString);
            return activity;
        }
        [HttpGet("workplace/teams")]
        public dynamic GetTeams()
        {
            string jsonString = "[{\"id\":1,\"name\":\"科学搬砖组\",\"avatar\":\"https://gw.alipayobjects.com/zos/rmsportal/BiazfanxmamNRoxxVxka.png\"},{\"id\":2,\"name\":\"程序员日常\",\"avatar\":\"https://gw.alipayobjects.com/zos/rmsportal/cnrhVkzwxjPwAaCfPbdc.png\"},{\"id\":1,\"name\":\"设计天团\",\"avatar\":\"https://gw.alipayobjects.com/zos/rmsportal/gaOngJwsRYRaVAuXXcmB.png\"},{\"id\":1,\"name\":\"中二少女团\",\"avatar\":\"https://gw.alipayobjects.com/zos/rmsportal/ubnKSIfAJTxIgXOKlciN.png\"},{\"id\":1,\"name\":\"骗你学计算机\",\"avatar\":\"https://gw.alipayobjects.com/zos/rmsportal/WhxKECPNujWoWEFNdnJE.png\"}]";
            var activity = JsonConvert.DeserializeObject<List<Team_Test>>(jsonString);
            return activity;
        }
        [HttpGet("workplace/radar")]
        public dynamic GetRadar()
        {
            List<RadarDataItem_Test> data = new List<RadarDataItem_Test>();

            // 添加数据项
            data.Add(new RadarDataItem_Test
            {
                Item = "引用",
                People = 70,
                Group = 30,
                Department = 40
            });
            data.Add(new RadarDataItem_Test
            {
                Item = "口碑",
                People = 60,
                Group = 70,
                Department = 40
            });
            data.Add(new RadarDataItem_Test
            {
                Item = "产量",
                People = 50,
                Group = 60,
                Department = 40
            });
            data.Add(new RadarDataItem_Test
            {
                Item = "贡献",
                People = 40,
                Group = 50,
                Department = 40
            });
            data.Add(new RadarDataItem_Test
            {
                Item = "热度",
                People = 60,
                Group = 70,
                Department = 40
            });
            data.Add(new RadarDataItem_Test
            {
                Item = "引用",
                People = 70,
                Group = 50,
                Department = 40
            });
            return data;
        }
        /// <summary>
        /// 导出excel 模板
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> DownmldExcel()
        {
            var menuList = await _sysMenu.ToListAsync();
            var templatePath = Path.Combine(App.WebHostEnvironment.WebRootPath, "template", "ExcelTemplate.xlsx");
            var bytes = _generateFilesService.RenderTemplateExcel(templatePath, menuList);
            return new FileContentResult(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet") { FileDownloadName = "test.xlsx" };
        }
        /// <summary>
        /// 导出PDF 模板
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult DownmldPdf()
        {
            var output = new BoxInfoOutput()
            {
                PartNumber = "DFS.2461513",
                Version = "abPeole",
                ProductName = "软件设计",
                Quantity = "56 PCS",
                ProductionDate = DateTime.Now.ToString("yyyy-MM-dd"),
                Supplier = "阿里巴巴事业部",
                QRCode = Convert.ToBase64String(_generateFilesService.RenderQrCode("这里是二维码内容"))
            };
            var setting = new PdfSetting(0,0,new PdfMargin(0,0,0,0));
            var templatePath = Path.Combine(App.WebHostEnvironment.WebRootPath, "template", "PdfTemplate.html");
            var bytes = _generateFilesService.RenderTemplatePdf(templatePath, output, setting);
            return new FileContentResult(bytes, "application/pdf") { FileDownloadName = "test.pdf" };
        }
        /// <summary>
        /// 发送邮件测试
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task SendEmail()
        {
            // 1、需要先配置租户邮件信息    2、确保消息队列地址配置正确，服务已启动
            var userInfo = await App.GetService<IRepository<SysUserDO>>().FirstOrDefaultAsync(t => t.Id == App.GetUserId());
            await EmailMQ.SendEmail(userInfo, "用户信息模板", "用户信息提醒邮件", "wangxiaopang8888@163.com");
        }
    }
    
}
