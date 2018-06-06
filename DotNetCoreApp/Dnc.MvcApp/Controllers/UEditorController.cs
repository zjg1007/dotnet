using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using UEditorNetCore;

namespace Dnc.MvcApp.Controllers
{
    [Route("api/[controller]")] //配置路由
    [EnableCors("Dnc")]
    public class UEditorController : Controller
    {
        private UEditorService ue;
        public UEditorController(UEditorService ue)
        {
            this.ue = ue;
        }
        public void Do()
        {
            ue.DoAction(HttpContext);
        }

    }
}