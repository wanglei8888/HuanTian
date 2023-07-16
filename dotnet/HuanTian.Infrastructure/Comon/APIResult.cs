using System.Net;

namespace HuanTian.Infrastructure
{
    /// <summary>
    /// 接口返回结果模板
    /// </summary>
    public class APIResult
    {
        public string? Message { get; set; }
        public long Timestamp { get; set; }
        public HttpStatusCode Code { get; set; }
        public object? Result { get; set; }


        public APIResult SetData(object obj)
        {
            this.Result = obj;
            return this;
        }
        public static APIResult Error(string msg = "")
        {
            return new APIResult() { Code = HttpStatusCode.OK, Message = msg };
        }
        public static APIResult Success(string msg = "")
        {
            return new APIResult() { Code = HttpStatusCode.InternalServerError, Message = msg };
        }
    }
}
