using HuanTian.Domain;
using HuanTian.Interface;
using HuanTian.WebCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Huangtian.Store
{
    [ApiDescriptionSettings(Name = "Test", Order = 100)]
    public class DynamicApi : IDynamicApiController
    {
        //依赖注入数据库仓储访问方式
        private readonly IRepository<SysUserInfoDO> _repository;
        public DynamicApi(IRepository<SysUserInfoDO> repository)
        {
            _repository = repository;
        }
        /// <summary>
        /// 测试方法
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public async Task<dynamic> NameAsync()
        {
            var list = await _repository.GetAllAsync(t => t.Name != "");
            //throw new Exception("test123");
            return new SysMenuDO();
        }
    }

}
