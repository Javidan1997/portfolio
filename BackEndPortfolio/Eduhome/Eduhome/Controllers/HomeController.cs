using Eduhome.Data;
using Eduhome.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Eduhome.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public HomeController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }


        public async Task<IActionResult> Index()
        {
            HomeViewModel homeVM = new HomeViewModel
            {
                Sliders = await _context.Sliders.ToListAsync(),
                Courses = await _context.Courses.Take(3).ToListAsync(),
                Events = await _context.Events.Take(8).ToListAsync(),
                Settings = await _context.Settings.ToListAsync(),
                Notices = await _context.Notices.Take(6).ToListAsync()



            };

            return View(homeVM);
        }
    }
}
