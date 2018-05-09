using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dnc.DataAccessRepository.Repositories;
using Dnc.Entities.Article;
using Dnc.ViewModels.Article;

namespace Dnc.MvcApp.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IEntityRepository _DbService;

        public ArticleController(IEntityRepository service)
        {
            this._DbService = service;
        }

        public IActionResult Index()
        {
            var boCollection = _DbService.GetAll<NewsArticle>(x=>x.ArticleType).ToList();   // 提取所有的文章类型数据
            var boVMCollection = new List<ArticleVM>();
            foreach (var item in boCollection)
            {
                boVMCollection.Add(new ArticleVM(item));
            }
            return View();
        }

        public IActionResult Create()
        {
            var bo = new NewsArticle();
            var boVM = new ArticleVM(bo);
            return View();
        } 

        public IActionResult Edit(Guid id)
        {
            var bo = _DbService.GetSingle<NewsArticle>(id, x => x.ArticleType);
            var boVM = new ArticleVM(bo);
            return View(boVM);
        }
    }
}
