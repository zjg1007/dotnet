using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dnc.MvcApp.ViewComponentModels.NavBar;
using System.Threading;
using Dnc.DataAccessRepository.Context;

namespace Dnc.MvcApp.ViewComponents.NavBar
{
    /// <summary>
    /// 用于在管理员后台根据主菜单的选择项，自动生成左侧操作导航菜单
    /// </summary>
    [ViewComponent(Name = "LeftNavBar")] // 组件签名，供前端调用时指定
    public class LeftNavBarComponent:ViewComponent
    {
        private EntityDbContext _DbContext;

        public LeftNavBarComponent(EntityDbContext context)
        {
            _DbContext = context;
        }

        /// <summary>
        /// 供前端调用的方法
        /// </summary>
        /// <returns></returns>
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var navBarItems = new List<LeftNavBarItemViewModel>();
            await Task.Run(()=> {
                navBarItems.Add(new LeftNavBarItemViewModel("测试01", ""));
                navBarItems.Add(new LeftNavBarItemViewModel("测试02", ""));
                navBarItems.Add(new LeftNavBarItemViewModel("测试03", ""));
                navBarItems.Add(new LeftNavBarItemViewModel("测试04", ""));
            });

            var viewModel = new LeftNavBarViewModel(navBarItems);

            return View(viewModel); //将模型返回到前端视图
        }

    }
}
