using DevitaP120.Areas.Manage.ViewModels;
using DevitaP120.Data;
using DevitaP120.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevitaP120.Areas.Manage.Controllers
{
    [Area("manage")]
    public class TagController : Controller
    {
        private readonly AppDbContext _context;

        public TagController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            TagViewModel tagVM = new TagViewModel
            {
                Tags = await _context.Tags.ToListAsync()
            };
            return View(tagVM);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(Tag tag)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if(await _context.Tags.AnyAsync(x=>x.Name.ToLower() == tag.Name.ToLower()))
            {
                ModelState.AddModelError("Name", "Tag already exist!");
                return View();
            }

            tag.Name = tag.Name.Trim();
            tag.CreatedAt = DateTime.UtcNow;
            tag.ModifiedAt = DateTime.UtcNow;

            await _context.Tags.AddAsync(tag);
            await _context.SaveChangesAsync();

            return RedirectToAction("index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            Tag tag = await _context.Tags.FirstOrDefaultAsync(x =>x.Id == id);

            if (tag == null)
                return NotFound();

            return View(tag);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(Tag tag)
        {
            Tag existTag = await _context.Tags.FirstOrDefaultAsync(x => x.Id == tag.Id);

            if (existTag == null)
                return NotFound();

            existTag.Name = tag.Name;
            existTag.ModifiedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return RedirectToAction("index");
        }


        public async Task<IActionResult> Delete(int id)
        {
            Tag tag = await _context.Tags.FirstOrDefaultAsync(x => x.Id == id);

            if (tag == null)
            {
                return NotFound();
            }

            return View(tag);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Tag tag)
        {
            Tag existTag = await _context.Tags.FirstOrDefaultAsync(x => x.Id == tag.Id);

            if (existTag == null)
            {
                return NotFound();
            }

            _context.Tags.Remove(existTag);
            await _context.SaveChangesAsync();

            return RedirectToAction("index");
        }
    }
}
