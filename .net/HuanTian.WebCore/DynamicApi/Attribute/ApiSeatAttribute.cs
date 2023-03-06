using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuanTian.WebCore
{
    /// <summary>
    /// 接口参数位置设置
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public class ApiSeatAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="seat"></param>
        public ApiSeatAttribute(ApiSeats seat = ApiSeats.ActionEnd)
        {
            Seat = seat;
        }

        /// <summary>
        /// 参数位置
        /// </summary>
        public ApiSeats Seat { get; set; }
    }
    /// <summary>
    /// 接口参数位置
    /// </summary>
    public enum ApiSeats
    {
        /// <summary>
        /// 控制器之前
        /// </summary>
        [Description("控制器之前")]
        ControllerStart,

        /// <summary>
        /// 控制器之后
        /// </summary>
        [Description("控制器之后")]
        ControllerEnd,

        /// <summary>
        /// 行为之前
        /// </summary>
        [Description("行为之前")]
        ActionStart,

        /// <summary>
        /// 行为之后
        /// </summary>
        [Description("行为之后")]
        ActionEnd
    }

}
