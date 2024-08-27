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

        public IDisposable BeginScope<TState>(TState state) => null;

        public bool IsEnabled(LogLevel logLevel) => logLevel >= LogLevel.Information;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (logLevel == LogLevel.Information && state != null && formatter != null)
            {
                var message = formatter(state, exception);
                if (message.StartsWith("Executed DbCommand"))
                {
                    // 提取 SQL 语句和参数列表
                    var sqlPattern = new Regex(@"CommandType='Text'\].*\n\s+(?<sql>.*?)\s+SELECT ROW_COUNT", RegexOptions.Singleline);
                    var paramPattern = new Regex(@"Parameters=\[(?<params>.*?)\], CommandType", RegexOptions.Singleline);

                    var sqlMatch = sqlPattern.Match(message);
                    var paramMatch = paramPattern.Match(message);

                    if (sqlMatch.Success && paramMatch.Success)
                    {
                        var sql = sqlMatch.Groups["sql"].Value;
                        var paramList = paramMatch.Groups["params"].Value;

                        // 替换参数值
                        var paramPatternForReplacement = new Regex(@"@(?<name>\w+)='(?<value>.*?)'", RegexOptions.Singleline);
                        sql = paramPatternForReplacement.Replace(sql, m =>
                        {
                            var name = m.Groups["name"].Value;
                            var value = m.Groups["value"].Value;
                            return sql.Replace($"@{name}", $"'{value}'");
                        });

                        // 输出替换后的 SQL
                        Console.WriteLine("Executed SQL with parameters: ");
                        Console.WriteLine(sql);
                    }
                }
            }
        }
    }

    // 使用自定义日志记录器
    public class SqlLoggerProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName)
        {
            return new EfLogger(categoryName);
        }

        public void Dispose() { }
    }

}
