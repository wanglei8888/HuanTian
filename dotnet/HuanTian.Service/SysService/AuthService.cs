﻿#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023  NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：DESKTOP-4P8G8RH
 * 公司名称：
 * 命名空间：HuanTian.Service
 * 唯一标识：d1e9cb75-3ee7-40a9-ad70-1850a42c3d35
 * 文件名：AuthService
 * 当前用户域：DESKTOP-4P8G8RH
 * 
 * 创建者：wanglei
 * 电子邮箱：271976304@qq.com
 * 创建时间：2023/4/5 20:59:47
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
using AutoMapper;
using HuanTian.Entities;
using HuanTian.EntityFrameworkCore;
using HuanTian.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HuanTian.Service
{
    public class AuthService
    {
        private readonly EfSqlContext _mySqlContext;
        private readonly IMapper _mapper;
        public AuthService(
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
        [AllowAnonymous]
        [HttpPost]
        public async Task<APIResult> Login(LoginInput input)
        {
            var userInfo = await _mySqlContext.UserInfoDO.FirstOrDefaultAsync(t => t.UserName == input.username && t.Password == input.password);
            LoginOutput output = new LoginOutput();
            if (userInfo != null)
            {
                output = _mapper.Map<LoginOutput>(userInfo);
                output.token = JWTHelper.GetToken(EncryptionHelper.Encrypt(userInfo.Id.ToString()));
                return APIResult.Success().SetData(output);
            }
            else
            {
                return APIResult.Error("登陆失败");
            }

        }
    }
}