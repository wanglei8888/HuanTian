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
using System.Reflection;

namespace HuanTian.Service
{
    public class GenerateFilesService : IGenerateFilesService, IScoped
    {
        public byte[] RenderPdf<TEntity>(string templateAddress, TEntity entity, PdfSetting setInfo = default)
        {
            try
            {
                // 读取 Razor 模板文件
                var templateString = File.ReadAllText(templateAddress);
                // 使用 Razor 转换引擎生成 HTML 字符串
                var htmlString = Engine.Razor.RunCompile(templateString, Guid.NewGuid().ToString(), typeof(TEntity), entity);

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
                        var pageSize = new PageSize(setInfo.Width, setInfo.Height);
                        pdfDocument.SetDefaultPageSize(PageSize.A4.Rotate());
                        if (setInfo.Width != 0 || setInfo.Height != 0)
                        {
                            pdfDocument.SetDefaultPageSize(pageSize);
                        }

                        // 转换为文件流
                        var elements = HtmlConverter.ConvertToElements(htmlString, new ConverterProperties().SetFontProvider(fontProvider));
                        using var document = new iText.Layout.Document(pdfDocument);
                        foreach (var element in elements)
                        {
                            document.Add((IBlockElement)element);
                        }

                        // 设置 PDF 外边距
                        if (setInfo.Margin != null)
                        {
                            document.SetMargins(setInfo.Margin.Top, setInfo.Margin.Right, setInfo.Margin.Bottom, setInfo.Margin.Left);
                        }

                    }
                    // 将 PDF 数据写入字节数组并返回
                    return stream.ToArray();
                }
                //File.WriteAllBytes("output.pdf", stream.ToArray());
            }
            catch (Exception ex)
            {
                throw new Exception("PdfGenerationService_RenderPdf_Error:" + ex.Message);
            }

        }

        public byte[] RenderPdf<TEntity>(string templateAddress, IEnumerable<TEntity> entityList, PdfSetting setInfo = null)
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
                throw new Exception("PdfGenerationService_RenderPdf_Error:" + ex.Message);
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
            var templatePath = System.IO.Path.Combine(App.WebHostEnvironment.WebRootPath, "Template", templateAddress);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var templatePackage = new ExcelPackage(new FileInfo(templatePath)))
            {
                var asd = templatePackage.Workbook;
                // 获取模板中的工作表
                var templateWorksheet = templatePackage.Workbook.Worksheets[0];

                // 填入数据
                int row = 2;// 从第二行开始填充数据
                foreach (var data in entityList)
                {
                    var propertys = data?.GetType().GetProperties() ?? new PropertyInfo[] { };
                    foreach (var property in propertys)
                    {
                        var value = property.GetValue(data);
                        var index = Array.IndexOf(propertys, property) + 1;
                        templateWorksheet.Cells[row, index].Value = value;
                    }
                    row++;
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

        // /*文件返回需要添加表头*/
        //// 添加表头 返回名字 不然前端文件名字会加格式
        //var cd = new System.Net.Mime.ContentDisposition
        //{
        //    FileName = $"{listOutput[0].PartNumber}-Bag-{DateTime.Now:HHmmss}.pdf",
        //    Inline = false
        //};
        //App.HttpContext.Response.Headers.Add("Content-Disposition", cd.ToString());
        //    return new FileContentResult(bytes, "application/pdf");
    }
}
