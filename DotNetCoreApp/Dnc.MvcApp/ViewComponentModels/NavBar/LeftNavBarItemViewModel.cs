using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dnc.MvcApp.ViewComponentModels.NavBar
{
    public class LeftNavBarItemViewModel
    {
        public string Name { get; set; }
        public string Url { get; set; }

        public LeftNavBarItemViewModel(string name,string url)
        {
            Name = name;
            Url = url;
        }
    }
}
