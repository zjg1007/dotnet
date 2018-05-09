using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dnc.Entities.Article
{
    /// <summary>
    /// 文章类型
    /// </summary>
    public class ArticleType:IEntity
    {
        [Key]
        public string ID { get; set; }
        [StringLength(20)]
        public string Name { get; set; }        // 文章类型名称
        
        public int Decription { get; set; }  // 文章类型说明
        
        public int SortCode { get; set; }    // 文章类型说明
        public int StandbySum { get; set; } //备用整形变量
        public ArticleType()
        {
            this.ID = Guid.NewGuid().ToString();
            this.Decription = 0;
            this.SortCode = 0;
            this.StandbySum = 0;
        }
    }
}
