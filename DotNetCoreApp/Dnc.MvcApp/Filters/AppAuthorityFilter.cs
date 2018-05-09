using Dnc.MvcApp.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Linq;

namespace Dnc.MvcApp.Filters
{
    /// <summary>
    /// 用于限制 Action 方法访问权限的过滤器
    /// </summary>
    public class AppAuthorityFilter: ResultFilterAttribute
    {
        private readonly string[] _Parameters;

        public AppAuthorityFilter(string[] parameter)
        {
            this._Parameters = parameter;
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            var userID = context.HttpContext.Session.GetString("LogonSystemUserID");
            if (String.IsNullOrEmpty(userID))
            {
                var routeDictionary = new RouteValueDictionary {
                    { "errorMeg","你访问的系统资源需要进行授权，你尚未登录系统，无法获取权限" },
                    { "action", "Error" },
                    { "controller", "Home" }
                };
                context.Result = new RedirectToRouteResult(routeDictionary);
            }
            else
            {
                var uID = Guid.Parse(userID);
                var userGroupName = DataAccessUtility.GetGroupNameByUserID(uID);
                if (!_Parameters.Contains(userGroupName))
                {
                    var routeDictionary = new RouteValueDictionary {
                    { "errorMeg","你访问的系统资源需要进行授权，你尚未获得授权，请联系相关人员询问。" },
                    { "action", "Error" },
                    { "controller", "Home" }};
                    context.Result = new RedirectToRouteResult(routeDictionary);
                }
            }
        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {
            base.OnResultExecuted(context);
        }
    }
}
