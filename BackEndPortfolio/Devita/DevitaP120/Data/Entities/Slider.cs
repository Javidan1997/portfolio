using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DevitaP120.Data.Entities
{
    public class Slider:BaseEntity
    {
        [StringLength(maximumLength:150)]
        public string Title { get; set; }

        [StringLength(maximumLength: 350)]
        public string Text { get; set; }
        public double? Price { get; set; }

        [StringLength(maximumLength: 350)]
        public string RedirectUrl { get; set; }

        [StringLength(maximumLength: 100)]
        public string Photo { get; set; }
    }
}
