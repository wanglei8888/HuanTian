using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;

namespace HuanTian.EntityFrameworkCore
{
    /// <summary>
    /// EF Core 日志记录器
    /// </summary>
    public class EfLogger : ILogger
    {
        private readonly string _categoryName;

        public EfLogger(string categoryName)
        {
            _categoryName = categoryName;
        }

        public IDisposable BeginScope<TState>(TState state) => default;

        public bool IsEnabled(LogLevel logLevel) => logLevel >= LogLevel.Information;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (logLevel == LogLevel.Information && state != null && formatter != null)
            {
                var message = formatter(state, exception);
                if (message.StartsWith("Executed DbCommand"))
                {
                    // 匹配从最后一个 `]` 到字符串末尾的部分
                    var sqlPattern = @"\].*?(\r?\n|\r)(?<sql>.+)$";
                    var sqlMatch = Regex.Match(message, sqlPattern, RegexOptions.Singleline);
                    var sqlContent = sqlMatch.Groups["sql"]?.Value ?? "";

                    // 定义字典用于存储参数键值对
                    // var parametersDic = new Dictionary<string, string>();

                    // 使用正则表达式匹配参数部分
                    var paramsPattern = @"@(?<name>[a-zA-Z0-9_]+)=(?<value>NULL|'[^']*')(?:\s*\((?:Nullable\s*=\s*(?:true|false))?(?:\s*(?:Size|DbType)\s*=\s*[^\)]*)?\))*";
                    var paramasMatches = Regex.Matches(message, paramsPattern);

                    // 遍历匹配结果并填充字典
                    foreach (Match paramasMarch in paramasMatches)
                    {
                        string name = paramasMarch.Groups["name"].Value;
                        string value = paramasMarch.Groups["value"].Value;
                        
                        if (sqlContent.Contains($"@{name}"))
                        {
                            // 判断类型
                            if (bool.TryParse(value.Replace("'", ""), out bool boolResult))
                            {
                                sqlContent = sqlContent.Replace($"@{name}", $"{boolResult}");
                            }
                            else
                            {
                                sqlContent = sqlContent.Replace($"@{name}", $"{value}");
                            }
                            
                        }
                        //if (parametersDic.ContainsKey(name))
                        //    continue;
                        //parametersDic.Add(name, value);
                    }
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("--------------------------------------------------------------");

                    if (sqlContent.StartsWith("SELECT"))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    if (sqlContent.StartsWith("UPDATE") || sqlContent.StartsWith("INSERT"))
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    if (sqlContent.StartsWith("DELETE"))
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }
                    Console.WriteLine(sqlContent);

                }
            }
        }

    }

    /// <summary>
    /// EF Core 日志记录器
    /// </summary>
    public class EFLoggerProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName)
        {
            return new EfLogger(categoryName);
        }

        public void Dispose() { }
    }

}
