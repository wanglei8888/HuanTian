using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using System.Text;

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
            Response.Headers.Add("Custom-Header", Guid.NewGuid().ToString());
            base.OnActionExecuted(filterContext);
        }
    }
}
