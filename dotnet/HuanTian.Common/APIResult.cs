﻿namespace HuanTian.Common
{
    /// <summary>
    /// 接口返回结果模板
    /// </summary>
    public class APIResult
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public long Timestamp { get; set; }
        public ResultCodeEnum Code { get; set; }
        public object Result { get; set; }


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
