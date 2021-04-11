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
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public CategoryController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            CategoryViewModel CategoryVM = new CategoryViewModel
            {
                Categories = await _context.Categories.ToListAsync()
            };

            return View(CategoryVM);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category Category)
        {
            #region CheckCategoryAlreadyExist
            if (await _context.Categories.AnyAsync(a => a.Name.ToLower() == Category.Name.ToLower()))
            {
                ModelState.AddModelError("Name", "Already exist");
                return View();
            }
            #endregion

            #region CheckModelState
            if (!ModelState.IsValid)
            {
                return View(Category);
            }
            #endregion



            Category.CreatedAt = DateTime.UtcNow;
            Category.ModifiedAt = DateTime.UtcNow;
            await _context.Categories.AddAsync(Category);
            await _context.SaveChangesAsync();

            return RedirectToAction("index");
        }
        public async Task<IActionResult> Edit(int id)
        {
            Category Category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);

            #region CheckCategoryNotFound
            if (Category == null)
            {
                return NotFound();
            }
            #endregion

            return View(Category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Category Category)
        {
            Category existCategory = await _context.Categories.FirstOrDefaultAsync(x => x.Id == Category.Id);

            #region CheckCategoryNotFound
            if (existCategory == null)
            {
                return NotFound();
            }
            #endregion

            existCategory.Name = Category.Name;
            existCategory.Count = Category.Count;
            existCategory.ModifiedAt = DateTime.UtcNow;



            await _context.SaveChangesAsync();

            return RedirectToAction("index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            Category Category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);

            #region CheckAuthorNotFound
            if (Category == null)
            {
                return NotFound();
            }
            #endregion
            return View(Category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int id)
        {
            Category Category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);

            #region CheckAuthorNotFound
            if (Category == null)
            {
                return NotFound();
            }
            #endregion

            _context.Categories.Remove(Category);
            _context.SaveChanges();

            return RedirectToAction("index");
        }


    }
}
