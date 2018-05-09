using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Dnc.Entities.Article;

namespace Dnc.ViewModels.Article
{
    public class ArticleVM
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
        public string Label { get; set; }
        public List<string> TLabel  { get; set; }

        public ArticleVM() { }

        public ArticleVM(NewsArticle bo) {
            this.ID = bo.ID;
            this.Name = bo.Name;
            this.SubName = bo.SubName;
            this.PublishDateTime = bo.PublishDateTime;
            this.Content = bo.Content;
            this.TitleImage = bo.TitleImage;
            if (bo.ArticleType != null)
            {
                this.ArticleTypeID = bo.ArticleType.ID;
                this.ArticleTypeName = bo.ArticleType.Name;
            }
            //标签获取
            if (bo.Label != null) {
                TLabel = new List<string>();
                this.Label = bo.Label;
                string[] LStr = bo.Label.Split(',');
                for (int i=0;i<LStr.Length;i++) {
                    TLabel.Add(LStr[i].ToString());
                }
            }
        }

        public void MapBo(NewsArticle bo)
        {
            bo.Name = this.Name;
            bo.SubName = this.SubName;
            bo.PublishDateTime = this.PublishDateTime;
            bo.Content = this.Content;
            //bo.TitleImage = this.TitleImage;
        }
    }
}
