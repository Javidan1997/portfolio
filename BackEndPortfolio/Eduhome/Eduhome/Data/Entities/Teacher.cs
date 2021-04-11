using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Eduhome.Data.Entities
{
    public class Teacher:BaseEntity
    {
        [StringLength(maximumLength: 150)]
        public string Fullname { get; set; }
        [StringLength(maximumLength: 250)]
        public string Title { get; set; }

        [StringLength(maximumLength: 1000)]

        public string Abouut { get; set; }
        public string Photo { get; set; }
        [StringLength(maximumLength: 150)]
        public string Degree { get; set; }
        [StringLength(maximumLength: 150)]
        public string Experience { get; set; }
        [StringLength(maximumLength: 150)]
        public string Hobbies { get; set; }
        [StringLength(maximumLength: 150)]
        public string Faculty { get; set; }
        [StringLength(maximumLength: 150)]
        public string Mail { get; set; }
        [StringLength(maximumLength: 150)]
        public string Numer { get; set; }
        [StringLength(maximumLength: 150)]
        public string Skype { get; set; }
        public List <EventTeacher> EventTeachers { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }

    }
}
