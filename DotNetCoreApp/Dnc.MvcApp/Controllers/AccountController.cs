using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Dnc.Entities.Application;
using Microsoft.AspNetCore.Http;
using Dnc.MvcApp.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Dnc.DataAccessRepository.Repositories;

namespace Dnc.MvcApp.Controllers
{
    /// <summary>
    /// 用户登录账号管理控制器
    /// </summary>
    public class AccountController : Controller
    {
        private readonly IEntityRepository _Service;  // EF 数据配置映射上下文

        public AccountController(IEntityRepository service)
        {
            _Service = service;
        }

        /// <summary>
        /// 导航到注册页面
        /// </summary>
        /// <param name="returnUrl">用于在前端处理成功后返回的 Url</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        /// <summary>
        /// 用户注册数据处理
        /// </summary>
        /// <param name="model">前端视图提供的 model 数据</param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                ApplicationUser user = null;
                await Task.Run(() =>
                {
                    user = _Service.GetSingleBy<ApplicationUser>(x => x.Name == model.UserName);
                });
                if (user != null)
                {
                    // 处理重复用户名问题
                    ModelState.AddModelError("逻辑错误", "你输入的用户名已经被别人注册了。");
                    return View("../../Views/Account/Register", model);
                }
                else
                {
                    user = new ApplicationUser();
                    var group = _Service.GetSingleBy<ApplicationGroup>(x => x.Name == "普通访客组");
                    user.Name = model.UserName;
                    user.Password = model.Password;
                    user.Group = group;
                    _Service.AddAndSave<ApplicationUser>(user);

                    return Redirect("../../Admin");
                }

            }
            return View("../../Views/Account/Register",model);
        }

        /// <summary>
        /// 用户登录验证
        /// </summary>
        /// <param name="model">与前端视图 Form 绑定的模型</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<LogonUserStatus> Login(LogonInformation model)
        {
            var logonStatus = new LogonUserStatus
            {
                IsLogon = false,
                Message = "用户名或密码错误。"
            };
            await Task.Run(() => {
                if (ModelState.IsValid)
                {
                    var user = _Service.GetSingleBy<ApplicationUser>(x => x.Name == model.UserName && x.Password == model.Password);
                    if (user != null)
                    {
                        // 处理登录的状态
                        logonStatus.IsLogon = true;
                        logonStatus.Message = "";
                        HttpContext.Session.SetString("LogonSystemUserID", user.ID.ToString());
                    }
                }
            });
            return logonStatus;
        }
    }
}
