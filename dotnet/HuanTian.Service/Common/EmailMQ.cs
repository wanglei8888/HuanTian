using HuanTian.Infrastructure.Dto;
using Scriban;
using SqlSugar.Extensions;
using System.Net;
using System.Net.Mail;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace HuanTian.Service
{
    /// <summary>
    /// 邮件消息队列
    /// </summary>
    public class EmailMQ
    {
        /// <summary>
        /// 发送邮件将信息存到消息队列中
        /// </summary>
        /// <param name="value">实体值</param>
        /// <param name="templateName">模板名称</param>
        /// <param name="emailSubject">邮件名称</param>
        /// <param name="email">发送的邮箱</param>
        public async static Task SendEmail<TEntity>(TEntity value, string templateName, string emailSubject, params string[] email)
        {
            var filePath = Path.Combine(App.WebHostEnvironment.WebRootPath, "Template", "Email", templateName + ".html");
            // 租户信息
            var tenantInfo = (await App.GetService<ISysTenantService>().Get(new SysTenantInput() { Id = App.GetTenantId() })).ToList()[0];
            if (string.IsNullOrWhiteSpace(tenantInfo.EmailConfig) || tenantInfo.EmailConfig.Split(';').Length != 4)
                throw new ArgumentNullException("租户邮箱配置错误,请修改后再试");
            
            var htmlContent = File.ReadAllText(filePath);
            // 替换其中的占位符
            var template = Template.Parse(htmlContent);
            var result = template.Render(value);
            var emailModel = new EmailMQDto(emailSubject, email, result, tenantInfo);
            // 送邮件将信息存到消息队列中
            var emailQueue = App.GetService<IMessageQueue>();
            emailQueue.SelectQueue(MsgQConst.Email)
                .Enqueue(emailModel.ToJsonString());
        }
        /// <summary>
        /// 打开邮件消息队列消费端
        /// </summary>
        public static void OpenConsumer()
        {
            var messageQueue = App.GetService<IMessageQueue>().SelectQueue(MsgQConst.Email);
            messageQueue.StartConsumingAsync(async (message) =>
            {
                // 处理接收到的消息
                var emailInfo = JsonSerializer.Deserialize<EmailMQDto>(message);
                var emailConfig = emailInfo.Tenant.EmailConfig.Split(';');
                // 创建MailMessage对象 设置发件人邮箱
                using MailMessage mail = new MailMessage();
                mail.From = new MailAddress(emailConfig[0]);
                // 设置收件人邮箱
                foreach (var item in emailInfo.To)
                {
                    // 检测邮箱格式
                    if (!IsValidEmail(item))
                    {
                        Console.WriteLine(item + "邮箱格式不正确");
                        continue;
                    }
                    mail.To.Add(item);
                }
                if (mail.To.Count == 0)
                    throw new ArgumentNullException("收件人邮箱配置错误,请修改后再试");
                
                // 设置邮件主题和正文
                mail.Subject = emailInfo.Subject;
                mail.Body = emailInfo.Data;
                mail.IsBodyHtml = true;
                // 创建SmtpClient对象
                using SmtpClient smtpClient = new SmtpClient(emailConfig[2], emailConfig[3].ObjToInt());
                // 设置发件人的邮箱账号和密码
                smtpClient.Credentials = new NetworkCredential(emailConfig[0], emailConfig[1]);
                // 启用SSL加密
                smtpClient.EnableSsl = true;

                // 发送邮件
                await smtpClient.SendMailAsync(mail);
                Console.WriteLine("邮件发送成功！");
               
                return true;
            });
        }
        private static bool IsValidEmail(string email)
        {
            // 定义电子邮件地址的正则表达式模式
            string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";

            // 使用正则表达式验证电子邮件地址的格式
            bool isValid = Regex.IsMatch(email, pattern);

            return isValid;
        }
    }
}
