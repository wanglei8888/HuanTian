﻿using HuanTian.Infrastructure;

namespace HuanTian.Infrastructure.Dto
{
    /// <summary>
    /// 发送邮件实体
    /// </summary>
    public class EmailMQDto : IMsgQBaseEntity
    {
        public EmailMQDto(string subject, string[] to, string data, SysTenantDO tenant)
        {
            //为实体赋值
            Subject = subject;
            To = to;
            Data = data;
            Tenant = tenant;
        }
        public string[] To { get; set; }
        public string Subject { get; set; }
        public string Data { get; set; }
        public SysTenantDO Tenant { get; set; }
        public string ErrorMsg { get; set; }
    }
}