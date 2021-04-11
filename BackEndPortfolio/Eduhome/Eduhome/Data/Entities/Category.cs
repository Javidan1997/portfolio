using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Eduhome.Data.Entities
{
    public class Category: BaseEntity
    {
        [StringLength(maximumLength: 150)]
        public string Name { get; set; }
        public int Count { get; set; }
        public List<Course> Courses { get; set; }
        public List<Event> Events { get; set; }
    }
}
