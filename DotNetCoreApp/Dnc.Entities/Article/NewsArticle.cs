using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dnc.Entities.Article
{
    /// <summary>
    /// 文章
    /// </summary>
    public class NewsArticle:IEntity
    {
        [Key]
        public string ID { get; set; }   
        [StringLength(50)]
        public string Name { get; set; }                     // 文章标题
        [StringLength(100)]
        public string SubName { get; set; }                  // 文章副标题
        [DataType(DataType.Date)]
        public DateTime PublishDateTime { get; set; }        // 文章发布时间
        public string Content { get; set; }                  // 文章正文内容
        [StringLength(255)]
        public string TitleImage { get; set; }               // 文章标题图片
        public int VisitiSum { get; set; }                  //文章访问量
        public  string Label { get; set; }              //文章所属标签

        public virtual ArticleType ArticleType { get; set; } // 归属的文章类型

        public NewsArticle()
        {
            this.ID = Guid.NewGuid().ToString();
            this.PublishDateTime = DateTime.Now;
        }

    }
}
