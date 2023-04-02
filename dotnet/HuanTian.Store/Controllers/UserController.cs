using HuanTian.DtoModel;
using HuanTian.EntityFrameworkCore;
using HuanTian.Interface;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace Huangtian.Store.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {

        private readonly ILogger<UserController> _logger;
        private readonly EfSqlContext _mySqlContext;
        private readonly IMneuService _mneuService;

        public UserController(
            ILogger<UserController> logger,
            EfSqlContext mySqlContext,
            IMneuService mneuService)
        {
            _logger = logger;
            _mySqlContext = mySqlContext;
            _mneuService = mneuService;
        }

        //[Authorize]
        [HttpGet]
        public async Task<IEnumerable<MenuOutput>> Info()
        {
            var user = HttpContext.User.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sid)?.Value;
            var menu = await _mneuService.GetUserMenu(1);
            return menu;
        }

    }
}