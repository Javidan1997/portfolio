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
    public class CategoryController : Controller
    {
        private AppDbContext _context;
      
        public CategoryController(AppDbContext context)
        {
            _context = context;       
        }

        public async Task<IActionResult> Index()
        {
            CategoryViewModel categoryVM = new CategoryViewModel
            {
                Categories = await _context.Categories.Where(c=>!c.IsDeleted).ToListAsync()
            };
            return View(categoryVM);
        }

        public async Task<IActionResult> Create()
        {
            Category lastCategory = await _context.Categories.Where(c=>!c.IsDeleted).OrderByDescending(c => c.Order).FirstOrDefaultAsync();

            if(lastCategory != null)
            {
                ViewBag.BiggestOrder = lastCategory.Order;
            }
            else
            {
                ViewBag.BiggestOrder = 1;
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryCreateViewModel categoryVM)
        {
            if (!ModelState.IsValid)
                return View();

            if(await _context.Categories.AnyAsync(x=>!x.IsDeleted &&  x.Name.ToLower() == categoryVM.Name.Trim().ToLower()))
            {
                ModelState.AddModelError("Name", "Category already exist!");
                return View();
            }
            
            Category category = new Category
            {
                Order = categoryVM.Order,
                Name = categoryVM.Name.Trim(),
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            };
            await _context.Categories.AddAsync(category);

            await _context.SaveChangesAsync();

            return RedirectToAction("index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            Category category = await _context.Categories.FirstOrDefaultAsync(x => !x.IsDeleted && x.Id == id);

            if(category == null)
            {
                return NotFound();
            }

            CategoryCreateViewModel categoryVM = new CategoryCreateViewModel
            {
                Name = category.Name,
                Order = category.Order
            };

            ViewBag.Id = id;

            return View(categoryVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CategoryCreateViewModel categoryVM)
        {
            Category category = await _context.Categories.FirstOrDefaultAsync(x => !x.IsDeleted && x.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
                return View();

            if (await _context.Categories.AnyAsync(x => x.Id!=id && x.Name.ToLower() == categoryVM.Name.Trim().ToLower()))
            {
                ModelState.AddModelError("Name", "Category already exist!");
                return View();
            }

            category.Order = categoryVM.Order;
            category.Name = categoryVM.Name;
            category.ModifiedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return RedirectToAction("index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            Category category = await _context.Categories.FirstOrDefaultAsync(x => !x.IsDeleted && x.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Category categoryModel)
        {
            Category category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == categoryModel.Id);

            if (category == null)
            {
                return NotFound();
            }

            category.IsDeleted = true;
            await _context.SaveChangesAsync();

            return RedirectToAction("index");
        }
    }
}
