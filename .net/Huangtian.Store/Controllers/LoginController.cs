using AutoMapper;
using HuanTian.Domain;
using HuanTian.DtoModel;
using HuanTian.EntityFrameworkCore.MySql;
using HuanTian.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Huangtian.Store.Controllers
{
    [Route("api/auth/[controller]")]
    [ApiController]
    public class LoginController : BaseController
    {
        private readonly MySqlContext _mySqlContext;
        private readonly ITestService _testAutofac;
        private readonly IMapper _mapper;
        public LoginController(
            MySqlContext mySqlContext,
            ITestService testAutofac,
            IMapper mapper
            )
        {
            _mySqlContext = mySqlContext;
            _testAutofac = testAutofac;
            _mapper = mapper;
        }

        [HttpPost(Name = "login")]
        public async Task<IEnumerable<WeatherForecast>> Get(LoginInput input)
        {
            var sa1 = _testAutofac.AddNum(5,6);
            var list = await _mySqlContext.UserInfoDO.ToListAsync();
            if (list != null)
            {
                var output = _mapper.Map<IEnumerable<LoginOutput>>(list);
            }

            //HttpContext.Response.Headers.Add("x-my-custom-header", "individual response");
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = null//Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();

        }
    }
}
