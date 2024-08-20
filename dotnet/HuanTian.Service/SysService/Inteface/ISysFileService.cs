using System.Text.Json.Nodes;

namespace HuanTian.Service
{
    public interface ISysFileService
    {
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<SysFileOutput> Upload(SysFileInput input);

        /// <summary>
        /// 传入JsonArray 生成Excel文件
        /// </summary>
        /// <param name="input">JsonArray类型</param>
        /// <param name="excelName">Excel名称</param>
        /// <param name="columsName">单独设置列名，如果不传值那就默认列名</param>
        IActionResult Download([FromBody] JsonArray input, [FromQuery] string excelName = default!, [FromQuery] string columsName = default!);
    }
}
