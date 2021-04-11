using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DevitaP120.Areas.Manage.ViewModels
{
    public class CategoryCreateViewModel
    {
        [Range(0,int.MaxValue)]
        public int Order { get; set; }
        [StringLength(maximumLength:50)]
        public string Name { get; set; }
    }
}
