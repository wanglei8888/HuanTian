using AutoMapper;
using HuanTian.Entities;
using HuanTian.Infrastructure;
using HuanTian.WebCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Huangtian.Service
{
    /// <summary>
    ///  test1231
    /// </summary>
    [ApiDescriptionSettings(Name = "UserInfoApi", Order = 100)]
    public class UserInfoApi : IDynamicApiController
    {
        //依赖注入数据库仓储访问方式
        private readonly IRepository<SysUserInfoDO> _repository;
        private readonly IMapper _mapper;
        public UserInfoApi(
            IRepository<SysUserInfoDO> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        /// <summary>
        /// 测试方法
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public async Task<dynamic> GetUserInfo123()
        {
            var list = await _repository.GetAllAsync(t => t.Name != "");
            var test2 = _mapper.Map<IEnumerable<SysUserInfoDO>>(list);
            //throw new Exception("test123");
            return new SysMenuDO();
        }

    }

}
