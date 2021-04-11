using Eduhome.Data;
using Eduhome.Data.Entities;
using Eduhome.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eduhome.Controllers
{
 
        public class EventController : Controller
        {
            private readonly AppDbContext _context;
            private readonly IWebHostEnvironment _env;

            public EventController(AppDbContext context, IWebHostEnvironment env)
            {
                _context = context;
                _env = env;
            }


            public async Task<IActionResult> Index()
            {
                EventIndexViewModel EventVM = new EventIndexViewModel
                {
                    Events = await _context.Events.ToListAsync()
                };

                return View(EventVM);
            }

            public async Task<IActionResult> Detail(int id)
            {
            Event Event = await _context.Events.Include(x => x.EventTags).Include(x => x.EventTeachers).FirstOrDefaultAsync(x => x.Id == id);

            #region CheckEventNotFound
            if (Event == null)
                {
                    return NotFound();
                }
                #endregion

                ViewBag.Categories = await _context.Categories.ToListAsync();
                ViewBag.Tags = await _context.Tags.ToListAsync();
                ViewBag.Teachers = await _context.Teachers.ToListAsync();
                return View(Event);
        }
        }
}
