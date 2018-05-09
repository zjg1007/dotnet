using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dnc.MvcApp.ViewComponentModels.NavBar
{
    public class LeftNavBarViewModel
    {
        public List<LeftNavBarItemViewModel> NavBarItems { get; }

        public LeftNavBarViewModel(List<LeftNavBarItemViewModel> items)
        {
            this.NavBarItems = items;
        }
    }
}
