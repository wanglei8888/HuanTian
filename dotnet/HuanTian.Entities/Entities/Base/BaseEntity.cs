using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Yitter.IdGenerator;

namespace HuanTian.Entities
{
    /// <summary>
    /// 表格基类
    /// </summary>
    public class BaseEntity
    {
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// 软删除(是否已经删除)
        /// </summary>
        [Comment("软删除")]
        public virtual bool Deleted { get; set; } = false;
        /// <summary>
        /// 实体新增自动赋值
        /// </summary>
        public virtual void CreateFunc()
        {
            Id = YitIdHelper.NextId();
            Deleted = false;
        }
    }
}
