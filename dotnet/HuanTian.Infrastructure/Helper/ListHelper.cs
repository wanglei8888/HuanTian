#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023 Microsoft NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：MS-SOBOTFLKMEBA
 * 公司名称：Microsoft
 * 命名空间：HuanTian.Common
 * 唯一标识：0204c040-4f3d-4855-90e7-1636a320cf75
 * 文件名：ListHelper
 * 当前用户域：MS-SOBOTFLKMEBA
 * 
 * 创建者：wanglei
 * 电子邮箱：
 * 创建时间：2023/3/15 17:29:16
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
using System.Collections;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace HuanTian.Infrastructure
{
    /// <summary>
    /// List集合帮助类
    /// </summary>
    public static class ListHelper
    {
        /// <summary>
        /// 将List转为Datable
        /// </summary>
        /// <param name="list">目标列表</param>
        /// <param name="Columns">自定义列名</param>
        /// <param name="isValFromCol">是否根据原来的列取值(针对列数和实际数据列的数量对不上的问题)</param>
        /// <returns></returns>
        public static DataTable ConvertToDataTable(this IList list, string[]? Columns = null, bool isValFromCol = false)
        {
            DataTable result = new DataTable();

            //填充表头
            if (Columns == null)
            {
                if (list.Count <= 0)
                {
                    return result;
                }
                PropertyInfo[] propertys = list[0]?.GetType().GetProperties() ?? new PropertyInfo[] { };
                foreach (PropertyInfo pi in propertys)
                {
                    //获取类型
                    Type colType = pi.PropertyType;
                    //当类型为Nullable<>时
                    if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                    {
                        colType = colType.GetGenericArguments()[0];
                    }
                    result.Columns.Add(pi.Name, colType);
                }
            }
            else
            {
                for (int i = 0; i < Columns.Length; i++)
                {
                    result.Columns.Add(Columns[i], string.Empty.GetType());
                }
            }

            //填充数据
            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    ArrayList tempList = new ArrayList();
                    if (Columns == null || isValFromCol)
                    {
                        PropertyInfo[] propertys = list[0]?.GetType().GetProperties() ?? new PropertyInfo[] { };
                        foreach (PropertyInfo pi in propertys)
                        {
                            object? obj = pi.GetValue(list[i], null);
                            tempList.Add(obj);

                        }
                    }
                    else
                    {
                        dynamic? search = list[i];
                        if (search != null)
                        {
                            foreach (var item in search)
                            {
                                tempList.Add(item);
                            }
                        }
                    }
                    object?[] array = tempList?.ToArray() ?? new object[] { };
                    result.LoadDataRow(array, true);
                }
            }

            return result;
        }
        /// <summary>
        /// DataTable转文件流
        /// </summary>
        /// <param name="dt">数据源DataTable</param>
        /// <param name="TableName">表格名字显示excel工作表名字</param>
        /// <returns></returns>
        public static ByteArrayOutputStream DatableToStream(DataTable dt, string tableName = "table1")
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

        /// <summary>
        /// 为集合里的所有实体赋值
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="list"></param>
        /// <param name="propertyExpression"></param>
        public static void SetValue<TEntity>(this IEnumerable<TEntity> list, Expression<Func<TEntity, bool>> propertyExpression)
        {
            foreach (var item in list)
            {
                // 切割lamboda表达式
                var conditions = new List<Expression>();
                if (propertyExpression.Body is BinaryExpression binaryExpression1)
                {
                    ExtractConditionsRecursively(binaryExpression1, conditions);
                }
                // 循环设置属性值
                foreach (var condition in conditions)
                {
                    if (condition is BinaryExpression binaryExpression)
                    {
                        if (binaryExpression.Left is MemberExpression memberExpression)
                        {
                            var propertyValue = Expression.Lambda(binaryExpression.Right).Compile().DynamicInvoke();
                            var propertyInfo = memberExpression.Member as PropertyInfo;
                            if (propertyInfo != null)
                            {
                                // Assuming 'this' refers to the current instance of the class
                                propertyInfo.SetValue(item, propertyValue);
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 切割lamboda表达式
        /// </summary>
        /// <param name="binaryExpression"></param>
        /// <param name="conditions"></param>
        private static void ExtractConditionsRecursively(BinaryExpression binaryExpression, List<Expression> conditions)
        {
            if (binaryExpression == null)
            {
                return;
            }
            if (binaryExpression.Left is MemberExpression memberExpression)
            {
                conditions.Add(binaryExpression);
            }

            if (binaryExpression.Left is BinaryExpression leftBinaryExpression)
            {
                ExtractConditionsRecursively(leftBinaryExpression, conditions);
            }

            if (binaryExpression.Right is BinaryExpression rightBinaryExpression)
            {
                ExtractConditionsRecursively(rightBinaryExpression, conditions);
            }
        }
    }
}