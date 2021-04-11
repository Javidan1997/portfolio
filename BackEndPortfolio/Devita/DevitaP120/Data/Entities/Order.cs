using DevitaP120.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevitaP120.Data.Entities
{
    public class Order:BaseEntity
    {
        public string AppUserId { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
        public double Price { get; set; }
        public double DiscountedPrice { get; set; }
        public OrderStatus Status { get; set; }


        public AppUser AppUser { get; set; }
        public Product Product { get; set; }
    }
}
