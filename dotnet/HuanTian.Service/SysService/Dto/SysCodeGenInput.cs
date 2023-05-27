#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023  NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：DESKTOP-6BOHDBE
 * 公司名称：
 * 命名空间：HuanTian.Service.CodeGen.Dto
 * 唯一标识：07275857-87af-499b-9f9a-3641e1094b67
 * 文件名：SysCodeGenInput
 * 当前用户域：DESKTOP-6BOHDBE
 * 
 * 创建者：wanglei
 * 电子邮箱：271976304@qq.com
 * 创建时间：2023/5/25 22:11:28
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

using Microsoft.AspNetCore.Routing.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HuanTian.Service
{
    /// <summary>
    /// SysCodeGenInput 的摘要说明
    /// </summary>
    public class SysCodeGenInput
    {
        /// <summary>
        /// 表格名字
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ApplicationName { get; set; } = "HuanTian.Service";
        /// <summary>
        /// 是否强制执行
        /// </summary>
        public bool Enforcement { get; set; } = false;
    }

    public class SysCodeGenTemplateInfo
    {
        public SysCodeGenTemplateInfo(string templatePath,string filePath)
        {
            TemplatePath = templatePath;
            FilePath = filePath;
        }
        public string TemplatePath { get; set; }
        public string FilePath { get; set; }
    }
    public class SysCodeGenTemplateInput
    {
        public string ClassName { get; set; }
        public string ClassNameLow { get; set; }
        public string EntityName { get; set; }
        public string EntityDescribe { get; set; }
        /// <summary>
        /// 是否包含修改信息
        /// </summary>
        public bool IsContailUpdate { get; set; }
        public string NameSpace { get; set; }
        public List<SysCodeGenDO> TableField { get; set; }
    }
}