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
    public class CourseController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public CourseController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }


        public async Task<IActionResult> Index()
        {
           

            CourseIndexViewModel courseVM = new CourseIndexViewModel
            {
                Courses = await _context.Courses.ToListAsync()
            };

            return View(courseVM);
        }
        public ActionResult Index(string search)
        {
            return View(_context.Courses.Where(x => x.Title.Contains(search)).ToList());
        }



        //public JsonResult GetSearchValue(string search)
        //{
        //    List<Course> allsearch = new List<Course>();
        //    allsearch = _context.Courses.Where(x => x.Title.Contains(search)).Select(x => new Course
        //    {
        //        Title = x.Title,
        //    }).ToList();
        //    return new JsonResult { Data = allsearch, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        //}



        public async Task<IActionResult> Detail(int id)
        {
            Course Course = await _context.Courses.Include(x => x.CourseTags).FirstOrDefaultAsync(x => x.Id == id);

            #region CheckCourseNotFound
            if (Course == null)
            {
                return NotFound();
            }
            #endregion

            ViewBag.Categories = await _context.Categories.ToListAsync();
            ViewBag.Tags = await _context.Tags.ToListAsync();
            ViewBag.Teachers = await _context.Teachers.ToListAsync();
            return View(Course);
        }
    }
}
