using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Eduhome.Data.Entities
{
    public class Notice:BaseEntity

    {
        [StringLength(maximumLength: 500)]
        public string NoticeDate { get; set; }
        [StringLength(maximumLength: 1500)]
        public string NoticeDesc { get; set; }
    }
}
