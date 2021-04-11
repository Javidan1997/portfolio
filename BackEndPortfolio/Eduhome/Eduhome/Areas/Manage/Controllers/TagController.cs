using Eduhome.Area.Manage.ViewModels;
using Eduhome.Areas.Manage.ViewModels;
using Eduhome.Data;
using Eduhome.Data.Entities;
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
    public class TagController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public TagController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            TagViewModel TagVM = new TagViewModel
            {
                Tags = await _context.Tags.ToListAsync()
            };

            return View(TagVM);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Tag Tag)
        {
            #region CheckTagAlreadyExist
            if (await _context.Tags.AnyAsync(a => a.Name.ToLower() == Tag.Name.ToLower()))
            {
                ModelState.AddModelError("Name", "Already exist");
                return View();
            }
            #endregion

            #region CheckModelState
            if (!ModelState.IsValid)
            {
                return View(Tag);
            }
            #endregion


         
            Tag.CreatedAt = DateTime.UtcNow;
            Tag.ModifiedAt = DateTime.UtcNow;
            await _context.Tags.AddAsync(Tag);
            await _context.SaveChangesAsync();

            return RedirectToAction("index");
        }
        public async Task<IActionResult> Edit(int id)
        {
            Tag Tag = await _context.Tags.FirstOrDefaultAsync(x => x.Id == id);

            #region CheckTagNotFound
            if (Tag == null)
            {
                return NotFound();
            }
            #endregion

            return View(Tag);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Tag Tag)
        {
            Tag existTag = await _context.Tags.FirstOrDefaultAsync(x => x.Id == Tag.Id);

            #region CheckTagNotFound
            if (existTag == null)
            {
                return NotFound();
            }
            #endregion

            existTag.Name = Tag.Name;
            existTag.ModifiedAt = DateTime.UtcNow;

         

            await _context.SaveChangesAsync();

            return RedirectToAction("index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            Tag Tag = await _context.Tags.FirstOrDefaultAsync(x => x.Id == id);

            #region CheckAuthorNotFound
            if (Tag == null)
            {
                return NotFound();
            }
            #endregion
            return View(Tag);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int id)
        {
            Tag Tag = await _context.Tags.FirstOrDefaultAsync(x => x.Id == id);

            #region CheckAuthorNotFound
            if (Tag == null)
            {
                return NotFound();
            }
            #endregion

            _context.Tags.Remove(Tag);
            _context.SaveChanges();

            return RedirectToAction("index");
        }


    }
}
