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
using System.Collections;
using System.Data;
using System.Reflection;

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
    }
}