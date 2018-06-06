using Dnc.Entities.Article;
using Dnc.ViewModels.Article;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dnc.MvcApp.ViewInjections
{
    public class LayerPage
    {
        public int code { get; set; }
        public string msg { get; set; }
        public int count { get; set; }
        public List<ArticleVM> data { get; set; }
    }
}
