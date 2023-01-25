using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using System.Text;
using HuanTian.Common;
using NPOI.SS.Formula.Functions;

namespace Huangtian.Store.Controllers
{
    public class BaseController : Controller
    {
        /// <summary>
        /// 对所有API返回的结果截取
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var myResult = (ObjectResult)filterContext.Result;
            // 返回的类型不是APIResult就对结果进行修改
            if (myResult != null && ((APIResult)myResult.Value) == null)
            {

                var resultModels = new APIResult
                {
                    Result = myResult.Value ?? "",
                    Status = true,
                    Message = "",
                    Code = ResultCodeEnum.Success
                };
                filterContext.Result = Ok(resultModels);
            }

            Response.Headers.Add("Custom-Header", Guid.NewGuid().ToString());
            base.OnActionExecuted(filterContext);
        }

        //byte[] result;
        //filterContext.HttpContext.Session.TryGetValue("UserID", out result);
        //if (result == null)
        //{
        //    filterContext.Result = new RedirectResult("/Login/Index");
        //    return;
        //}
    }
}
