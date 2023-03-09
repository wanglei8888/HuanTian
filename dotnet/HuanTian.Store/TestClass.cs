using HuanTian.WebCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Huangtian.Store
{
    [ApiDescriptionSettings(Name = "Test", Order = 100)]
    public class TestClass : IDynamicApiController
    {
        /// <summary>
        /// 测试方法
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public string Name()
        {
            return "";
        }
    }


    [ApiDescriptionSettings(Name = "Test2", Order = 10)]
    public class TestClass1 : IDynamicApiController
    {
        /// <summary>
        /// 测试方法
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string Name2()
        {
            return "";
        }
    }
}
