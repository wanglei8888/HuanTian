using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace HuanTian.Domain.Base
{
    public class BaseCreateUpdateInfo
    {
        /// <summary>
        /// 创建人
        /// </summary>
        [MaxLength(50)]
        [Description("创建人")]
        public string? Creater { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Description("创建时间")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        [MaxLength(50)]
        [Description("修改人")]
        public string? Updater { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        [Description("修改时间")]
        public DateTime UpdateTime { get; set; }
    }
}
