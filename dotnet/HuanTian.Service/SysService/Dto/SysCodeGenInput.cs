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
        public long Id { get; set; }
        /// <summary>
        /// 是否强制执行
        /// </summary>
        public bool Enforcement { get; set; } = false;
    }
    public class SysCodeGenFileInput
    {
        public string TableName { get; set; }
        public string ApplicationName { get; set; }
        public string FrontPath { get; set; }
    }
    public class SysCodeGenTableInput : IPageInput
    {
        /// <summary>
        /// 表格名字
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        public int PageSize { get; set; }
        public int PageNo { get; set; }
    }
    public class SysCodeGenGetInput
    {
        /// <summary>
        /// 表格名字
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        public long MasterId { get; set; }
    }
    public class SysCodeGenFormInput:SysCodeGenDO
    {
        
    }
    public class SysCodeGenDetailFormInput
    {
        public List<SysCodeGenDetailDO> Detail { get; set; }
    }
    public class SysCodeGenTemplateInfo
    {
        public SysCodeGenTemplateInfo(string templatePath, string filePath)
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
        public string NameSpace { get; set; }
        public bool IsContailUpdate { get; set; }
        /// <summary>
        /// 后端命名为帕斯卡
        /// </summary>
        public List<SysCodeGenDetailDO> TableField { get; set; }
        /// <summary>
        /// 前端命名为驼峰
        /// </summary>
        public List<SysCodeGenDetailDO> TableFieldCamelCase { get; set; }
        public int QueryLineCount { get; set; }
        public int TableFieldCount { get; set; }
    }
}