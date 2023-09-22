﻿using Hangfire;
using Hangfire.Console;
using Hangfire.Dashboard.BasicAuthorization;
using Hangfire.Heartbeat;
using Hangfire.Heartbeat.Server;
using Hangfire.HttpJob;
using Hangfire.MySql;
using Hangfire.Server;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using Spring.Core.TypeConversion;
using System.Data;
using IsolationLevel = System.Transactions.IsolationLevel;

namespace HuanTian.ScheduledTasks
{
    public static class HangfireCollectionExtensions
    {
        private const string HangfireSettingsKey = "Hangfire:HangfireSettings";
        private const string HttpJobOptionsKey = "Hangfire:HttpJobOptions";
        private const string HangfireConnectStringKey = "ConnectionStrings:MySql";
        private const string HangfireLangKey = "Hangfire:HttpJobOptions:Lang";

        public static IServiceCollection AddSelfHangfire(this IServiceCollection services, IConfiguration configuration)
        {
            var hangfireSettings = configuration.GetSection(HangfireSettingsKey);
            var httpJobOptions = configuration.GetSection(HttpJobOptionsKey);

            services.Configure<HangfireSettings>(hangfireSettings);
            services.Configure<HangfireHttpJobOptions>(httpJobOptions);

            services.AddTransient<IBackgroundProcess, ProcessMonitor>();

            services.AddHangfire(globalConfiguration =>
            {
                services.ConfigurationHangfire(configuration, globalConfiguration);
            });



            services.AddHangfireServer((provider, config) =>
            {
                var settings = provider.GetService<IOptions<HangfireSettings>>().Value;
                // ConfigFromEnv(settings);
                var queues = settings.JobQueues.Select(m => m.ToLower()).Distinct().ToList();
                var workerCount = Math.Max(Environment.ProcessorCount, settings.WorkerCount); //工作线程数，当前允许的最大线程，默认20


                config.ServerName = settings.ServerName;
                config.ServerTimeout = TimeSpan.FromMinutes(4);
                config.SchedulePollingInterval = TimeSpan.FromSeconds(15);//秒级任务需要配置短点，一般任务可以配置默认时间，默认15秒
                config.ShutdownTimeout = TimeSpan.FromMinutes(30); //超时时间
                config.Queues = queues.ToArray(); //队列
                config.WorkerCount = workerCount;
            });

            return services;
        }


        public static void ConfigurationHangfire(this IServiceCollection services, IConfiguration configuration,
            IGlobalConfiguration globalConfiguration)
        {
            var serverProvider = services.BuildServiceProvider();

            var langStr = configuration.GetSection(HangfireLangKey).Get<string>();
            var envLangStr = GetEnvConfig<string>("Lang");
            if (!string.IsNullOrEmpty(envLangStr)) langStr = envLangStr;
            if (!string.IsNullOrEmpty(langStr))
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(langStr);
            }

            var hangfireSettings = serverProvider.GetService<IOptions<HangfireSettings>>().Value;
            // ConfigFromEnv(hangfireSettings);

            var httpJobOptions = serverProvider.GetService<IOptions<HangfireHttpJobOptions>>().Value;
            // ConfigFromEnv(httpJobOptions);

            httpJobOptions.GlobalSettingJsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory!, "hangfire",
                "hangfire_global.json");

            var sqlConnectStr = configuration.GetSection(HangfireConnectStringKey).Get<string>();
            var envSqlConnectStr = GetEnvConfig<string>("HangfireMysqlConnectionString");
            if (!string.IsNullOrEmpty(envSqlConnectStr)) sqlConnectStr = envSqlConnectStr;

            var mysqlOption = new MySqlStorageOptions
            {
                TransactionIsolationLevel = IsolationLevel.ReadCommitted,               // 事务隔离级别。默认为已提交读。
                QueuePollInterval = TimeSpan.FromSeconds(15),                     // 作业队列轮询间隔。默认值为 15 秒。
                JobExpirationCheckInterval = TimeSpan.FromHours(1),               // 作业过期检查间隔（管理过期记录）。默认值为 1 小时。
                CountersAggregateInterval = TimeSpan.FromMinutes(5),              // 聚合计数器的间隔。默认为 5 分钟 
                PrepareSchemaIfNecessary = false,                                       // 如果设置为true，它会创建数据库表。默认为true
                DashboardJobListLimit = 50000,                                          // 仪表板作业列表限制。默认值为 50000
                TransactionTimeout = TimeSpan.FromMinutes(1),                     // 交易超时。默认值为 1 分钟
                TablesPrefix = hangfireSettings.TablePrefix                             // 数据库中表的前缀
            };

