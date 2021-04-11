using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pustok.Models
{
    public class Slider:BaseEntity
    {
        [Required]
        [StringLength(maximumLength:100)]
        public string Photo { get; set; }

        [Required]
        [StringLength(maximumLength: 100)]
        public string Title { get; set; }
        [Required]
        [StringLength(maximumLength: 100)]
        public string SubTitle { get; set; }

        [StringLength(maximumLength: 500)]
        public string Desc { get; set; }

        [StringLength(maximumLength: 20)]
        public string Price { get; set; }

        [StringLength(maximumLength: 250)]
        public string RedirectUrl { get; set; }
        public int Order { get; set; }
      

    }
}
