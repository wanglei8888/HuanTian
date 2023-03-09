using AutoMapper;
using HuanTian.Common;
using HuanTian.DtoModel;
using HuanTian.EntityFrameworkCore.MySql;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Huangtian.Store.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly EfSqlContext _mySqlContext;
        private readonly IMapper _mapper;
        public AuthController(
            EfSqlContext mySqlContext,
            IMapper mapper
            )
        {
            _mySqlContext = mySqlContext;
            _mapper = mapper;
        }
        /// <summary>
        /// 获取登陆信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<APIResult> Login(LoginInput input)
        {
            var userInfo = await _mySqlContext.UserInfoDO.FirstOrDefaultAsync(t => t.UserName == input.username && t.Password == input.password);
            LoginOutput output = new LoginOutput();
            if (userInfo != null)
            {
                output = _mapper.Map<LoginOutput>(userInfo);
                output.token = JWTHelper.GetToken(EncryptionHelper.Encrypt(userInfo.ID.ToString()));
                return APIResult.Success().SetData(output);
            }
            else
            {
                return APIResult.Error("登陆失败");
            }
            

        }
    }
}
