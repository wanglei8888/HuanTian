using Microsoft.AspNetCore.Http;
using NPOI.Util;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Text.Json.Nodes;
using System.Web;

namespace HuanTian.Service
{
    public class SysFileService : ISysFileService, IDynamicApiController
    {
        /// <summary>
        /// 传入JsonArray 生成Excel文件
        /// </summary>
        /// <param name="input">JsonArray类型</param>
        /// <param name="excelName">Excel名称</param>
        /// <param name="columsName">单独设置列名，如果不传值那就使用默认列名,传值实体转json</param>
        /// <returns>IActionResult excel文件</returns>
        [HttpPost]
        public IActionResult Download([Required][FromBody] JsonArray input,[FromQuery]string excelName = default!,[FromQuery] string columsName = default!)
        {
            if (input == null)
                throw new Exception("请传入正确的jsonArray格式入参");
            
            var columnsDic = new Dictionary<string,string>();
            var jobject = new JsonObject();
            var firstObject = input.FirstOrDefault();
            if (firstObject is JsonObject)
            {
                jobject = firstObject as JsonObject;
            }

            // 获取所有的 key 作为列名
            foreach (var item in jobject!)
            {
                columnsDic.Add(item.Key,item.Key);
            }
            // 如果传入了列名则使用传入的列名
            if (columsName.IsJsonObJect())
            {
                var columsNameObject = JsonNode.Parse(columsName)!.AsObject();
                foreach (var item in columsNameObject)
                {
                    if (!columnsDic.ContainsKey(item.Key))
                        continue;
                    columnsDic[item.Key] = item.Value!.ToString();
                }
            }
           
            // 存储所有数据
            var allData = input.Copy();

            // 如果列名包含 children 则递归获取所有children jsonobject 为 一个jsonarray
            if (columnsDic.ContainsKey("children"))
            {
                allData = TreeHelper.FlattenJsonArray(input);
            }

            // 生成 Excel
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Sheet1");

            // 添加列头
            for (int i = 0; i < columnsDic.Count; i++)
            {
                worksheet.Cells[1, i + 1].Value = columnsDic.ElementAt(i).Value;
            }

            // 添加行数据
            for (int rowIndex = 0; rowIndex < allData.Count; rowIndex++)
            {
                var data = allData[rowIndex];
                for (int colIndex = 0; colIndex < columnsDic.Count; colIndex++)
                {
                    var key = columnsDic.ElementAt(colIndex).Key;
                    var value = data![key];
                    worksheet.Cells[rowIndex + 2, colIndex + 1].Value = value?.ToString();
                }
            }
            #region 设置 excel 样式
            var headerRange = worksheet.Cells[$"A1:{GetColumnLetter(columnsDic.Count)}1"];
            headerRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
            headerRange.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#5b9bd5")); // 设置背景颜色为 #5b9bd5
            headerRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            headerRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            // 设置标题的字体颜色和加粗
            headerRange.Style.Font.Color.SetColor(Color.White); // 设置字体颜色
            headerRange.Style.Font.Bold = true; // 设置字体加粗 

            // 定义数据范围
            var dataRange = worksheet.Cells[2, 1, input.Count + 1, columnsDic.Count];

            // 设置填充模式和背景颜色
            dataRange.Style.Fill.PatternType = ExcelFillStyle.Solid; // 设置填充模式为实色填充
            dataRange.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#ddebf7")); // 设置背景颜色为 #ddebf7

            // 设置边框
            dataRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
            dataRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            dataRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
            dataRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;

            // 自适应列宽
            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
            #endregion

            // 将ExcelPackage转换为字节数组
            byte[] bytes;
            using (MemoryStream ms = new MemoryStream())
            {
                package.SaveAs(ms);
                bytes = ms.ToArray();
            }

            if (string.IsNullOrEmpty(excelName))
            {
                excelName = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_fff") + "导出";
            }
            // 设置响应头 文件名称
            var encodedFileName = HttpUtility.UrlEncode($"{excelName}", System.Text.Encoding.UTF8);
            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = $"{encodedFileName}.xlsx",
                Inline = false
            };

            App.HttpContext.Response.Headers.Add("Content-Disposition", cd.ToString());
            return new FileContentResult(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"); 
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<SysFileOutput> Upload([FromForm] SysFileInput input)
        {
            int count = 0;
            var output = new SysFileOutput();
            var path = Path.Combine(App.WebHostEnvironment.WebRootPath, "FileCount", input.FilePath, input.File.FileName);
            // 文件名
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(input.File.FileName);
            // 文件扩展名
            var extension = Path.GetExtension(input.File.FileName);

            if (!File.Exists(path.GetParentPath()))
            {
                Directory.CreateDirectory(path.GetParentPath());
            }
            string newFileName = $"{fileNameWithoutExtension}{extension}";
            path = Path.Combine(App.WebHostEnvironment.WebRootPath, "FileCount", input.FilePath, newFileName);
            if (input.Add)
            {
                // 判断否存在同名文件 加上序号
                while (File.Exists(path))
                {
                    count++;
                    newFileName = $"{fileNameWithoutExtension}-{count}{extension}";
                    path = Path.Combine(App.WebHostEnvironment.WebRootPath, "FileCount", input.FilePath, newFileName);
                }
            }

            if (input.Add || !File.Exists(path))
            {
                await using (var stream = new FileStream(path, FileMode.Create))
                {
                    await input.File.CopyToAsync(stream);
                }
            }
            output.FileName = Path.GetFileName(path);

            // 固定IP地址  不推荐使用
            // output.FilePath = (App.HttpContext.Request.Scheme + @"://" + Path.Combine(App.HttpContext.Request.Host.Value, "FileCount", input.FilePath, output.FileName)).Replace("\\", "/");

            // 相对地址  通过前端代理到服务器地址使用
            output.FilePath = ("\\"+ Path.Combine("FileCount", input.FilePath, output.FileName)).Replace("\\","/");
            return output;
        }
        /// <summary>
        /// 获取列号对应的 Excel 列字母
        /// </summary>
        /// <param name="columnNumber"></param>
        /// <returns></returns>
        private string GetColumnLetter(int columnNumber)
        {
            // 将列号转换为 Excel 列字母
            string columnLetter = "";
            int dividend = columnNumber;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                columnLetter = (char)(65 + modulo) + columnLetter;
                dividend = (dividend - modulo) / 26;
            }

            return columnLetter;
        }

    }

}
