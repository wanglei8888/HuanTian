#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023 HP NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：WEIHAN
 * 公司名称：HP
 * 命名空间：HuanTian.WebCore
 * 唯一标识：7def4609-818c-460b-af19-7ad8f9ca1b2d
 * 文件名：CustomStringLocalizer
 * 当前用户域：weihan
 * 
 * 创建者：wanglei
 * 电子邮箱：271976304@qq.com
 * 创建时间：2023/9/21 16:59:55
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
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using System.Collections.Concurrent;
using System.Globalization;

namespace HuanTian.WebCore
{
    public class CustomStringLocalizer : IStringLocalizer
    {
        private readonly ConcurrentDictionary<string, string> _localizedStrings;

        public CustomStringLocalizer()
        {
            var resourceFilesList = new List<string>();
            _localizedStrings = new ConcurrentDictionary<string, string>();

            // 加载同一个语言的所有 JSON 文件并汇总在一起
            var culture = CultureInfo.CurrentUICulture.Name;
            // 遍历下属的所有文件夹
            var subDirectories = Directory.GetDirectories("Resources", "*", SearchOption.AllDirectories);
            if (subDirectories.Length == 0)
            {
                resourceFilesList.AddRange(Directory.GetFiles("Resources", $"*.{culture}.json"));
            }
           
            foreach (var subDirectory in subDirectories)
            {
                resourceFilesList.AddRange(Directory.GetFiles(subDirectory, $"*.{culture}.json"));
            }
            
            foreach (var file in resourceFilesList)
            {
                var json = File.ReadAllText(file);
                var localizedStrings = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

                foreach (var localizedString in localizedStrings)
                {
                    _localizedStrings[localizedString.Key] = localizedString.Value;
                }
            }
        }

        public LocalizedString this[string name] => new LocalizedString(name, GetString(name));

        public LocalizedString this[string name, params object[] arguments] => new LocalizedString(name, GetString(name, arguments));

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            return _localizedStrings.Select(kv => new LocalizedString(kv.Key, kv.Value));
        }

        public string GetString(string name)
        {
            return _localizedStrings.TryGetValue(name, out var value) ? value : name;
        }

        public string GetString(string name, params object[] arguments)
        {
            var format = GetString(name);
            return string.Format(format, arguments);
        }
    }
    public class CustomStringLocalizerFactory : IStringLocalizerFactory
    {
        public IStringLocalizer Create(Type resourceSource)
        {
            return new CustomStringLocalizer();
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            return new CustomStringLocalizer();
        }
    }

}