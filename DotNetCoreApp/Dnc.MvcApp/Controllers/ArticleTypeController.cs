using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dnc.Entities.Article;
using Dnc.MvcApp.Filters;
using Dnc.DataAccessRepository.Repositories;
using Dnc.ViewModels.Article;

namespace Dnc.MvcApp.Controllers
{
    /// <summary>
    /// 文章类型管理控制器
    /// </summary>
    public class ArticleTypeController : Controller
    {
        private readonly IEntityRepository _DbService;

        public ArticleTypeController(IEntityRepository service)
        {
            this._DbService = service;
        }

        [HttpGet]
        [AppAuthorityFilter(new string[] { "授权用户组", "系统管理员组"})]
        public IActionResult Index()
        {
            var boCollection = _DbService.GetAll<ArticleType>().ToList();   // 提取所有的文章类型数据
            var boVMCollection = new List<ArticleTypeVM>();
            foreach (var item in boCollection)
            {
                boVMCollection.Add(new ArticleTypeVM(item));
            }

            return View(boVMCollection);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var boVM = new ArticleTypeVM(new ArticleType());
            return View(boVM);
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var bo = _DbService.GetSingle<ArticleType>(id);
            var boVM = new ArticleTypeVM(bo);
            return View(boVM);
        }

        [HttpPost]
        public async Task<IActionResult> Save(ArticleTypeVM boVM)
        {
            var isOK = false;
            await Task.Run(() => {
                if (ModelState.IsValid)
                {
                    // 检查是否已经存在 ArticleType 实例
                    var id = Guid.Parse(boVM.ID);
                    var bo = _DbService.GetSingle<ArticleType>(id);
                    if (bo == null)
                    {
                        bo = new ArticleType();
                        boVM.MapBo(bo);
                        _DbService.AddAndSave(bo);
                    }
                    else
                    {
                        boVM.MapBo(bo);
                        _DbService.EditAndSave(bo);
                    }

                    isOK = true;
                }
            });
            if (isOK)
                return RedirectToAction("Index");
            else
                return View();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var bo = _DbService.GetSingle<ArticleType>(id);
            await Task.Run(() => {
                _DbService.DeleteAndSave(bo);
            });
            return View();
        }
    }
}
