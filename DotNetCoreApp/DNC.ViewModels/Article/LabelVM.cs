using Dnc.Entities.Article;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dnc.ViewModels.Article
{
    public class LabelVM
    {
        /// <summary>
        /// 标签ID
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 标签名称
        /// </summary>
        [Display(Name = "标签名称")]
        public string Name { get; set; }
        public LabelVM() { }
        public LabelVM(Label lb)
        {
            this.ID = lb.ID;
            this.Name = lb.Name;
        }
        public void MapBo(Label bo)
        {
            bo.Name = this.Name;
        }
    }
}
