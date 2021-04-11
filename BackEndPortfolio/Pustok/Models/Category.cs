﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pustok.Models
{
    public class Category:BaseEntity
    {
        [Required]
        [StringLength(maximumLength:100)]
        public string Name { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}