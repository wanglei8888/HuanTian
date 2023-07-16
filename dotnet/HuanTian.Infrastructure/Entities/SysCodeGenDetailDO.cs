#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023  NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：DESKTOP-6BOHDBE
 * 公司名称：
 * 命名空间：HuanTian.Infrastructure.Entities
 * 唯一标识：ff7f56fd-f84d-4aa0-9d6e-5ecc5cf67e77
 * 文件名：SysCodeGenDetailDO
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


namespace HuanTian.Infrastructure
{
    /// <summary>
    /// 代码生成数据详情表
    /// </summary>
    [Comment("代码生成数据详情表")]
    public class SysCodeGenDetailDO : BaseEntityCreate
    {
        /// <summary>
        /// 主表ID
        /// </summary>
        [Comment("主表ID")]
        public long MasterId { get; set; }
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
        /// 列类型
        /// </summary>
        [MaxLength(30)]
        [Comment("列类型")]
        public string DataType { get; set; }
        /// <summary>
        /// 前端显示类型
        /// </summary>
        [MaxLength(30)]
        [Comment("前端显示类型")]
        public string FrontendType { get; set; }
        /// <summary>
        /// 下拉框绑定字典值 如果没有就是不是下拉框
        /// </summary>
        [MaxLength(30)]
        [Comment("下拉框绑定字典值 如果没有就是不是下拉框")]
        public string? DropDownCode { get; set; }
        /// <summary>
        /// 查询方式
        /// </summary>
        [MaxLength(30)]
        [Comment("查询方式")]
        public string? QueryType { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [Comment("排序")]
        public int Order { get; set; }
        /// <summary>
        /// 是否是查询参数
        /// </summary>
        [Comment("是否是查询参数")]
        public bool QueryParameters { get; set; }
        /// <summary>
        /// 是否必填
        /// </summary>
        [Comment("是否必填")]
        public bool Required { get; set; }
        
    }
}