            globalConfiguration
                .UseStorage(new MySqlStorage(sqlConnectStr, mysqlOption))
                .UseConsole(new ConsoleOptions
                {
                    BackgroundColor = "#000079"
                })
                .UseHangfireHttpJob(httpJobOptions)
                .UseHeartbeatPage();
        }

        public static IApplicationBuilder ConfigureSelfHangfire(this IApplicationBuilder app, IConfiguration Configuration)
        {
            var langStr = Configuration.GetSection(HangfireLangKey).Get<string>();
            var envLangStr = GetEnvConfig<string>("Lang");
            if (!string.IsNullOrEmpty(envLangStr)) langStr = envLangStr;

            if (!string.IsNullOrEmpty(langStr))
            {
                var options = new RequestLocalizationOptions
                {
                    DefaultRequestCulture = new RequestCulture(langStr)
                };
                app.UseRequestLocalization(options);
            }

            var services = app.ApplicationServices;
            var hangfireSettings = services.GetService<IOptions<HangfireSettings>>().Value;


            var dashbordConfig = new DashboardOptions
            {
                AppPath = "#",
                IgnoreAntiforgeryToken = true,
                DisplayStorageConnectionString = hangfireSettings.DisplayStorageConnectionString,
                IsReadOnlyFunc = Context => false
            };

            if (hangfireSettings.HttpAuthInfo.IsOpenLogin && hangfireSettings.HttpAuthInfo.Users.Any())
            {
                var httpAuthInfo = hangfireSettings.HttpAuthInfo;
                var users = hangfireSettings.HttpAuthInfo.Users.Select(m => new BasicAuthAuthorizationUser
                {
                    Login = m.Login,
                    Password = m.Password,
                    PasswordClear = m.PasswordClear
                });

                var basicAuthAuthorizationFilterOptions = new BasicAuthAuthorizationFilterOptions
                {
                    RequireSsl = httpAuthInfo.RequireSsl,
                    SslRedirect = httpAuthInfo.SslRedirect,
                    LoginCaseSensitive = httpAuthInfo.LoginCaseSensitive,
                    Users = users
                };

                dashbordConfig.Authorization = new[]
                {
                    new BasicAuthAuthorizationFilter(basicAuthAuthorizationFilterOptions)
                };

            }

            app.UseHangfireHttpJob().UseHangfireDashboard(hangfireSettings.StartUpPath, dashbordConfig);

            if (!string.IsNullOrEmpty(hangfireSettings.ReadOnlyPath))
                //只读面板，只能读取不能操作 
                app.UseHangfireDashboard(hangfireSettings.ReadOnlyPath, new DashboardOptions
                {
                    IgnoreAntiforgeryToken = true,
                    AppPath = hangfireSettings.StartUpPath, //返回时跳转的地址
                    DisplayStorageConnectionString = false, //是否显示数据库连接信息
                    IsReadOnlyFunc = Context => true
                });

            return app;
        }


        #region Docker运行的参数配置https://github.com/yuzd/Hangfire.HttpJob/wiki/000.Docker-Quick-Start


        private static void ConfigFromEnv(HangfireSettings settings)
        {
            var hangfireQueues = GetEnvConfig<string>("HangfireQueues");
            if (!string.IsNullOrEmpty(hangfireQueues))
            {
                settings.JobQueues = hangfireQueues.Split(',').ToList();
            }
            var serverName = GetEnvConfig<string>("ServerName");
            if (!string.IsNullOrEmpty(serverName))
            {
                settings.ServerName = serverName;
            }
            var workerCount = GetEnvConfig<string>("WorkerCount");
            if (!string.IsNullOrEmpty(workerCount))
            {
                settings.WorkerCount = int.Parse(workerCount);
            }

            var tablePrefix = GetEnvConfig<string>("TablePrefix");
            if (!string.IsNullOrEmpty(tablePrefix))
            {
                settings.TablePrefix = tablePrefix;
            }

            var hangfireUserName = GetEnvConfig<string>("HangfireUserName");
            var hangfirePwd = GetEnvConfig<string>("HangfirePwd");
            if (!string.IsNullOrEmpty(hangfireUserName) && !string.IsNullOrEmpty(hangfirePwd))
            {
                settings.HttpAuthInfo = new HttpAuthInfo { Users = new List<UserInfo>() };
                settings.HttpAuthInfo.Users.Add(new UserInfo
                {
                    Login = hangfireUserName,
                    PasswordClear = hangfirePwd
                });
            }
        }

        private static void ConfigFromEnv(HangfireHttpJobOptions settings)
        {
            var defaultRecurringQueueName = GetEnvConfig<string>("DefaultRecurringQueueName");
            if (!string.IsNullOrEmpty(defaultRecurringQueueName))
            {
                settings.DefaultRecurringQueueName = defaultRecurringQueueName;
            }

            if (settings.MailOption == null) settings.MailOption = new MailOption();

            var hangfireMailServer = GetEnvConfig<string>("HangfireMail_Server");
            if (!string.IsNullOrEmpty(hangfireMailServer))
            {
                settings.MailOption.Server = hangfireMailServer;
            }

            var hangfireMailPort = GetEnvConfig<int>("HangfireMail_Port");
            if (hangfireMailPort > 0)
            {
                settings.MailOption.Port = hangfireMailPort;
            }

            var hangfireMailUseSsl = Environment.GetEnvironmentVariable("HangfireMail_UseSsl");
            if (!string.IsNullOrEmpty(hangfireMailUseSsl))
            {
                settings.MailOption.UseSsl = hangfireMailUseSsl.ToLower().Equals("true");
            }

            var hangfireMailUser = GetEnvConfig<string>("HangfireMail_User");
            if (!string.IsNullOrEmpty(hangfireMailUser))
            {
                settings.MailOption.User = hangfireMailUser;
            }

            var hangfireMailPassword = GetEnvConfig<string>("HangfireMail_Password");
            if (!string.IsNullOrEmpty(hangfireMailPassword))
            {
                settings.MailOption.Password = hangfireMailPassword;
            }

        }
        private static T GetEnvConfig<T>(string key)
        {
            try
            {
                var value = Environment.GetEnvironmentVariable(key.Replace(":", "_"));
                if (!string.IsNullOrEmpty(value))
                {
                    return (T)TypeConversionUtils.ConvertValueIfNecessary(typeof(T), value, null);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return default;
        }

        #endregion

    }

    
}