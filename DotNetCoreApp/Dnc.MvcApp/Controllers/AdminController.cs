using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Dnc.MvcApp.Filters;
using Dnc.DataAccessRepository.Repositories;
using Dnc.Entities.Article;
using Dnc.ViewModels.Article;
using Dnc.Entities.Application;
using Dnc.MvcApp.ViewInjections;

namespace Dnc.MvcApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly IEntityRepository _context;

        public AdminController(IEntityRepository context)
        {
            _context = context;
        }

        [HttpGet]
        //[AppAuthorityFilter(new string[] { "授权用户组", "系统管理员组" })]
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult EditorArticle(NewsArticle na) {
            
            return View();
        }
        /// <summary>
        /// 后台（文章发布）
        /// </summary>
        /// <returns></returns>
        public IActionResult form1()
        {
            //文章类别
            ViewBag.TypeList = _context.GetAll<ArticleType>().Distinct(p => p.Name).ToList();
            //文章标签
            ViewBag.LabelList = _context.GetAll<Label>().Distinct(p=>p.Name).ToList();
            return View();
        }
        [HttpPost]
        public IActionResult form1(ArticleVM na,string htmlInfo)
        {
            var logonStatus = new LogonUserStatus
            {
                IsLogon = false,
                Message = "用户名或密码错误。"
            };
            if (ModelState.IsValid)
            {
                var data = _context.GetSingleBy<ArticleType>(m => m.ID == na.ArticleTypeName);
                _context.AddAndSave<NewsArticle>(new NewsArticle
                {
                    Name = na.Name,
                    Content = htmlInfo,
                    SubName = na.SubName,
                    TitleImage = "../../images/newsDemo.jpg",
                    VisitiSum = 0,
                    Label = na.Label,
                    ArticleType = data
                });
                logonStatus.IsLogon = true;
                logonStatus.Message = "发布成功！";
            }
            else {
                logonStatus.IsLogon = false;
                logonStatus.Message = "发布失败！";
            }
           
            return Json(logonStatus);
        }
        public IActionResult main1()
        {

            return View();
        }
        public IActionResult computer()
        {

            return View();
        }
        public IActionResult Default()
        {

            return View();
        }
        public IActionResult error()
        {

            return View();
        }
        public IActionResult filelist()
        {

            return View();
        }
        public IActionResult imglist()
        {

            return View();
        }
        public IActionResult imglist1()
        {

            return View();
        }
        public IActionResult imgtable()
        {

            return View();
        }
        public IActionResult index()
        {

            return View();
        }
        public IActionResult left()
        {

            return View();
        }
        public IActionResult login()
        {

            return View();
        }
        /// <summary>
        /// 后台（文章数据列表管理）
        /// </summary>
        /// <returns></returns>
        public IActionResult right()
        {
           
            return View();
        }
        /// <summary>
        /// 获取文章列表信息（分页）
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public async Task< IActionResult> rightData(int page = 1,int limit=10)
        {
            LayerPage lp = new LayerPage();
            List<ArticleVM> avm = new List<ArticleVM>();
            await Task.Run(() =>
            {
                lp.code = 0;
                lp.msg = "";
                var data = _context.GetAll<NewsArticle>(m => m.ArticleType).ToList();
                lp.count = data.Count();
                foreach (var item in data)
                {
                    avm.Add(new ArticleVM(item));
                }
                avm.OrderByDescending(b => b.PublishDateTime).Skip((lp.code - 1) * limit).Take(limit).ToList();
                lp.data = avm;
            });
            return Json(lp);
        }
        public IActionResult tab()
        {

            return View();
        }
        public IActionResult tools()
        {

            return View();
        }
        public IActionResult top()
        {

            return View();
        }
        /// <summary>
        /// 后台（文章删除）
        /// </summary>
        /// <param name="id">文章id String </param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult del(string id)
        {
            var logonStatus = new LogonUserStatus
            {
                IsLogon = false,
                Message = "删除失败。"
            };
            try
            {
                var data = _context.GetSingleBy<NewsArticle>(m => m.ID == id);
                _context.DeleteAndSave<NewsArticle>(data);
                logonStatus.IsLogon = true;
                logonStatus.Message = "删除成功";
            }
            catch (Exception)
            {

                logonStatus.IsLogon = false;
                logonStatus.Message = "删除失败";
            }
            
            return Json(logonStatus);
        }
        /// <summary>
        /// 文章内容编辑
        /// </summary>
        /// <param name="bodyInfo"></param>
        /// <returns></returns>
        public IActionResult Edit(string id)
        {
            //文章类别
            ViewBag.TypeList = _context.GetAll<ArticleType>().Distinct(p => p.Name).ToList();
            //文章标签
            ViewBag.LabelList = _context.GetAll<Label>().Distinct(p => p.Name).ToList();
           var data = _context.GetSingleBy<NewsArticle>(m => m.ID == id);
            ArticleVM avm = new ArticleVM(data);
            return View(avm);
        }
        [HttpPost]
        public IActionResult Edit(ArticleVM bodyInfo,string htmlInfo)
        {
            var logonStatus = new LogonUserStatus
            {
                IsLogon = false,
                Message = "编辑失败！"
            };
            try
            {
                var data = _context.GetSingleBy<NewsArticle>(m => m.ID == bodyInfo.ID);
               var type = _context.GetSingleBy<ArticleType>(m => m.ID == bodyInfo.ArticleTypeName);
                bodyInfo.MapBo(data);
                data.Content = htmlInfo;
                data.ArticleType = type;
                data.PublishDateTime = DateTime.Now;
                _context.EditAndSave<NewsArticle>(data);
                logonStatus.IsLogon = true;
                logonStatus.Message = "编辑成功";
            }
            catch (Exception)
            {
                logonStatus.IsLogon = true;
                logonStatus.Message = "编辑失败";

            }
            return Json(logonStatus);
        }
        /// <summary>
        /// 查看文章
        /// </summary>
        /// <returns></returns>
        public IActionResult Select(string id)
        {
            //文章类别
            ViewBag.TypeList = _context.GetAll<ArticleType>().Distinct(p => p.Name).ToList();
            //文章标签
            ViewBag.LabelList = _context.GetAll<Label>().Distinct(p => p.Name).ToList();
            var data = _context.GetSingleBy<NewsArticle>(m => m.ID == id);
            ArticleVM avm = new ArticleVM(data);
            return View(avm); 
        }
    }
}
