using Dnc.DataAccessRepository.Context;
using Dnc.Entities.Article;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dnc.MvcApp.ViewInjections
{
    /// <summary>
    /// 提取文章数据的一个简单的页面注入器
    /// </summary>
    public class ArticleInjection
    {
        private readonly EntityDbContext _DbContext;  // EF 数据配置映射上下文

        public ArticleInjection(EntityDbContext context)
        {
            _DbContext = context;
        }

        public List<NewsArticle> GetArticleCollection() => _DbContext.Articles.ToList();
    }
}