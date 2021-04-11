using Eduhome.ViewModels;
using Eduhome.Data;
using Eduhome.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eduhome.Controllers
{
    public class TeacherController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public TeacherController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }


        public async Task<IActionResult> Index()
        {
            TeacherIndexViewModel TeacherVM = new TeacherIndexViewModel
            {
                Teachers = await _context.Teachers.ToListAsync()
            };

            return View(TeacherVM);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Teacher Teacher = await _context.Teachers.FirstOrDefaultAsync(x => x.Id == id);

            #region CheckTeacherNotFound
            if (Teacher == null)
            {
                return NotFound();
            }
            #endregion

      
            return View(Teacher);
        }
    }
}
