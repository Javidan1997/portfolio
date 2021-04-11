using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DevitaP120.Data.Entities
{
    public class Tag:BaseEntity
    {
        [StringLength(maximumLength:20)]
        public string Name { get; set; }

        public List<ProductTag> ProductTags { get; set; }
    }
}
