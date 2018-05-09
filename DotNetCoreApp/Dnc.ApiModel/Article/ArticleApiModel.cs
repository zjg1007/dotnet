using Dnc.Entities.Article;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dnc.ApiModel.Article
{
    public class ArticleApiModel
    {
        public string ID { get; set; }
        [StringLength(50)]
        public string Name { get; set; }                     // 文章标题
        public string SubName { get; set; }                  // 文章副标题
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh mm}", ApplyFormatInEditMode = true)]
        public DateTime PublishDateTime { get; set; }        // 文章发布时间
        public string Content { get; set; }                  // 文章正文内容
        public string TitleImage { get; set; }               // 文章标题图片

        public virtual string ArticleTypeID { get; set; }      // 归属的文章类型
        public virtual string ArticleTypeName { get; set; }  // 归属的文章类型

        public ArticleApiModel() { }

        public ArticleApiModel(NewsArticle bo)
        {
            this.ID = bo.ID;
            this.Name = bo.Name;
            this.SubName = bo.SubName;
            this.PublishDateTime = bo.PublishDateTime;
            this.Content = bo.Content;
            this.TitleImage = "http://localhost:8063/"+bo.TitleImage.Substring(6, bo.TitleImage.Length-6);
            if (bo.ArticleType != null)
            {
                this.ArticleTypeID = bo.ArticleType.ID;
                this.ArticleTypeName = bo.ArticleType.Name;
            }
        }
    }
}
