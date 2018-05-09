using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dnc.Entities.Organization;
using Dnc.Services.Utilities;
using Microsoft.AspNetCore.Cors;
using Dnc.Entities.Application;
using Newtonsoft.Json;

namespace Dnc.Services.Controllers
{
    [Route("api/[controller]")]
    public class PersonController : Controller
    {
        [HttpGet]
        [EnableCors("DncDemo")]
        public IEnumerable<Person> Get()
        {
            return BusinessDataAccess.Persons;
        }

        [HttpGet("{id}")]
        [EnableCors("DncDemo")]
        public Person Get(string id)
        {
            var persons = BusinessDataAccess.Persons;
            var person= persons.FirstOrDefault(x => x.ID == id);

            return person;
        }

        [HttpPost]
        [EnableCors("DncDemo")]
        public LogonUserStatus Post(string logonQueryJson)
        {
            var logonStatus = new LogonUserStatus
            {
                IsLogon = false,
                Message = "<span class='mif-warning'></span> 用户名或密码错误。"
            };
            // 将遵循 json 规格定义的对象数据字符串转换为C#对象
            var logonInfo = JsonConvert.DeserializeObject<LogonInformation>(logonQueryJson);
            logonStatus.IsLogon = true;
            logonStatus.Message = "登录成功。";
            return logonStatus;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
