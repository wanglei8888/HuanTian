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
using HuanTian.Entities;
using HuanTian.WebCore;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HuanTian.Service
{
    /// <summary>
    /// 前端静态数据访问接口 为节约时间暂时数据都是静态的,后期再考虑写入数据库
    /// </summary>
    [Route("api/")]
    public class TestData : IDynamicApiController
    {
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
                People=  70 ,
                Group =   30 ,
                Department =   40 
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
    }
}
