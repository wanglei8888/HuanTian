#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023  NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：DESKTOP-6BOHDBE
 * 公司名称：
 * 命名空间：HuanTian.Entities.Entities
 * 唯一标识：ff7f56fd-f84d-4aa0-9d6e-5ecc5cf67e77
 * 文件名：SysCodeGenDO
 * 当前用户域：DESKTOP-6BOHDBE
 * 
 * 创建者：wanglei
 * 电子邮箱：271976304@qq.com
 * 创建时间：2023/5/26 23:02:04
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

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


namespace HuanTian.Entities
{
    /// <summary>
    /// 代码生成数据表格
    /// </summary>
    public class SysCodeGenDO : BaseEntityCreate
    {
        /// <summary>
        /// 表格名字
        /// </summary>
        [MaxLength(50)]
        [Comment("表格名字")]
        public string TableName { get; set; }
        /// <summary>
        /// 列类型
        /// </summary>
        [MaxLength(30)]
        [Comment("列类型")]
        public string DataType { get; set; }
        /// <summary>
        /// 列名字
        /// </summary>
        [MaxLength(30)]
        [Comment("列名字")]
        public string DbColumnName { get; set; }
        /// <summary>
        /// 列备注
        /// </summary>
        [MaxLength(30)]
        [Comment("列备注")]
        public string ColumnDescription { get; set; }
        /// <summary>
        /// 下拉框绑定字典值 如果没有就是不是下拉框
        /// </summary>
        [MaxLength(30)]
        [Comment("下拉框绑定字典值 如果没有就是不是下拉框")]
        public string? DropDownList { get; set; }
        /// <summary>
        /// 是否是查询参数
        /// </summary>
        [Comment("是否是查询参数")]
        public bool QueryParameters { get; set; }
    }
}