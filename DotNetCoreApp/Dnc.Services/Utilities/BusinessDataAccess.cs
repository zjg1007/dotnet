using Dnc.Entities.Organization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dnc.Services.Utilities
{
    public static class BusinessDataAccess
    {
        public static List<Person> Persons=new List<Person>();

        public static void Set()
        {
            Persons.Add(new Person() { Name = "李玉华", Sex = false, BirthDay = DateTime.Parse("1990-9-19") });
            Persons.Add(new Person() { Name = "钟博琳", Sex = true, BirthDay = DateTime.Parse("1989-9-19") });
            Persons.Add(new Person() { Name = "黄小立", Sex = false, BirthDay = DateTime.Parse("1990-9-19") });
            Persons.Add(new Person() { Name = "王卫红", Sex = true, BirthDay = DateTime.Parse("1973-9-19") });
            Persons.Add(new Person() { Name = "张富贵", Sex = false, BirthDay = DateTime.Parse("1990-9-19") });
            Persons.Add(new Person() { Name = "陈东风", Sex = false, BirthDay = DateTime.Parse("1990-9-19") });
            Persons.Add(new Person() { Name = "黄复礼", Sex = false, BirthDay = DateTime.Parse("1988-9-19") });
        }
    }
}
