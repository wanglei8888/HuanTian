#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023 HP NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：WEIHAN
 * 公司名称：HP
 * 命名空间：HuanTian.Infrastructure
 * 唯一标识：b81d6b6f-6974-40c7-b891-2ac929f2c77f
 * 文件名：StringExtensions
 * 当前用户域：weihan
 * 
 * 创建者：wanglei
 * 创建时间：2023/4/28 17:37:27
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

using HuanTian.Entities;
using System.Text;
using System.Text.RegularExpressions;

namespace HuanTian.Infrastructure
{
    /// <summary>
    /// string 字符串拓展
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// 字符串转Enum,支持 string 和 int 例:(Enable、1)
        /// </summary>
        /// <typeparam name="T">枚举</typeparam>
        /// <param name="str">字符串</param>
        /// <returns>转换的枚举</returns>
        public static T ToEnum<T>(this string str)
        {
            return (T)Enum.Parse(typeof(T), str);
        }
        /// <summary>
        /// 格式化字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string Format(this string str, params object[] args)
        {
            return args == null || args.Length == 0 ? str : string.Format(str, args);
        }
        /// <summary>
        /// 切割骆驼命名式字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string[] SplitCamelCase(this string str)
        {
            if (str == null) return Array.Empty<string>();

            if (string.IsNullOrWhiteSpace(str)) return new string[] { str };
            if (str.Length == 1) return new string[] { str };

            return Regex.Split(str, @"(?=\p{Lu}\p{Ll})|(?<=\p{Ll})(?=\p{Lu})")
                .Where(u => u.Length > 0)
                .ToArray();
        }

        /// <summary>
        /// 清除字符串前后缀
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="pos">0：前后缀，1：后缀，-1：前缀</param>
        /// <param name="affixes">前后缀集合</param>
        /// <returns></returns>
        public static string ClearStringAffixes(this string str, int pos = 0, params string[] affixes)
        {
            // 空字符串直接返回
            if (string.IsNullOrWhiteSpace(str)) return str;

            // 空前后缀集合直接返回
            if (affixes == null || affixes.Length == 0) return str;

            var startCleared = false;
            var endCleared = false;

            string tempStr = null;
            foreach (var affix in affixes)
            {
                if (string.IsNullOrWhiteSpace(affix)) continue;

                if (pos != 1 && !startCleared && str.StartsWith(affix, StringComparison.OrdinalIgnoreCase))
                {
                    tempStr = str[affix.Length..];
                    startCleared = true;
                }
                if (pos != -1 && !endCleared && str.EndsWith(affix, StringComparison.OrdinalIgnoreCase))
                {
                    var _tempStr = !string.IsNullOrWhiteSpace(tempStr) ? tempStr : str;
                    tempStr = _tempStr[..^affix.Length];
                    endCleared = true;
                }
                if (startCleared && endCleared) break;
            }

            return !string.IsNullOrWhiteSpace(tempStr) ? tempStr : str;
        }
        /// <summary>
        /// 首字母小写
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToLowerCamelCase(this string str)
        {
            if (string.IsNullOrWhiteSpace(str)) return str;

            return string.Concat(str.First().ToString().ToLower(), str.AsSpan(1));
        }

        /// <summary>
        /// 首字母大写
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToUpperCamelCase(this string str)
        {
            if (string.IsNullOrWhiteSpace(str)) return str;

            return string.Concat(str.First().ToString().ToUpper(), str.AsSpan(1));
        }

        /// <summary>
        /// string将属性名转换成小写，并将驼峰命名方式转换为下划线命名方式
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToLowerHump(this string value) => string.Concat(value.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString())).ToLower();
        /// <summary>
        /// 默认为分钟单位仅支持天以下单位 例: 24 * 60 等于一天,
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type">时间单位</param>
        /// <returns>时间单位距今多少</returns>
        public static TimeSpan ToTimeSpan(this string value, TimeUnit type = TimeUnit.Minute)
        {
            var array = value.Replace(" ", "").Split('*');
            var sumMinuts = 0;
            foreach (var item in array)
            {
                sumMinuts += Convert.ToInt32(item);
            }
            return type switch
            {
                TimeUnit.Minute => TimeSpan.FromMinutes(sumMinuts),
                TimeUnit.Hour => TimeSpan.FromHours(sumMinuts),
                TimeUnit.Day => TimeSpan.FromDays(sumMinuts),
                TimeUnit.Week => TimeSpan.FromDays(sumMinuts),
                _ => TimeSpan.FromMinutes(sumMinuts),
            };
        }
        /// <summary>
        /// 字符串转换为帕斯卡命名方式
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToPascalCase(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            var words = Regex.Split(input, @"(?<!^)(?=[A-Z])|_"); // 使用正则表达式拆分单词
            var result = new StringBuilder();

            foreach (var word in words)
            {
                result.Append(char.ToUpper(word[0])); // 首字母大写
                result.Append(word.Substring(1).ToLower()); // 其余字母小写
            }

            return result.ToString();
        }
        /// <summary>
        /// 字符串转换为驼峰命名方式
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToCamelCase(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            var words = Regex.Split(input, @"(?<!^)(?=[A-Z])|_| "); // 使用正则表达式拆分单词
            var result = new StringBuilder();

            foreach (var word in words)
            {
                if (result.Length == 0)
                {
                    result.Append(word.ToLower()); // 第一个单词直接转为小写
                }
                else
                {
                    result.Append(char.ToUpper(word[0])); // 首字母大写
                    result.Append(word.Substring(1).ToLower()); // 其余字母小写
                }
            }

            return result.ToString();
        }
        /// <summary>
        /// 获取文件路径的父级路径
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetParentPath(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            return new DirectoryInfo(input).Parent.FullName;
        }
    }
}
