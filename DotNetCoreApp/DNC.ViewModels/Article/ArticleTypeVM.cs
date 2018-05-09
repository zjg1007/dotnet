using Dnc.Entities.Article;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dnc.ViewModels.Article
{
    public class ArticleTypeVM
    {
        public string ID { get; set; }

        [Required(ErrorMessage = "类型名称不能为空。")]
        [Display(Name = "类型名称")]
        [StringLength(20, ErrorMessage = "文章类型文字不能超过 20 个字符")]
        public string Name { get; set; }   // 文章类型名称

        //[StringLength(500, ErrorMessage = "文章类型说明文字不能超过 500 个字符")]
        public int Decription { get; set; }  // 文章类型说明

       // [Required(ErrorMessage = "类型编码不能为空。")]
        //[StringLength(50, ErrorMessage = "文章类型编码不能超过 50 个字符")]
        public int SortCode { get; set; }  // 文章类型说明

        public ArticleTypeVM() { }

        public ArticleTypeVM(ArticleType bo)
        {
            this.ID         = bo.ID.ToString();
            this.Name       = bo.Name;
            this.Decription = bo.Decription;
            this.SortCode   = bo.SortCode;
        }

        public void MapBo(ArticleType bo)
        {
            bo.Name       = this.Name;
            bo.Decription = this.Decription;
            bo.SortCode   = this.SortCode;
        }
    }
}
