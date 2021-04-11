using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Eduhome.Data.Entities
{
    public class Settings: BaseEntity
    {
        [StringLength(maximumLength: 500)]
        public string VideoUrl { get; set; }
        [StringLength(maximumLength: 1500)]
        public string About { get; set; }
       
        [StringLength(maximumLength: 1500)]
        public string TestimonialAbout { get; set; }
        [StringLength(maximumLength: 500)]
        public string TestimonialName { get; set; }
        [StringLength(maximumLength: 500)]
        public string TestimonialTitle { get; set; }
        [StringLength(maximumLength: 500)]
        public string Adress { get; set; }
        [StringLength(maximumLength: 500)]
        public string Number { get; set; }

   


    }
}
