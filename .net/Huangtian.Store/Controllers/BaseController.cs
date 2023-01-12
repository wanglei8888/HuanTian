using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using System.Text;
using HuanTian.Common;

namespace Huangtian.Store.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //byte[] result;
            //filterContext.HttpContext.Session.TryGetValue("UserID", out result);
            //if (result == null)
            //{
            //    filterContext.Result = new RedirectResult("/Login/Index");
            //    return;
            //}
            var myResult = (ObjectResult)filterContext.Result;
            if (myResult != null)
            {
                var resultModels = new ResultModels 
                {
                    Result = myResult.Value,
                    Status = 200,
                    Message = ""
                };
                filterContext.Result = Ok(resultModels);
            }
            
            Response.Headers.Add("Custom-Header", Guid.NewGuid().ToString());
            base.OnActionExecuted(filterContext);
        }
    }
}
