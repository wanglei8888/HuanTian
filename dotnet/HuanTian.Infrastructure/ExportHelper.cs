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
using NPOI.Util;
using NPOI.XSSF.UserModel;
using System.Data;
using System.Text;

namespace HuanTian.Infrastructure
{
    /// <summary>
    /// 导出帮助类
    /// </summary>
    public static class ExportHelper
    {

        /// <summary>
        /// DataTable转文件流
        /// </summary>
        /// <param name="dt">数据源DataTable</param>
        /// <param name="TableName">表格名字显示excel工作表名字</param>
        /// <returns></returns>
        public static ByteArrayOutputStream DatableToStream(DataTable dt,string tableName ="table1")
        {
            XSSFWorkbook workbook = new XSSFWorkbook();
            string sheetName = !string.IsNullOrWhiteSpace(dt.TableName) ? dt.TableName : tableName;
            XSSFSheet sheet = workbook.CreateSheet(sheetName) as XSSFSheet;

            XSSFCellStyle dateStyle = workbook.CreateCellStyle() as XSSFCellStyle;
            XSSFDataFormat format = workbook.CreateDataFormat() as XSSFDataFormat;
            dateStyle.DataFormat = format.GetFormat("yyyy-mm-dd");

            //取得列宽
            int[] arrColWidth = new int[dt.Columns.Count];
            foreach (DataColumn item in dt.Columns)
            {
                arrColWidth[item.Ordinal] = Encoding.GetEncoding(936).GetBytes(item.ColumnName.ToString()).Length;
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {   
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    int intTemp = Encoding.GetEncoding(936).GetBytes(dt.Rows[i][j].ToString()).Length;
                    if (intTemp > arrColWidth[j])
                    {
                        arrColWidth[j] = intTemp;
                    }
                }
            }
            int rowIndex = 0;

            #region 新建表，填充列头，样式

            XSSFRow headerRow = sheet.CreateRow(0) as XSSFRow;
            XSSFCellStyle headStyle = workbook.CreateCellStyle() as XSSFCellStyle;
            XSSFFont font = workbook.CreateFont() as XSSFFont;
            font.FontHeightInPoints = 10;
            font.Boldweight = 700;
            headStyle.SetFont(font);
            foreach (DataColumn column in dt.Columns)
            {
                int colWidth = (arrColWidth[column.Ordinal] + 1) * 256;

                headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                headerRow.GetCell(column.Ordinal).CellStyle = headStyle;

                //设置列宽
                if (colWidth < 255 * 256)
                {
                    sheet.SetColumnWidth(column.Ordinal, colWidth);
                }
                else
                {
                    sheet.SetColumnWidth(column.Ordinal, 6000);
                }
            }
            rowIndex = 1;

            #endregion

            foreach (DataRow row in dt.Rows)
            {
                //行数超过65535之后，新建工作簿
                if (rowIndex == 65535)
                {
                    sheet = workbook.CreateSheet(sheetName) as XSSFSheet;
                }

                #region 填充内容5

                XSSFRow dataRow = sheet.CreateRow(rowIndex) as XSSFRow;
                foreach (DataColumn column in dt.Columns)
                {
                    XSSFCell newCell = dataRow.CreateCell(column.Ordinal) as XSSFCell;

                    string drValue = row[column].ToString();

                    switch (column.DataType.ToString())
                    {
                        case "System.String"://字符串类型
                            newCell.SetCellValue(drValue);
                            break;
                        case "System.DateTime"://日期类型
                            if (!string.IsNullOrEmpty(drValue))
                            {
                                drValue = (Convert.ToDateTime(drValue)).ToString("yyyy-MM-dd");
                            }
                            newCell.SetCellValue(drValue);
                            break;
                        case "System.Object":
                            newCell.SetCellValue(drValue.ToString());
                            break;
                        case "System.Boolean"://布尔型
                            bool boolV = false;
                            bool.TryParse(drValue, out boolV);
                            newCell.SetCellValue(boolV);
                            break;
                        case "System.Int16"://整型
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Byte":
                            int intV = 0;
                            int.TryParse(drValue, out intV);
                            newCell.SetCellValue(intV);
                            break;
                        case "System.Decimal"://浮点型
                        case "System.Double":
                            double doubV = 0;
                            double.TryParse(drValue, out doubV);
                            newCell.SetCellValue(doubV);
                            break;
                        case "System.DBNull"://空值处理
                            newCell.SetCellValue("");
                            break;
                        default:
                            newCell.SetCellValue("");
                            break;
                    }
                }

                #endregion

                rowIndex++;
            }
            ByteArrayOutputStream stream = new ByteArrayOutputStream();
            workbook.Write(stream);
            return stream;
        }

//         if (steam != null) 导出代码
//            {
//                byte[] buffer = steam.ToByteArray();
//                var outUrl = Path.Combine("E:\\Code", "区县导出temp.xlsx"); ;
//                using (FileStream filestream = new FileStream(outUrl, FileMode.Create))
//                {
//                    byte[] bt = new byte[filestream.Length];
//                    filestream.Read(bt, 0, bt.Length);
//                    await filestream.WriteAsync(buffer);
//    }
//}
    }
}