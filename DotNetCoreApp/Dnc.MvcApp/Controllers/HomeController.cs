using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Linq.Expressions;
using Dnc.Entities.Application;
using Dnc.DataAccessRepository.Repositories;
using Dnc.Entities.Article;
using Dnc.ViewModels.Article;
using System.Collections.Generic;
using Dnc.MvcApp.Filters;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Dnc.MvcApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEntityRepository _Service;

        public HomeController(IEntityRepository service)
        {
            _Service = service;
        }

        //[Route("")]
        //[Route("首页")]
        //[Route("首页/Index")]
        //[HttpGet("{id}")]
        
        public async Task<IActionResult> Index( int id=1)
        {
            if (id==0) id = 1;
            var boVMCollection = new List<ArticleVM>();
            await Task.Run(() =>
            {
              
            
            //文章分类信息
            ViewBag.ArtiType = _Service.GetAll<ArticleType>().ToList();
            
            //文章标签显示
            ViewBag.Label = _Service.GetAll<Label>().ToList();

            
            //文章列表

            var data = _Service.GetAll<NewsArticle>().ToList();
           
            foreach (var item in data)
            {
                boVMCollection.Add(new ArticleVM(item));
            }
            });
            //分页
            var pageOption = new MoPagerOption
            {
                CurrentPage = id,
                PageSize = 2,
                Total =  boVMCollection.Count(),
                RouteUrl = ""
            };

            //分页参数
            ViewBag.PagerOption = pageOption;

            //数据
            return View( boVMCollection.OrderByDescending(b => b.PublishDateTime).Skip((pageOption.CurrentPage - 1) * pageOption.PageSize).Take(pageOption.PageSize).ToList());
        }

       
        public IActionResult About(string id)
        {
            var data = _Service.GetSingleBy<NewsArticle>(m => m.ID == id,m=>m.ArticleType);
            return View(data);
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "这是 联系方式 的内容。";

            return View();
        }

        public IActionResult Error(string errorMeg)
        {
            ViewBag.ErrorMessage = errorMeg;
            return View();
        }

    }
}