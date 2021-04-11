using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pustok.Models
{
    public class Feature:BaseEntity
    {
        [StringLength(maximumLength:200)]
        public string Icon { get; set; }
            
        [StringLength(maximumLength: 100)]
        public string Title { get; set; }

        [StringLength(maximumLength: 100)]
        public string SubTitle { get; set; }
        public int Order { get; set; }

    }
}
