using Dnc.Entities.Application;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dnc.MvcApp.ViewComponents.UserLogon
{
    [ViewComponent(Name = "Logon")]
    public class LogonComponent: ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var viewModel = new LogonInformation();
            await Task.Run(() => {
                // 做一些初始化工作
            });

            return View(viewModel);
        }
    }
}
