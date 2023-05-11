using HuanTian.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Yitter.IdGenerator;

namespace HuanTian.Entities
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
        public virtual long CreateBy { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Comment("创建时间")]
        public virtual DateTime CreateOn { get; set; }
        /// <summary>
        /// 实体新增自动赋值
        /// </summary>
        public override void CreateFunc()
        {
            Id = YitIdHelper.NextId();
            CreateOn = DateTime.Now;
            CreateBy = App.GetUserId;
        }
    }
}
