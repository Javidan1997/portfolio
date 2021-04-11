using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Eduhome.Data.Entities
{
    public class Event:BaseEntity
    {
        [StringLength(maximumLength: 150)]
        public string Title { get; set; }

        [StringLength(maximumLength: 1000)]

        public string Desc { get; set; }
        public string Photo { get; set; }
        [StringLength(maximumLength: 150)]
        public string Date { get; set; }
        [StringLength(maximumLength: 150)]
        public string Venure { get; set; }
        [StringLength(maximumLength: 150)]
        public string Time { get; set; }
      
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        [NotMapped]
        public int[] TagIds { get; set; }
        public List<EventTag> EventTags { get; set; }
        [NotMapped]
        public int [] TeacherIds { get; set; }
        public List<EventTeacher> EventTeachers { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }

    }
}
