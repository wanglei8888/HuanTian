using Microsoft.AspNetCore.Http;

namespace HuanTian.Service
{
    public class SysFileInput
    {
        public IFormFile File { get; set; }
        /// <summary>
        /// 文件储存地址
        /// </summary>
        public string FilePath { get; set; } = "File";
        /// <summary>
        /// true 新增同名文件会累加序号,false 修改同名会跳过
        /// </summary>
        public bool Add { get; set; } = true;
    }
}
