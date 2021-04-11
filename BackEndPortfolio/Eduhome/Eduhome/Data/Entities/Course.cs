using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Eduhome.Data.Entities
{
    public class Course : BaseEntity
    {
        [StringLength(maximumLength: 150)]
        public string Title { get; set; }

        [StringLength(maximumLength: 1000)]

        public string Desc { get; set; }
        public string Photo { get; set; }
        [StringLength(maximumLength: 150)]
        public string StartDate { get; set; }
        [StringLength(maximumLength: 150)]
        public string Duration { get; set; }
        public int CourseHours { get; set; }
        [StringLength(maximumLength: 150)]
        public string SkillLevel { get; set; }
        [StringLength(maximumLength: 150)]
        public string Language { get; set; }
        public double Price { get; set; }
        public int StudentCount { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [NotMapped]
        public int[] TagIds { get; set; }
        public List<CourseTag> CourseTags { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }


    }
}
