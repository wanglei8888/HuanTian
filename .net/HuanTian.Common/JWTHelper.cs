using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HuanTian.Common
{
    /// <summary>
    /// JWT操作类
    /// </summary>
    public class JWTHelper
    {
        /// <summary>
        /// 生成JWT Token
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static string GetToken(string userId)
        {
            //生成JWT
            //Header,选择签名算法
            var signingAlogorithm = SecurityAlgorithms.HmacSha256;
            //Payload,存放用户信息，下面我们放了一个用户id
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sid,userId)
            };
            //Signature
            //取出私钥并以utf8编码字节输出
            var sd1a = Appsettings.GetInfo("JWTAuthentication:SecretKey");
            var sd1aaa = Appsettings.GetInfo("JWTAuthentication:Issuer");
            var sd1aaasda = Appsettings.GetInfo("JWTAuthentication:Audience");
            var secretByte = Encoding.UTF8.GetBytes(Appsettings.GetInfo("JWTAuthentication:SecretKey"));
            //使用非对称算法对私钥进行加密
            var signingKey = new SymmetricSecurityKey(secretByte);
            //使用HmacSha256来验证加密后的私钥生成数字签名
            var signingCredentials = new SigningCredentials(signingKey, signingAlogorithm);
            //生成Token
            var Token = new JwtSecurityToken(
                    issuer: Appsettings.GetInfo("JWTAuthentication:Issuer"),        //发布者
                    audience: Appsettings.GetInfo("JWTAuthentication:Audience"),    //接收者
                    claims: claims,                                         //存放的用户信息
                    notBefore: DateTime.UtcNow,                             //发布时间
                    expires: DateTime.UtcNow.AddDays(1),                      //有效期设置为1天
                    signingCredentials                                      //数字签名
                );
            //生成字符串token
            var TokenStr = new JwtSecurityTokenHandler().WriteToken(Token);
            return TokenStr;
        }
    }
}
