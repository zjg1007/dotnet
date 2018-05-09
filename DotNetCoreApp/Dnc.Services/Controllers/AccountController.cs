using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dnc.DataAccessRepository.Repositories;
using Dnc.Entities.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Dnc.Services.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IEntityRepository _Service;  // EF 数据配置映射上下文

        public AccountController(IEntityRepository service)
        {
            _Service = service;
        }

        [HttpPost]
        [AllowAnonymous]
        public LogonUserStatus Post([FromBody]LogonInformation model)
        {
            var logonStatus = new LogonUserStatus
            {
                IsLogon = false,
                Message = "用户名或密码错误。"
            };
            var user = _Service.GetSingleBy<ApplicationUser>(x => x.Name == model.UserName && x.Password == model.Password);
            if (user != null)
            {
                // 处理登录的状态
                logonStatus.IsLogon = true;
                logonStatus.Message = "";
                HttpContext.Session.SetString("LogonSystemUserID", user.ID.ToString());
            }
            return logonStatus;
        }

    }
}
