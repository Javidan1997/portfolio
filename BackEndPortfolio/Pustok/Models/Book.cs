using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pustok.Models
{
    public class Book:BaseEntity
    {
        public int CategoryId { get; set; }
        public int AuthorId { get; set; }
        [StringLength(maximumLength:150)]
        public string Name { get; set; }

        [StringLength(maximumLength: 1500)]
        public string Desc { get; set; }

        [StringLength(maximumLength: 10)]
        public string CodePref { get; set; }
        public int CodeNum { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsNew { get; set; }
        public double Price { get; set; }
        public double DiscountPercent { get; set; }
        public double DiscountedPrice { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual Category Category { get; set; }
        public virtual Author Author { get; set; }
        public virtual ICollection<BookPhoto> BookPhotos { get; set; }
    }
}
