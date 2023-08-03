using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using Yitter.IdGenerator;

namespace HuanTian.Infrastructure
{
    /// <summary>
    /// 创建者基类
    /// </summary>
    public class BaseEntityCreate : BaseEntity
    {
        /// <summary>
        /// 创建人
        /// </summary>
        [Comment("创建人")]
        public virtual long? CreateBy { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Comment("创建时间")]
        public virtual DateTime? CreateOn { get; set; }
        /// <summary>
        /// 实体新增自动赋值
        /// </summary>
        public override void CreateFunc()
        {
            Id = YitIdHelper.NextId();
            Deleted = false;
            CreateOn = DateTime.Now;
            CreateBy = App.GetUserId();
        }
    }
}
