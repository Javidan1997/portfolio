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
            EventViewModel EventVM = new EventViewModel
            {
                Events = await _context.Events.Include(x => x.Category).ToListAsync()
            };

            return View(EventVM);
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _context.Categories.ToListAsync();
            ViewBag.Tags = await _context.Tags.ToListAsync();
            ViewBag.Teachers = await _context.Teachers.ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Event Event)
        {
            #region CheckEventAlreadyExist
            if (await _context.Events.AnyAsync(a => a.Title.ToLower() == Event.Title.ToLower()))
            {
                ViewBag.Categories = await _context.Categories.ToListAsync();
                ViewBag.Tags = await _context.Tags.ToListAsync();
                ViewBag.Teachers = await _context.Teachers.ToListAsync();

                ModelState.AddModelError("Name", "Already exist");
                return View();
            }
            #endregion

            #region CheckModelState
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _context.Categories.ToListAsync();
                ViewBag.Tags = await _context.Tags.ToListAsync();
                ViewBag.Teachers = await _context.Teachers.ToListAsync();

                return View(Event);
            }
            #endregion
            if (!await _context.Categories.AnyAsync(c => c.Id == Event.CategoryId))
            {
                ViewBag.Categories = await _context.Categories.ToListAsync();
                ViewBag.Tags = await _context.Tags.ToListAsync();
                ViewBag.Teachers = await _context.Teachers.ToListAsync();

                ModelState.AddModelError("CategoryId", "Category not found!");
                return View();
            }

            Event.EventTags = await _createEventTags(Event.TagIds);
            Event.EventTeachers = await _createEventTeachers(Event.TeacherIds);


            if (Event.File != null)
            {
                #region CheckFileLength
                if (Event.File.Length > 2 * (1024 * 1024))
                {
                    ModelState.AddModelError("File", "Should be less than 2mb");
                    return View();
                }
                #endregion

                #region CheckFileContentType
                if (Event.File.ContentType != "image/png" && Event.File.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("File", "Select file type properly");
                    return View();
                }
                #endregion


                string filename = FileManager.Save(_env.WebRootPath, "uploads/Events", Event.File);

                Event.Photo = filename;
            }
            Event.CreatedAt = DateTime.UtcNow;
            Event.ModifiedAt = DateTime.UtcNow;
            await _context.Events.AddAsync(Event);
            await _context.SaveChangesAsync();

            return RedirectToAction("index");
        }
        public async Task<IActionResult> Edit(int id)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Event Event)
        {
           
            Event existEvent = await _context.Events.Include(x => x.EventTags).Include(x => x.EventTeachers).FirstOrDefaultAsync(x => x.Id == Event.Id);

            #region CheckEventNotFound
            if (existEvent == null)
            {
                return NotFound();
            }
            #endregion
            if (!await _context.Categories.AnyAsync(x => x.Id == Event.CategoryId))
                return NotFound();
            existEvent.EventTags = await _getUpdatedEventTagsAsync(existEvent.EventTags, Event.TagIds, Event.Id);
            existEvent.EventTeachers = await _getUpdatedEventTeacherAsync(existEvent.EventTeachers, Event.TeacherIds, Event.Id);

            existEvent.CategoryId = Event.CategoryId;
            existEvent.Title = Event.Title;
            existEvent.Date = Event.Date;
            existEvent.Time = Event.Time;
            existEvent.Venure = Event.Venure;
 
            existEvent.ModifiedAt = DateTime.UtcNow;

            if (Event.File != null)
            {
                #region CheckFileLength
                if (Event.File.Length > 2 * (1024 * 1024))
                {
                    ModelState.AddModelError("File", "Should be less than 2mb");
                    return View();
                }
                #endregion

                #region CheckFileContentType
                if (Event.File.ContentType != "image/png" && Event.File.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("File", "Select file type properly");
                    return View();
                }
                #endregion

                string filename = FileManager.Save(_env.WebRootPath, "uploads/Events", Event.File);

                if (!string.IsNullOrWhiteSpace(existEvent.Photo))
                {
                    FileManager.Delete(_env.WebRootPath, "uploads/Events", existEvent.Photo);
                }

                existEvent.Photo = filename;
            }
            else if (string.IsNullOrWhiteSpace(Event.Photo))
            {
                FileManager.Delete(_env.WebRootPath, "uploads/Events", existEvent.Photo);

                existEvent.Photo = null;
            }


            await _context.SaveChangesAsync();
            ViewBag.Categories = await _context.Categories.ToListAsync();
            ViewBag.Tags = await _context.Tags.ToListAsync();
            //ViewBag.Teachers = await _context.Teachers.ToListAsync();
            return RedirectToAction("index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            Event Event = await _context.Events.FirstOrDefaultAsync(x => x.Id == id);

            #region CheckAuthorNotFound
            if (Event == null)
            {
                return NotFound();
            }
            #endregion
            return View(Event);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int id)
        {
            Event Event = await _context.Events.FirstOrDefaultAsync(x => x.Id == id);

            #region CheckAuthorNotFound
            if (Event == null)
            {
                return NotFound();
            }
            #endregion

            _context.Events.Remove(Event);
            _context.SaveChanges();

            return RedirectToAction("index");
        }
        private async Task<List<EventTag>> _getUpdatedEventTagsAsync(List<EventTag> EventTags, int[] tagIds, int EventId)
        {
            List<EventTag> removableTags = new List<EventTag>();
            removableTags.AddRange(EventTags);

            foreach (var tagId in tagIds)
            {
                EventTag tag = EventTags.FirstOrDefault(x => x.TagId == tagId);

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

                    tag = new EventTag
                    {
                        TagId = tagId,
                        EventId = EventId
                    };

                    EventTags.Add(tag);
                }
            }

            EventTags = EventTags.Except(removableTags).ToList();

            return EventTags;
        }
        private async Task<List<EventTeacher>> _getUpdatedEventTeacherAsync(List<EventTeacher> eventTeachers, int[] teacherIds, int EventId)
        {
            List<EventTeacher> removableTeachers = new List<EventTeacher>();
            removableTeachers.AddRange(eventTeachers);

            foreach (var teacherId in teacherIds)
            {
                EventTeacher teacher = eventTeachers.FirstOrDefault(x => x.TeacherId == teacherId);

                if (teacher != null)
                {
                    removableTeachers.Remove(teacher);
                }
                else
                {
                    if (!await _context.Tags.AnyAsync(x => x.Id == teacherId))
                    {
                        throw new Exception("Teacher not found!");
                    }

                    teacher = new EventTeacher
                    {
                        TeacherId = teacherId,
                        EventId = EventId
                    };

                    eventTeachers.Add(teacher);
                }
            }

            eventTeachers = eventTeachers.Except(removableTeachers).ToList();

            return eventTeachers;
        }
        private async Task<List<EventTag>> _createEventTags(int[] tagIds)
        {

            List<EventTag> EventTags = new List<EventTag>();
            foreach (var tagId in tagIds)
            {
                if (!await _context.Tags.AnyAsync(x => x.Id == tagId))
                {
                    throw new Exception("Tag not found!");
                }

                EventTag EventTag = new EventTag
                {
                    TagId = tagId
                };

                EventTags.Add(EventTag);
            }

            return EventTags;
        }
        private async Task<List<EventTeacher>> _createEventTeachers(int[] teacherIds)
        {

            List<EventTeacher> eventTeachers = new List<EventTeacher>();
            foreach (var teacherId in teacherIds)
            {
                if (!await _context.Teachers.AnyAsync(x => x.Id == teacherId))
                {
                    throw new Exception("Teacher not found!");
                }

                EventTeacher eventTeacher = new EventTeacher
                {
                    TeacherId = teacherId
                };

                eventTeachers.Add(eventTeacher);
            }

            return eventTeachers;
        }


    }
}
