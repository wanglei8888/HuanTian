using HuanTian.WebCore;
using Microsoft.AspNetCore.Mvc;

namespace Huangtian.Store
{
    public class TestClass : IDynamicApiController
    {
        [HttpGet]
        public string Name()
        {
            return "";
        }
    }
}
