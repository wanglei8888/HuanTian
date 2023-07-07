#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023 Microsoft NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：MS-SOBOTFLKMEBA
 * 公司名称：Microsoft
 * 命名空间：HuanTian.Common
 * 唯一标识：13fcd490-cf6a-4de2-8f87-a740edb40ef4
 * 文件名：SteamHelper
 * 当前用户域：MS-SOBOTFLKMEBA
 * 
 * 创建者：wanglei
 * 电子邮箱：
 * 创建时间：2023/3/15 17:32:26
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

using HuanTian.Entities;
using System.IO.Compression;

namespace HuanTian.Infrastructure
{
    /// <summary>
    /// 文件信息帮助类
    /// </summary>
    public static class FileHelper
    {
        /// <summary>
        /// 将文件流转换为字节数组
        /// </summary>
        /// <param name="fileMemoryInfos"></param>
        /// <returns></returns>
        public static byte[] SteamToZip(List<FileMemoryInfoDto> fileMemoryInfos)
        {
            var output = new MemoryStream();
            using (var archive = new ZipArchive(output, ZipArchiveMode.Create, true))
            {
                foreach (var item in fileMemoryInfos)
                {
                    var fileInArchive = archive.CreateEntry(item.FileName, CompressionLevel.Optimal);
                    using (var entryStream = fileInArchive.Open())
                    {
                        item.FileMemory.CopyTo(entryStream);
                    }
                }
            }
            return output.ToArray();
        }
    }
}