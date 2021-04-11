using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pustok.Models
{
    public class Author:BaseEntity
    {
        [StringLength(maximumLength:50)]
        public string Fullname { get; set; }

        [StringLength(maximumLength: 500)]
        public string Desc { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
