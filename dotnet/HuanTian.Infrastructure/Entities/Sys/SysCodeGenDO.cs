#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023  NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：DESKTOP-6BOHDBE
 * 公司名称：
 * 命名空间：HuanTian.Infrastructure.Entities
 * 唯一标识：0e454076-1544-43d2-8e92-26cdd4bdb409
 * 文件名：Class1
 * 当前用户域：DESKTOP-6BOHDBE
 * 
 * 创建者：wanglei
 * 电子邮箱：271976304@qq.com
 * 创建时间：2023/7/2 21:37:34
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
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HuanTian.Infrastructure
{
    /// <summary>
    /// 代码生成数据表
    /// </summary>
    [Comment("代码生成数据表")]
    public class SysCodeGenDO : BaseEntityCreate
    {
        /// <summary>
        /// 名称
        /// </summary>
        [MaxLength(30)]
        [Comment("名称")]
        public string Name { get; set; }
        /// <summary>
        /// 表格名字
        /// </summary>
        [MaxLength(50)]
        [Comment("表格名字")]
        public string TableName { get; set; }
        /// <summary>
        /// 所属菜单
        /// </summary>
        [Comment("所属菜单")]
        public long MenuId { get; set; }
        /// <summary>
        /// 生成方式
        /// </summary>
        [Comment("生成方式")]
        public GenerationWayEnum GenerationWay { get; set; }
    }
    /// <summary>
    /// 生成方式
    /// </summary>
    public enum GenerationWayEnum
    {
        /// <summary>
        /// 生成到项目
        /// </summary>
        GenToProj,
        /// <summary>
        /// 打包生成
        /// </summary>
        GenToPack

    }
}