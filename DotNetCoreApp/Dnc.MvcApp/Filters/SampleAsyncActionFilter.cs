using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dnc.MvcApp.Filters
{
    public class SampleAsyncActionFilter: IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(
            ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            // 在 action 执行之前执行一些相关的功能

            await next();
            // 在 action 执行之后执行一些相关的功能
        }

    }
}
