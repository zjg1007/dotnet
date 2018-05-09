using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Dnc.Entities.Organization
{
    public class Person:IEntity
    {
        [Key]
        public string ID { get; set; }
        [StringLength(20)]
        public string Name { get; set; }
        public bool Sex { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDay { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        [StringLength(50)]
        public string SortCode { get; set; }

        public Person()
        {
            this.ID = Guid.NewGuid().ToString();
            this.BirthDay = DateTime.Now;
        }
    }
}
