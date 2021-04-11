using Eduhome.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eduhome.ViewModels
{
    public class HomeViewModel
    {
        public List<Slider> Sliders { get; set; }
        public List <Course> Courses { get; set; }
        public List<Settings> Settings { get; set; }
        public List<Notice> Notices { get; set; }


        public List<Event> Events { get; set; }

    }
}
