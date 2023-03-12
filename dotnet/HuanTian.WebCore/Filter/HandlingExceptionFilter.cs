using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuanTian.WebCore
{
    /// <summary>
    /// 异常处理
    /// </summary>
    public class HandlingExceptionFilter : IAsyncExceptionFilter
    {
        public HandlingExceptionFilter() 
        {
            
        }
        public async Task OnExceptionAsync(ExceptionContext context)
        {

            
            //throw new NotImplementedException();
        }
    }
}
