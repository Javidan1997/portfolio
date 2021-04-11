using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pustok.Models
{
    public class Setting:BaseEntity
    {
        [Required]
        [StringLength(maximumLength:100)]
        public string Logo { get; set; }
        [Required]
        [StringLength(maximumLength: 100)]
        public string FooterLogo { get; set; }

        [StringLength(maximumLength: 20)]
        public string ServicePhone { get; set; }

        [StringLength(maximumLength: 250)]
        public string Address { get; set; }
        [StringLength(maximumLength: 20)]
        public string Phone { get; set; }

        [StringLength(maximumLength: 100)]
        public string Email { get; set; }

        [StringLength(maximumLength: 100)]
        public string FacebookUrl { get; set; }

        [StringLength(maximumLength: 100)]
        public string TwitterUrl { get; set; }
        [StringLength(maximumLength: 100)]
        public string YoutubeUrl { get; set; }

    }
}
