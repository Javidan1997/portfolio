using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pustok.Models
{
    public class BookPhoto:BaseEntity
    {
        public int BookId { get; set; }
        [StringLength(maximumLength:100)]
        public string Name { get; set; }
        public int Order { get; set; }
        public bool? PosterStatus { get; set; }

        public virtual Book Book { get; set; }
    }
}
