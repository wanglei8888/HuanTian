#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2023  NJRN 保留所有权利。
 * CLR版本：4.0.30319.42000
 * 机器名称：DESKTOP-6BOHDBE
 * 公司名称：
 * 命名空间：HuanTian.Infrastructure.Entities.Base
 * 唯一标识：564d99d1-7495-42f0-a87d-4a9f969d436b
 * 文件名：BaseEntityUpdate
 * 当前用户域：DESKTOP-6BOHDBE
 * 
 * 创建者：wanglei
 * 电子邮箱：271976304@qq.com
 * 创建时间：2023/5/11 21:29:26
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
using Yitter.IdGenerator;

namespace HuanTian.Infrastructure
{
    /// <summary>
    /// 修改基类
    /// </summary>
    public class BaseEntityUpdate : BaseEntityCreate
    {
        /// <summary>
        /// 修改人
        /// </summary>
        [Comment("修改人")]
        public virtual long? UpdateBy { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        [Comment("修改时间")]
        public virtual DateTime? UpdateOn { get; set; }
        /// <summary>
        /// 实体新增自动赋值
        /// </summary>
        public override void CreateFunc()
        {
            Id = YitIdHelper.NextId();
            CreateBy = App.GetUserId();
            CreateOn = DateTime.Now;
        }
        /// <summary>
        /// 实体修改自动赋值
        /// </summary>
        public virtual void UpdateFunc()
        {
            UpdateOn = DateTime.Now;
            UpdateBy = App.GetUserId();
        }
    }
}