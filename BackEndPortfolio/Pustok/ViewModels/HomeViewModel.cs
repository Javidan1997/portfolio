﻿using Pustok.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pustok.ViewModels
{
    public class HomeViewModel
    {
        public List<Slider> Sliders { get; set; }
        public List<Feature> Features { get; set; }
        public List<Promotion> Promotions { get; set; }
        public List<Book> DiscountedSliderBooks { get; set; }
        public List<Book> NewSliderBooks { get; set; }
        public List<Book> AllSliderBooks { get; set; }

    }
}
