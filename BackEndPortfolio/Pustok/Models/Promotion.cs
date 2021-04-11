using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pustok.Models
{
    public class Promotion:BaseEntity
    {
        [StringLength(maximumLength:100)]
        public string Photo { get; set; }

        [StringLength(maximumLength: 250)]
        public string RedirectUrl { get; set; }
    }
}
