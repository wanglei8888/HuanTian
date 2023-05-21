#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023 HP NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：WEIHAN
 * 公司名称：HP
 * 命名空间：HuanTian.Service.Common
 * 唯一标识：bc34d234-bb5d-4ef4-a74e-596a3df11972
 * 文件名：GenerateFilesService
 * 当前用户域：weihan
 * 
 * 创建者：wanglei
 * 创建时间：2023/5/17 14:54:49
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

using iText.Html2pdf;
using iText.Html2pdf.Resolver.Font;
using iText.IO.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using iText.Layout.Properties;
using OfficeOpenXml;
using QRCoder;
using RazorEngine;
using RazorEngine.Templating;
using SixLabors.ImageSharp.Formats.Jpeg;
using System.Text.RegularExpressions;

namespace HuanTian.Service
{
    public class GenerateFilesService : IGenerateFilesService, IScoped
    {
        /// <summary>
		///     匹配表达式
		/// </summary>
		private const string VariableRegexString = "(\\{\\{)+([\\w_.>|\\?:&=]*)+(\\}\\})";
        /// <summary>
        ///     变量正则
        /// </summary>
        private readonly Regex _variableRegex = new Regex(VariableRegexString, RegexOptions.IgnoreCase);
        public byte[] RenderTemplatePdf<TEntity>(string templateAddress, TEntity entity, PdfSetting setInfo = default)
        {
            try
            {
                // 读取 Razor 模板文件
                var templateString = File.ReadAllText(templateAddress);
                using (var stream = new MemoryStream())
                {
                    using var pdfWriter = new PdfWriter(stream);
                    using (var pdfDocument = new PdfDocument(pdfWriter))
                    {
                        // 添加字体
                        var fontProvider = new DefaultFontProvider();
                        var fontPath = System.IO.Path.Combine(App.WebHostEnvironment.WebRootPath, "Template", "SIMKAI.TTF");
                        var fontProgram = FontProgramFactory.CreateFont(fontPath);
                        fontProvider.AddFont(fontProgram);

                        // 设置 PDF 页面大小
                        pdfDocument.SetDefaultPageSize(PageSize.A4.Rotate());
                        if (setInfo != null && (setInfo.Width != 0 || setInfo.Height != 0))
                        {
                            var pageSize = new PageSize(setInfo.Width, setInfo.Height);
                            pdfDocument.SetDefaultPageSize(pageSize);
                        }

                        // 转换为文件流
                        var elements = new List<IElement>();
                        
                        // 使用 Razor 转换引擎生成 HTML 字符串
                        var htmlString = Engine.Razor.RunCompile(templateString, Guid.NewGuid().ToString(), typeof(TEntity), entity);
                        elements.AddRange(HtmlConverter.ConvertToElements(htmlString, new ConverterProperties().SetFontProvider(fontProvider)));
                        
                        using var document = new iText.Layout.Document(pdfDocument);
                        // 设置 PDF 外边距
                        if (setInfo?.Margin != null)
                        {
                            document.SetMargins(setInfo.Margin.Top, setInfo.Margin.Right, setInfo.Margin.Bottom, setInfo.Margin.Left);
                        }
                        // 添加进document 容器
                        foreach (var element in elements)
                        {
                            var index = elements.IndexOf(element) + 1;
                            document.Add((IBlockElement)element);
                            if (index != elements.Count())
                            {
                                document.Add(new AreaBreak(AreaBreakType.NEXT_PAGE)); // 在元素列表末尾添加分页符
                            }
                        }

                    }
                    // 将 PDF 数据写入字节数组并返回
                    return stream.ToArray();
                
            }
                //File.WriteAllBytes("output.pdf", stream.ToArray());
            }
            catch (Exception ex)
            {
                throw new FriendlyException($"{nameof(RenderTemplatePdf)}生成失败。" + ex.Message);
            }

        }
        public byte[] RenderTemplatePdf<TEntity>(string templateAddress, IEnumerable<TEntity> entityList, PdfSetting setInfo = null)
        {
            try
            {
                // 读取 Razor 模板文件
                var templateString = File.ReadAllText(templateAddress);
                using (var stream = new MemoryStream())
                {
                    using var pdfWriter = new PdfWriter(stream);
                    using (var pdfDocument = new PdfDocument(pdfWriter))
                    {
                        // 添加字体
                        var fontProvider = new DefaultFontProvider();
                        var fontPath = System.IO.Path.Combine(App.WebHostEnvironment.WebRootPath, "Template", "SIMKAI.TTF");
                        var fontProgram = FontProgramFactory.CreateFont(fontPath);
                        fontProvider.AddFont(fontProgram);

                        // 设置 PDF 页面大小
                        pdfDocument.SetDefaultPageSize(PageSize.A4.Rotate());
                        if (setInfo != null && (setInfo.Width != 0 || setInfo.Height != 0))
                        {
                            var pageSize = new PageSize(setInfo.Width, setInfo.Height);
                            pdfDocument.SetDefaultPageSize(pageSize);
                        }

                        // 转换为文件流
                        var elements = new List<IElement>();
                        foreach (var element in entityList)
                        {
                            // 使用 Razor 转换引擎生成 HTML 字符串
                            var htmlString = Engine.Razor.RunCompile(templateString, Guid.NewGuid().ToString(), typeof(TEntity), element);
                            elements.AddRange(HtmlConverter.ConvertToElements(htmlString, new ConverterProperties().SetFontProvider(fontProvider)));
                        }

                        using var document = new iText.Layout.Document(pdfDocument);
                        // 设置 PDF 外边距
                        if (setInfo?.Margin != null)
                        {
                            document.SetMargins(setInfo.Margin.Top, setInfo.Margin.Right, setInfo.Margin.Bottom, setInfo.Margin.Left);
                        }
                        // 添加进document 容器
                        foreach (var element in elements)
                        {
                            var index = elements.IndexOf(element) + 1;
                            document.Add((IBlockElement)element);
                            if (index != elements.Count())
                            {
                                document.Add(new AreaBreak(AreaBreakType.NEXT_PAGE)); // 在元素列表末尾添加分页符
                            }
                        }

                    }
                    // 将 PDF 数据写入字节数组并返回
                    return stream.ToArray();
                }
            }
            catch (Exception ex)
            {
                throw new FriendlyException($"{nameof(RenderTemplatePdf)}生成失败。" + ex.Message);
            }
        }
        public byte[] RenderQrCode(string value)
        {
            var qrGenerator = new QRCodeGenerator();
            var qrCodeData = qrGenerator.CreateQrCode(value, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new QRCode(qrCodeData);
            var qrCodeImage = qrCode.GetGraphic(20);

            using (var ms = new MemoryStream())
            {
                // 二维码储存一个位置
                // using var stream = new FileStream(@"D:\Email\qrcode.png", FileMode.Create);
                qrCodeImage.Save(ms, new JpegEncoder());
                return ms.ToArray();
            }
        }
        public byte[] RenderTemplateExcel<TEntity>(string templateAddress, IEnumerable<TEntity> entityList)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var templatePackage = new ExcelPackage(new FileInfo(templateAddress)))
            {
                // 获取模板中的工作表
                foreach (var sheet in templatePackage.Workbook.Worksheets)
                {
                    if (sheet.Dimension == null)
                        continue;
                    var endColumnIndex = sheet.Dimension.End.Column;
                    var endRowIndex = sheet.Dimension.End.Row;
                    var excelColumns = (from cell in sheet.Cells[sheet.Dimension.Start.Row, sheet.Dimension.Start.Column,
                       endRowIndex, endColumnIndex]
                                        where _variableRegex.IsMatch((cell.Value ?? string.Empty).ToString())
                                        select cell).ToList();
                    // 遍历工作簿中的可替换变量值
                    foreach (var excelItem in excelColumns)
                    {
                        var nameMatch = Regex.Match(excelItem.Text, @"\{\{(.*?)\}\}");
                        if (nameMatch.Success)
                        {
                            var entityName = nameMatch.Groups[1].Value;
                            var excelVar = Regex.Match(excelItem.Text, VariableRegexString).Value;
                            // 遍历集合
                            var i = 0;
                            foreach (var item in entityList)
                            {
                                // 集合就一直往下显示数据
                                var value = TEntityHelper<TEntity>.GetEnutityColumn(item, entityName);
                                var addressIndex = Regex.Match(excelItem.Address, @"\d+").Value;
                                var address = excelItem.Address.Replace(addressIndex, (Convert.ToInt32(addressIndex) + i).ToString());
                                sheet.Cells[address].Value = excelItem.Text.Replace(excelVar, value);
                                i++;
                            }
                        }
                    }
                }
                // 将ExcelPackage转换为字节数组
                byte[] bytes;
                using (MemoryStream ms = new MemoryStream())
                {
                    templatePackage.SaveAs(ms);
                    bytes = ms.ToArray();
                }
                return bytes;
            }
        }
        public byte[] RenderTemplateExcel<TEntity>(string templateAddress, TEntity entity)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var templatePackage = new ExcelPackage(new FileInfo(templateAddress)))
            {
                // 获取模板中的工作表
                foreach (var sheet in templatePackage.Workbook.Worksheets)
                {
                    if (sheet.Dimension == null)
                        continue;
                    var endColumnIndex = sheet.Dimension.End.Column;
                    var endRowIndex = sheet.Dimension.End.Row;
                    var excelColumns = (from cell in sheet.Cells[sheet.Dimension.Start.Row, sheet.Dimension.Start.Column,
                       endRowIndex, endColumnIndex]
                                        where _variableRegex.IsMatch((cell.Value ?? string.Empty).ToString())
                                        select cell).ToList();
                    // 遍历工作簿中的可替换变量值
                    foreach (var excelItem in excelColumns)
                    {
                        var nameMatch = Regex.Match(excelItem.Text, @"\{\{(.*?)\}\}");
                        if (nameMatch.Success)
                        {
                            var entityName = nameMatch.Groups[1].Value;
                            var value = TEntityHelper<TEntity>.GetEnutityColumn(entity, entityName);
                            var excelVar = Regex.Match(excelItem.Text, VariableRegexString).Value;
                            sheet.Cells[excelItem.Address].Value = excelItem.Text.Replace(excelVar, value);
                        }
                    }
                }
                // 将ExcelPackage转换为字节数组
                byte[] bytes;
                using (MemoryStream ms = new MemoryStream())
                {
                    templatePackage.SaveAs(ms);
                    bytes = ms.ToArray();
                }
                return bytes;
            }
        }

    }
}
