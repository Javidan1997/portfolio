using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DevitaP120.Data.Entities
{
    public class Product:BaseEntity
    {
        public int CategoryId { get; set; }

        [StringLength(maximumLength:100)]
        public string Name { get; set; }

        [StringLength(maximumLength: 120)]
        public string Slug { get; set; }
        [Range(0,double.MaxValue)]
        public double Price { get; set; }
        [Range(0, double.MaxValue)]
        public double ProducingPrice { get; set; }
        [Range(0, double.MaxValue)]
        public double DiscountedPrice { get; set; }
        [Range(0,100)]
        public double DiscountPercent { get; set; }

        [StringLength(maximumLength: 1500)]
        public string Desc { get; set; }

        [StringLength(maximumLength: 500)]
        public string InfoText { get; set; }
        public bool IsAvailable { get; set; }
        public double Rate { get; set; }

        public Category Category { get; set; }
        public List<ProductTag> ProductTags { get; set; }

        [NotMapped]
        public int[] TagIds { get; set; }

        public List<ProductPhoto> ProductPhotos { get; set; }

        [NotMapped]
        public List<IFormFile> Files { get; set; } = new List<IFormFile>();

        [NotMapped]
        public List<int> FileIds { get; set; }

        public List<ProductReview> ProductReviews { get; set; }

        public List<Order> Orders { get; set; }
    }
}
