#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023  NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：DESKTOP-6BOHDBE
 * 公司名称：
 * 命名空间：HuanTian.Infrastructure.Dto
 * 唯一标识：2f5ece9f-ceb0-47e0-a316-d023206fb7c8
 * 文件名：FileInfo
 * 当前用户域：DESKTOP-6BOHDBE
 * 
 * 创建者：wanglei
 * 电子邮箱：271976304@qq.com
 * 创建时间：2023/7/4 21:07:40
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HuanTian.Infrastructure
{
    /// <summary>
    /// 文件信息dto
    /// </summary>
    public class FileInfoDto
    {
        public FileInfoDto(string fileValue, string fileName)
        {
            FileValue = fileValue;
            FileName = fileName;
        }
        /// <summary>
        /// 文件值
        /// </summary>
        public string FileValue { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }
    }
    public class FileMemoryInfoDto
    {
        public FileMemoryInfoDto(MemoryStream fileMemory, string fileName)
        {
            FileMemory = fileMemory;
            FileName = fileName;
        }
        /// <summary>
        /// 文件值
        /// </summary>
        public MemoryStream FileMemory { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }
    }
}