using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dnc.Entities.Application
{
    public class ApplicationGroup:IEntity
    {
        [Key]
        public string ID { get; set; }
        [StringLength(20)]
        public string Name { get; set; }
        [StringLength(500)]
        public string Decription { get; set; }

        public ApplicationGroup()
        {
            this.ID = Guid.NewGuid().ToString();
        }

    }
}
