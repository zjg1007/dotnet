using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dnc.DataAccessRepository.Repositories;
using Dnc.Entities.Article;
using Dnc.ApiModel.Article;

namespace Dnc.Services.Controllers
{
    [Route("api/[controller]")]
    public class ArticleController : Controller
    {
        private readonly IEntityRepository _DbService;

        public ArticleController(IEntityRepository service)
        {
            this._DbService = service;
        }

        /// <summary>
        /// 提取全部文章数据
        /// 访问方式：http://site:port/Article  
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<ArticleApiModel> Get()
        {
            var boCollection = _DbService.GetAll<NewsArticle>(x=>x.ArticleType).ToList();   // 提取所有的文章类型数据
            var boAMCollection = new List<ArticleApiModel>();
            foreach (var item in boCollection)
            {
                boAMCollection.Add(new ArticleApiModel(item));
            }
            return boAMCollection;
        }

        /// <summary>
        /// 根据文章的 id 获取文章的详细数据
        /// 访问方式：http://site:port/Article/18d64e28-7c32-4aec-a66c-4c0590fd8ce4
        /// </summary>
        /// <param name="id">文章的 ID</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ArticleApiModel Get(Guid id)
        {
            var bo = _DbService.GetSingle<NewsArticle>(id, x => x.ArticleType);
            var boAM = new ArticleApiModel(bo);
            return boAM;
        }

        /// <summary>
        /// 根据文章类型ID，提取相关类型的全部文章
        /// 访问方式：http://site:port/Article/GetByTypeID?typeID=e7322044-21fa-44a1-aaf9-0efedefe687a
        /// </summary>
        /// <param name="typeID">文章类型的 ID</param>
        /// <returns></returns>
        [HttpGet("GetByTypeID")]
        public IEnumerable<ArticleApiModel> GetByTypeID(Guid typeID)
        {
            var boCollection = _DbService.GetAll<NewsArticle>(x => x.ArticleType).Where(x => x.ArticleType.ID == typeID.ToString()).ToList();
            var boAMCollection = new List<ArticleApiModel>();
            foreach (var item in boCollection)
            {
                boAMCollection.Add(new ArticleApiModel(item));
            }
            return boAMCollection;
        }


        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
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
