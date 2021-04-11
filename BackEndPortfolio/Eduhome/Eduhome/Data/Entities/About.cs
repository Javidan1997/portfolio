using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Eduhome.Data.Entities
{
    public class About : BaseEntity
    {
        [StringLength(maximumLength: 50)]
        public string Name { get; set; }

        [StringLength(maximumLength: 250)]

        public string Desc { get; set; }
        public string Photo { get; set; }

    }
}
