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
using Newtonsoft.Json;

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
        /// <summary>
        /// 标签管理页面显示
        /// </summary>
        /// <returns></returns>
        public IActionResult imglist()
        {

            return View();
        }
        /// <summary>
        /// 获取标签所有信息-API
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public async Task<IActionResult> labelInfo(int page = 1, int limit = 10)
        {
            LayerPage<Label> lp = new LayerPage<Label>();
            string strName = Request.Query["key[name]"]+"";
            await Task.Run(() =>
            {
                lp.code = 0;
                lp.msg = "";
                var data = _context.GetAll<Label>().Where(m => m.Name.Contains(strName)).ToList();
                lp.count = data.Count();
                data.OrderByDescending(b => b.Name).Skip((lp.code - 1) * limit).Take(limit).ToList();
                lp.data = data;
            });
            
            return Json(lp);
        }
        /// <summary>
        /// 标签管理-编辑信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult labelEdit()
        {
            return View();
        }
        /// <summary>
        /// 标签管理-插入标签
        /// </summary>
        /// <param name="lb"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult insertLabel(Label lb)
        {
            var logonStatus = new LogonUserStatus
            {
                IsLogon = false,
                Message = "添加失败！"
            };
            try
            {
                Label and = new Label();
                and.Name = lb.Name;
                _context.AddAndSave<Label>(and);
                logonStatus.IsLogon = true;
                logonStatus.Message = "添加成功";
            }
            catch (Exception)
            {
                logonStatus.IsLogon = false;
                logonStatus.Message = "添加失败";
            }
            return Json(logonStatus);
        }
        /// <summary>
        /// 标签管理-提交编辑
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult labelEdit(LabelVM model)
        {
            var logonStatus = new LogonUserStatus
            {
                IsLogon = false,
                Message = "编辑失败！"
            };
            try
            {
                var data = _context.GetSingleBy<Label>(m => m.ID == model.ID);
                model.MapBo(data);
                _context.EditAndSave<Label>(data);
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
        /// 标签管理-删除信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult labelDel(String id,[FromForm]string batchid)
        {
            var logonStatus = new LogonUserStatus
            {
                IsLogon = false,
                Message = "删除失败。"
            };
            try
            {
                if (string.IsNullOrEmpty(batchid) && id!=null) {
                    var data = _context.GetSingleBy<Label>(m => m.ID == id);
                    _context.DeleteAndSave<Label>(data);
                }
                else
                {
                    // 将遵循 json 规格定义的对象数据字符串转换为C#对象
                    var labelInfo = JsonConvert.DeserializeObject<List<Label>>(batchid);
                    foreach (var item in labelInfo)
                    {
                       var data = _context.GetSingleBy<Label>(m => m.ID == item.ID); 
                        _context.Delete(data);
                    }
                    _context.Save();
                }
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
        public IActionResult imglist1()
        {

            return View();
        }
        /// <summary>
        /// 分类管理页面
        /// </summary>
        /// <returns></returns>
        public IActionResult imgtable()
        {

            return View();
        }
        /// <summary>
        /// 分类管理（数据分页）
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> typeInfo(int page = 1, int limit = 10)
        {
            string strTrpe = Request.Query["key[name]"] + "";
            LayerPage<ArticleType> lp = new LayerPage<ArticleType>();
            await Task.Run(() =>
            {
                lp.code = 0;
                lp.msg = "";
                var data = _context.GetAll<ArticleType>().Where(m=>m.Name.Contains(strTrpe)).ToList();
                lp.count = data.Count();
                data.OrderByDescending(b => b.Name).Skip((lp.code - 1) * limit).Take(limit).ToList();
                lp.data = data;
            });
            return Json(lp);
        }
        /// <summary>
        /// 类别管理（新增信息）
        /// </summary>
        /// <param name="id">类别ID</param>
        /// <returns></returns>
        public IActionResult typeEdit()
        {
            return View();
        }
        /// <summary>
        /// 类别管理-添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult typeInsert(ArticleType model)
        {
            var logonStatus = new LogonUserStatus
            {
                IsLogon = false,
                Message = "添加失败！"
            };
            try
            {
                ArticleType file = new ArticleType();
                file = model;
                _context.AddAndSave<ArticleType>(file);
                logonStatus.IsLogon = true;
                logonStatus.Message = "添加成功！";
            }
            catch (Exception)
            {
                logonStatus.IsLogon = false;
                logonStatus.Message = "添加失败！";
            }
           
            return Json(logonStatus);
        }
        [HttpPost]
        public IActionResult typeEdit(ArticleTypeVM model)
        {
            var logonStatus = new LogonUserStatus
            {
                IsLogon = false,
                Message = "编辑失败！"
            };
            try
            {
                var data = _context.GetSingleBy<ArticleType>(m => m.ID == model.ID);
                model.MapBo(data);
                _context.EditAndSave<ArticleType>(data);
                logonStatus.IsLogon = true;
                logonStatus.Message = "编辑成功";
            }
            catch (Exception)
            {
                logonStatus.IsLogon = false;
                logonStatus.Message = "编辑失败";
            }
            
            return Json(logonStatus);
        }
        /// <summary>
        /// 文章类别管理（删除信息）
        /// </summary>
        /// <param name="id">类别ID</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult typeDel(String id,[FromForm] string batchid)
        {
            var logonStatus = new LogonUserStatus
            {
                IsLogon = false,
                Message = "删除失败！"
            };
            try
            {
                if (string.IsNullOrEmpty(batchid) && id != null)
                {
                    var data = _context.GetSingleBy<ArticleType>(m => m.ID == id);
                    _context.DeleteAndSave<ArticleType>(data);
                }
                else
                {
                    // 将遵循 json 规格定义的对象数据字符串转换为C#对象
                    var labelInfo = JsonConvert.DeserializeObject<List<ArticleType>>(batchid);
                    foreach (var item in labelInfo)
                    {
                        var data = _context.GetSingleBy<ArticleType>(m => m.ID == item.ID);
                        _context.Delete(data);
                    }
                    _context.Save();
                }
               
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
        public async Task<IActionResult> rightData(int page = 1,int limit=10)
        {
            string str = Request.Query["key[name]"] + "";
            LayerPage<ArticleVM> lp = new LayerPage<ArticleVM>();
            List<ArticleVM> avm = new List<ArticleVM>();
            await Task.Run(() =>
            {
                lp.code = 0;
                lp.msg = "";
                var data = _context.GetAll<NewsArticle>(m => m.ArticleType).Where(m=>m.Name.Contains(str)).ToList();
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
        public IActionResult del(string id,[FromForm] string batchid)
        {
            var logonStatus = new LogonUserStatus
            {
                IsLogon = false,
                Message = "删除失败。"
            };
            try
            {
                if (string.IsNullOrEmpty(batchid) && id != null)
                {
                    var data = _context.GetSingleBy<NewsArticle>(m => m.ID == id);
                    _context.DeleteAndSave<NewsArticle>(data);
                }
                else
                {
                    // 将遵循 json 规格定义的对象数据字符串转换为C#对象
                    var labelInfo = JsonConvert.DeserializeObject<List<NewsArticle>>(batchid);
                    foreach (var item in labelInfo)
                    {
                        var data = _context.GetSingleBy<NewsArticle>(m => m.ID == item.ID);
                        _context.Delete(data);
                    }
                    _context.Save();
                }
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
            var data = _context.GetSingle<NewsArticle>(Guid.Parse(id),m =>m.ArticleType);
            //文章类别 傻瓜式排序 按字段值拍在列的首位
            var TypeList_q = _context.GetAll<ArticleType>().Distinct(p => p.Name).ToList();
            TypeList_q.Remove(data.ArticleType);
            List<ArticleType> filerType = new List<ArticleType>();
            filerType.Add(data.ArticleType);
            foreach (var item in TypeList_q)
            {
                filerType.Add(item);
            }
            ViewBag.TypeList = filerType;
            
            //文章标签
            ViewBag.LabelList = _context.GetAll<Label>().Distinct(p => p.Name).ToList();
           
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
            var data = _context.GetSingleBy<NewsArticle>(m => m.ID == id);
            //文章类别 傻瓜式排序 按字段值拍在列的首位
            var TypeList_q = _context.GetAll<ArticleType>().Distinct(p => p.Name).ToList();
            TypeList_q.Remove(data.ArticleType);
            List<ArticleType> filerType = new List<ArticleType>();
            filerType.Add(data.ArticleType);
            foreach (var item in TypeList_q)
            {
                filerType.Add(item);
            }
            ViewBag.TypeList = filerType;
            //文章标签
            ViewBag.LabelList = _context.GetAll<Label>().Distinct(p => p.Name).ToList();
           
            ArticleVM avm = new ArticleVM(data);
            return View(avm); 
        }
    }
}
