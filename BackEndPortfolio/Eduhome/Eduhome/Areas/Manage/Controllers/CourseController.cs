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
            CourseViewModel CourseVM = new CourseViewModel
            {
                Courses = await _context.Courses.Include(x => x.Category).ToListAsync()
            };

            return View(CourseVM);
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _context.Categories.ToListAsync();
            ViewBag.Tags = await _context.Tags.ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Course Course)
        {
            #region CheckCourseAlreadyExist
            if (await _context.Courses.AnyAsync(a => a.Title.ToLower() == Course.Title.ToLower()))
            {
                ViewBag.Categories = await _context.Categories.ToListAsync();
                ViewBag.Tags = await _context.Tags.ToListAsync();
                ModelState.AddModelError("Name", "Already exist");
                return View();
            }
            #endregion

            #region CheckModelState
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _context.Categories.ToListAsync();
                ViewBag.Tags = await _context.Tags.ToListAsync();
                return View(Course);
            }
            #endregion
            if (!await _context.Categories.AnyAsync(c => c.Id == Course.CategoryId ))
            {
                ViewBag.Categories = await _context.Categories.ToListAsync();
                ViewBag.Tags = await _context.Tags.ToListAsync();
                ModelState.AddModelError("CategoryId", "Category not found!");
                return View();
            }

            Course.CourseTags = await _createCourseTags(Course.TagIds);


            if (Course.File != null)
            {
                #region CheckFileLength
                if (Course.File.Length > 2 * (1024 * 1024))
                {
                    ModelState.AddModelError("File", "Should be less than 2mb");
                    return View();
                }
                #endregion

                #region CheckFileContentType
                if (Course.File.ContentType != "image/png" && Course.File.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("File", "Select file type properly");
                    return View();
                }
                #endregion


                string filename = FileManager.Save(_env.WebRootPath, "uploads/courses", Course.File);

                Course.Photo = filename;
            }
            Course.CreatedAt = DateTime.UtcNow;
            Course.ModifiedAt = DateTime.UtcNow;
            await _context.Courses.AddAsync(Course);
            await _context.SaveChangesAsync();

            return RedirectToAction("index");
        }
        public async Task<IActionResult> Edit(int id)
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
            return View(Course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Course Course)
        {
            Course existCourse = await _context.Courses.Include(x => x.CourseTags).FirstOrDefaultAsync(x => x.Id == Course.Id);

            #region CheckCourseNotFound
            if (existCourse == null)
            {
                return NotFound();
            }
            #endregion
            if (!await _context.Categories.AnyAsync(x => x.Id == Course.CategoryId))
                return NotFound();
            existCourse.CourseTags = await _getUpdatedCourseTagsAsync(existCourse.CourseTags, Course.TagIds, Course.Id);

            existCourse.CategoryId = Course.CategoryId;
            existCourse.Title = Course.Title;
            existCourse.CourseHours = Course.CourseHours;
            existCourse.Duration = Course.Duration;
            existCourse.Language = Course.Language;
            existCourse.SkillLevel = Course.SkillLevel;
            existCourse.StudentCount = Course.StudentCount;
            existCourse.StartDate = Course.StartDate;
            existCourse.Desc = Course.Desc;
            existCourse.ModifiedAt = DateTime.UtcNow;

            if (Course.File != null)
            {
                #region CheckFileLength
                if (Course.File.Length > 2 * (1024 * 1024))
                {
                    ModelState.AddModelError("File", "Should be less than 2mb");
                    return View();
                }
                #endregion

                #region CheckFileContentType
                if (Course.File.ContentType != "image/png" && Course.File.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("File", "Select file type properly");
                    return View();
                }
                #endregion

                string filename = FileManager.Save(_env.WebRootPath, "uploads/courses", Course.File);

                if (!string.IsNullOrWhiteSpace(existCourse.Photo))
                {
                    FileManager.Delete(_env.WebRootPath, "uploads/courses", existCourse.Photo);
                }

                existCourse.Photo = filename;
            }
            else if (string.IsNullOrWhiteSpace(Course.Photo))
            {
                FileManager.Delete(_env.WebRootPath, "uploads/courses", existCourse.Photo);

                existCourse.Photo = null;
            }


            await _context.SaveChangesAsync();

            return RedirectToAction("index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            Course Course = await _context.Courses.FirstOrDefaultAsync(x => x.Id == id);

            #region CheckAuthorNotFound
            if (Course == null)
            {
                return NotFound();
            }
            #endregion
            return View(Course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int id)
        {
            Course Course = await _context.Courses.FirstOrDefaultAsync(x => x.Id == id);

            #region CheckAuthorNotFound
            if (Course == null)
            {
                return NotFound();
            }
            #endregion

            _context.Courses.Remove(Course);
            _context.SaveChanges();

            return RedirectToAction("index");
        }
        private async Task<List<CourseTag>> _getUpdatedCourseTagsAsync(List<CourseTag> CourseTags, int[] tagIds, int courseId)
        {
            List<CourseTag> removableTags = new List<CourseTag>();
            removableTags.AddRange(CourseTags);

            foreach (var tagId in tagIds)
            {
                CourseTag tag = CourseTags.FirstOrDefault(x => x.TagId == tagId);

                if (tag != null)
                {
                    removableTags.Remove(tag);
                }
                else
                {
                    if (!await _context.Tags.AnyAsync(x => x.Id == tagId))
                    {
                        throw new Exception("Tag not found!");
                    }

                    tag = new CourseTag
                    {
                        TagId = tagId,
                        CourseId = courseId
                    };

                    CourseTags.Add(tag);
                }
            }

            CourseTags = CourseTags.Except(removableTags).ToList();

            return CourseTags;
        }
        private async Task<List<CourseTag>> _createCourseTags(int[] tagIds)
        {

            List<CourseTag> CourseTags = new List<CourseTag>();
            foreach (var tagId in tagIds)
            {
                if (!await _context.Tags.AnyAsync(x => x.Id == tagId))
                {
                    throw new Exception("Tag not found!");
                }

                CourseTag CourseTag = new CourseTag
                {
                    TagId = tagId
                };

                CourseTags.Add(CourseTag);
            }

            return CourseTags;
        }


    }
}
