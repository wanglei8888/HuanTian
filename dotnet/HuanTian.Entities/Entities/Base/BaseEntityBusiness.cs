using Microsoft.EntityFrameworkCore;
using Yitter.IdGenerator;

namespace HuanTian.Entities
{
    /// <summary>
    /// 完整业务实体基类
    /// </summary>
    public class BaseEntityBusiness : BaseEntityUpdate
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        [Comment("租户ID")]
        public virtual long TenantId { get; set; }
        /// <summary>
        /// 实体新增自动赋值
        /// </summary>
        public override void CreateFunc()
        {
            Id = YitIdHelper.NextId();
            Deleted = false;
            CreateOn = DateTime.Now;
            CreateBy = App.GetUserId;
            TenantId = App.GetTenantId;
        }
        
    }
}
