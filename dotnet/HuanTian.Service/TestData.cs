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
using HuanTian.Entities.DtoModel;
using HuanTian.WebCore;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace HuanTian.Service
{
    [Route("api/")]
    public class TestData : IDynamicApiController
    {
        [HttpGet("list/search/projects")]
        public dynamic Get()
        {
            string jsonString = "[{\"id\":1,\"cover\":\"https://gw.alipayobjects.com/zos/rmsportal/WdGqmHpayyMjiEhcKoVE.png\",\"title\":\"Alipay\",\"description\":\"那是一种内在的东西， 他们到达不了，也无法触及的\",\"status\":1,\"updatedAt\":\"2018-07-26 00:00:00\"},{\"id\":2,\"cover\":\"https://gw.alipayobjects.com/zos/rmsportal/zOsKZmFRdUtvpqCImOVY.png\",\"title\":\"Angular\",\"description\":\"希望是一个好东西，也许是最好的，好东西是不会消亡的\",\"status\":1,\"updatedAt\":\"2018-07-26 00:00:00\"},{\"id\":3,\"cover\":\"https://gw.alipayobjects.com/zos/rmsportal/dURIMkkrRFpPgTuzkwnB.png\",\"title\":\"Ant Design\",\"description\":\"城镇中有那么多的酒馆，她却偏偏走进了我的酒馆\",\"status\":1,\"updatedAt\":\"2018-07-26 00:00:00\"},{\"id\":4,\"cover\":\"https://gw.alipayobjects.com/zos/rmsportal/sfjbOqnsXXJgNCjCzDBL.png\",\"title\":\"Ant Design Pro\",\"description\":\"那时候我只会想自己想要什么，从不想自己拥有什么\",\"status\":1,\"updatedAt\":\"2018-07-26 00:00:00\"},{\"id\":5,\"cover\":\"https://gw.alipayobjects.com/zos/rmsportal/siCrBXXhmvTQGWPNLBow.png\",\"title\":\"Bootstrap\",\"description\":\"凛冬将至\",\"status\":1,\"updatedAt\":\"2018-07-26 00:00:00\"},{\"id\":6,\"cover\":\"https://gw.alipayobjects.com/zos/rmsportal/ComBAopevLwENQdKWiIn.png\",\"title\":\"Vue\",\"description\":\"生命就像一盒巧克力，结果往往出人意料\",\"status\":1,\"updatedAt\":\"2018-07-26 00:00:00\"}]";
            var role = JsonConvert.DeserializeObject<List<Item>>(jsonString);
            var page = new PagingEntity() { Data = role, PageNo = 0, PageSize = 10, TotalPage = 6, TotalCount = 57 };
            return page;
        }
    }
}
