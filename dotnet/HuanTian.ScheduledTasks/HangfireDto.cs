using System.Text;

namespace HuanTian.ScheduledTasks
{
    public class HangfireSettings
    {
        public string ServerName { get; set; }
        public string TablePrefix { get; set; }
        public string StartUpPath { get; set; }
        public string ReadOnlyPath { get; set; }
        public List<string> JobQueues { get; set; }
        public HttpAuthInfo HttpAuthInfo { get; set; } = new HttpAuthInfo();
        public int WorkerCount { get; set; } = 40;
        public bool DisplayStorageConnectionString { get; set; } = false;
    }

    public class HttpAuthInfo
    {
        /// <summary>
        /// Redirects all non-SSL requests to SSL URL
        /// </summary>
        public bool SslRedirect { get; set; } = false;

        /// <summary>
        /// Requires SSL connection to access Hangfire dahsboard. It's strongly recommended to use SSL when you're using basic authentication.
        /// </summary>
        public bool RequireSsl { get; set; } = false;

        /// <summary>
        /// Whether or not login checking is case sensitive.
        /// </summary>
        public bool LoginCaseSensitive { get; set; } = true;

        public bool IsOpenLogin { get; set; } = true;

        public List<UserInfo> Users { get; set; } = new List<UserInfo>();
    }

    public class UserInfo
    {
        public string Login { get; set; }
        public string PasswordClear { get; set; }

        public byte[] Password => Encoding.UTF8.GetBytes(PasswordClear);
    }
}
