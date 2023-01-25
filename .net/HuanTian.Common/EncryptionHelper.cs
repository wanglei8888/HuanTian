using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HuanTian.Common
{
    public class EncryptionHelper
    {
        /// <summary>
        /// 加密算法
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string Encrypt(string source)
        {
            string randomNumber = GenerateRandom(11);
            return randomNumber.Substring(0, 5) + Encode(Encoding.UTF8, source).Replace("=", ".").Replace('+', '*').Replace('/', '-') + randomNumber.Substring(6);
        }

        /// <summary>
        /// 解密算法
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string Decrypt(string source)
        {
            source = source.Substring(5);
            source = source.Substring(0, source.Length - 5);
            return Decode(Encoding.UTF8, source.Replace(".", "=").Replace('*', '+').Replace('-', '/'));
        }

        /// <summary>
        /// 随机产生字母或数字
        /// </summary>
        /// <param name="Length"></param>
        /// <returns></returns>
        private static string GenerateRandom(int Length)
        {
            char[] constant =
            {
              '0','1','2','3','4','5','6','7','8','9',
              'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',
              'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'
            };
            StringBuilder newRandom = new StringBuilder(62);
            Random rd = new Random();
            for (int i = 0; i < Length; i++)
            {
                newRandom.Append(constant[rd.Next(62)]);
            }
            return newRandom.ToString();
        }
        /// <summary>
        /// Base64编码
        /// </summary>
        /// <param name="encode">加密采用的编码方式</param>
        /// <param name="source">待加密的明文</param>
        /// <returns></returns>
        private static string Encode(Encoding encode, string source)
        {
            string encodeString = "";
            byte[] bytes = encode.GetBytes(source);
            try
            {
                encodeString = Convert.ToBase64String(bytes);
            }
            catch
            {
                encodeString = source;
            }
            return encodeString;
        }

        /// <summary>
        /// Base64解码
        /// </summary>
        /// <param name="encode">解密采用的编码方式，注意和加密时采用的方式一致</param>
        /// <param name="result">待解密的密文</param>
        /// <returns>解密后的字符串</returns>
        private static string Decode(Encoding encode, string result)
        {
            string decodeString = "";
            byte[] bytes = Convert.FromBase64String(result);
            try
            {
                decodeString = encode.GetString(bytes);
            }
            catch
            {
                decodeString = result;
            }
            return decodeString;
        }
        /// <summary>
        /// SHA1 加密，返回大写字符串
        /// </summary>
        /// <param name="content">需要加密字符串</param>
        /// <returns>返回40位UTF8 大写</returns>
        public static string SHA1(string content)
        {
            return SHA1(content, Encoding.UTF8);
        }
        /// <summary>
        /// SHA1 加密，返回大写字符串
        /// </summary>
        /// <param name="content">需要加密字符串</param>
        /// <param name="encode">指定加密编码</param>
        /// <returns>返回40位大写字符串</returns>
        public static string SHA1(string content, Encoding encode)
        {
            try
            {
                SHA1 sha1 = new SHA1CryptoServiceProvider();
                byte[] bytes_in = encode.GetBytes(content);
                byte[] bytes_out = sha1.ComputeHash(bytes_in);
                sha1.Dispose();
                string result = BitConverter.ToString(bytes_out);
                result = result.Replace("-", "");
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("SHA1加密出错：" + ex.Message);
            }
        }
    }
}
