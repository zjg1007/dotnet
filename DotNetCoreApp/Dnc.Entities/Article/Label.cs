using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dnc.Entities.Article
{
    /// <summary>
    /// 标签表
    /// </summary>
    public class Label : IEntity
    {
        [Key]
        public string ID { get; set; }
        /// <summary>
        /// 标签名称
        /// </summary>
        public string Name { get; set; }
        public Label(){
            this.ID = Guid.NewGuid().ToString();
        }
    }
}
