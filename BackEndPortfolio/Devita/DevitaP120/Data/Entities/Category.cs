using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DevitaP120.Data.Entities
{
    public class Category:BaseEntity
    {
        [StringLength(maximumLength:50)]
        public string Name { get; set; }
       
        [Range(minimum:1,maximum:int.MaxValue)]
        public int Order { get; set; }
        public bool IsDeleted { get; set; }
        public List<Product> Products { get; set; }
    }
}
