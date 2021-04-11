using Eduhome.Area.Manage.ViewModels;
using Eduhome.Areas.Manage.ViewModels;
using Eduhome.Data;
using Eduhome.Data.Entities;
using Eduhome.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eduhome.Area.Manage.Controllers
{
    [Area("Manage")]
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
            TeacherViewModel TeacherVM = new TeacherViewModel
            {
                Teachers = await _context.Teachers.ToListAsync()
            };

            return View(TeacherVM);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Teacher Teacher)
        {
            #region CheckTeacherAlreadyExist
            if (await _context.Teachers.AnyAsync(a => a.Fullname.ToLower() == Teacher.Fullname.ToLower()))
            {
                ModelState.AddModelError("Fullname", "Already exist");
                return View();
            }
            #endregion

            #region CheckModelState
            if (!ModelState.IsValid)
            {
                return View(Teacher);
            }
            #endregion


            if (Teacher.File != null)
            {
                #region CheckFileLength
                if (Teacher.File.Length > 2 * (1024 * 1024))
                {
                    ModelState.AddModelError("File", "Should be less than 2mb");
                    return View();
                }
                #endregion

                #region CheckFileContentType
                if (Teacher.File.ContentType != "image/png" && Teacher.File.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("File", "Select file type properly");
                    return View();
                }
                #endregion


                string filename = FileManager.Save(_env.WebRootPath, "uploads/Teachers", Teacher.File);

                Teacher.Photo = filename;
            }
            Teacher.CreatedAt = DateTime.UtcNow;
            Teacher.ModifiedAt = DateTime.UtcNow;
            await _context.Teachers.AddAsync(Teacher);
            await _context.SaveChangesAsync();

            return RedirectToAction("index");
        }
        public async Task<IActionResult> Edit(int id)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Teacher Teacher)
        {
            Teacher existTeacher = await _context.Teachers.FirstOrDefaultAsync(x => x.Id == Teacher.Id);

            #region CheckTeacherNotFound
            if (existTeacher == null)
            {
                return NotFound();
            }
            #endregion

            existTeacher.Fullname = Teacher.Fullname;
            existTeacher.Abouut = Teacher.Abouut;
            existTeacher.Degree = Teacher.Degree;
            existTeacher.Experience = Teacher.Experience;
            existTeacher.Faculty = Teacher.Faculty;
            existTeacher.Hobbies = Teacher.Hobbies;
            existTeacher.Mail = Teacher.Mail;
            existTeacher.Numer = Teacher.Numer;
            existTeacher.Skype = Teacher.Skype;
            existTeacher.Title = Teacher.Title;



            existTeacher.ModifiedAt = DateTime.UtcNow;

            if (Teacher.File != null)
            {
                #region CheckFileLength
                if (Teacher.File.Length > 2 * (1024 * 1024))
                {
                    ModelState.AddModelError("File", "Should be less than 2mb");
                    return View();
                }
                #endregion

                #region CheckFileContentType
                if (Teacher.File.ContentType != "image/png" && Teacher.File.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("File", "Select file type properly");
                    return View();
                }
                #endregion

                string filename = FileManager.Save(_env.WebRootPath, "uploads/Teachers", Teacher.File);

                if (!string.IsNullOrWhiteSpace(existTeacher.Photo))
                {
                    FileManager.Delete(_env.WebRootPath, "uploads/Teachers", existTeacher.Photo);
                }

                existTeacher.Photo = filename;
            }
            else if (string.IsNullOrWhiteSpace(Teacher.Photo))
            {
                FileManager.Delete(_env.WebRootPath, "uploads/Teachers", existTeacher.Photo);

                existTeacher.Photo = null;
            }


            await _context.SaveChangesAsync();

            return RedirectToAction("index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            Teacher Teacher = await _context.Teachers.FirstOrDefaultAsync(x => x.Id == id);

            #region CheckAuthorNotFound
            if (Teacher == null)
            {
                return NotFound();
            }
            #endregion
            return View(Teacher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int id)
        {
            Teacher Teacher = await _context.Teachers.FirstOrDefaultAsync(x => x.Id == id);

            #region CheckAuthorNotFound
            if (Teacher == null)
            {
                return NotFound();
            }
            #endregion

            _context.Teachers.Remove(Teacher);
            _context.SaveChanges();

            return RedirectToAction("index");
        }


    }
}
