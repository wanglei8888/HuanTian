using HuanTian.Domain;
using HuanTian.WebCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Huangtian.Store
{
    [ApiDescriptionSettings(Name = "Test", Order = 100)]
    public class DynamicApi : IDynamicApiController
    {
        /// <summary>
        /// 测试方法
        /// </summary>
        /// <returns></returns>
        //[Authorize]
        [HttpGet]
        public dynamic Name()
        {
            //throw new Exception("test123");
            return new SysMenuDO();
        }
    }

}
