using System.Buffers.Text;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace HuanTian.Infrastructure
{
    /// <summary>
    /// 加密解密
    /// </summary>
    public class EncryptionHelper
    {
        /// <summary>
        /// 普通随机加密算法
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string Encrypt(string source)
        {
            string randomNumber = GenerateRandom(11);
            return randomNumber.Substring(0, 5) + Encode(Encoding.UTF8, source).Replace("=", ".").Replace('+', '*').Replace('/', '-') + randomNumber.Substring(6);
        }

        /// <summary>
        /// 普通随机解密算法
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

        /// <summary>
        /// AES加密 
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="skey">16位长度key值</param>
        /// <returns></returns>
        public static string Encrypt(string text, string skey)
        {
            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(skey);
                using Aes aes = Aes.Create();
                using ICryptoTransform transform = aes.CreateEncryptor(bytes, aes.IV);
                using MemoryStream memoryStream = new MemoryStream();
                using (CryptoStream stream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write, leaveOpen: true))
                {
                    using StreamWriter streamWriter = new StreamWriter(stream, null, -1, leaveOpen: true);
                    streamWriter.Write(text);
                }

                byte[] iV = aes.IV;
                int num = iV.Length + (int)memoryStream.Length;
                byte[] buffer = memoryStream.GetBuffer();
                int bytesWritten = Base64.GetMaxEncodedToUtf8Length(num);
                byte[] array = new byte[bytesWritten];
                Unsafe.CopyBlock(ref array[0], ref iV[0], (uint)iV.Length);
                Unsafe.CopyBlock(ref array[iV.Length], ref buffer[0], (uint)memoryStream.Length);
                Base64.EncodeToUtf8InPlace(array, num, out bytesWritten);
                return Encoding.ASCII.GetString(array.AsSpan().Slice(0, bytesWritten));
            }
            catch (Exception)
            {
                return string.Empty;
            }
          
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="hash">加密值</param>
        /// <param name="skey">16位长度key值</param>
        /// <returns></returns>
        public static string Decrypt(string hash, string skey)
        {
            try
            {
                byte[] array = Convert.FromBase64String(hash);
                byte[] array2 = new byte[16];
                byte[] array3 = new byte[array.Length - array2.Length];
                Unsafe.CopyBlock(ref array2[0], ref array[0], (uint)array2.Length);
                Unsafe.CopyBlock(ref array3[0], ref array[array2.Length], (uint)(array.Length - array2.Length));
                byte[] bytes = Encoding.UTF8.GetBytes(skey);
                using Aes aes = Aes.Create();
                using ICryptoTransform transform = aes.CreateDecryptor(bytes, array2);
                using MemoryStream stream = new MemoryStream(array3);
                using CryptoStream stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Read);
                using StreamReader streamReader = new StreamReader(stream2);
                return streamReader.ReadToEnd();
            }
            catch (Exception)
            {

                return string.Empty;
            }
           
        }
    }
}
