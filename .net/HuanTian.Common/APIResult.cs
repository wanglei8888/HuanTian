using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuanTian.Common
{
    public class APIResult
    {
        public string Message { get; set; }
        public object Result { get; set; }
        public bool Status { get; set; }
        public ResultCodeEnum Code { get; set; }

        public APIResult SetData(object obj)
        {
            this.Result = obj;
            return this;
        }
        public static APIResult Error(string msg = "")
        {
            return new APIResult() { Code = ResultCodeEnum.NotSuccess, Status = false, Message = msg };
        }
        public static APIResult Success(string msg = "")
        {
            return new APIResult() { Code = ResultCodeEnum.Success, Status = true, Message = msg };
        }
    }
    public enum ResultCodeEnum
    {
        /// <summary>
        /// 操作成功。
        /// </summary>
        Success = 200,

        /// <summary>
        /// 操作不成功
        /// </summary>
        NotSuccess = 500,

        /// <summary>
        /// 无权限
        /// </summary>
        NoPermission = 401,

        /// <summary>
        ///  Access过期
        /// </summary>
        AccessTokenExpire = 1001,

        /// <summary>
        /// Refresh过期
        /// </summary>
        RefreshTokenExpire = 1002,

        /// <summary>
        /// 没有角色登录
        /// </summary>
        NoRoleLogin = 1003,
    }
}
