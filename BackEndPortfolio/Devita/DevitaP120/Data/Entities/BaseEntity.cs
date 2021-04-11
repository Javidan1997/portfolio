using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevitaP120.Data.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime ModifiedAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
