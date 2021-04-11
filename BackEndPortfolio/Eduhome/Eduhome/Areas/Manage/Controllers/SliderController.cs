using Eduhome.Area.Manage.ViewModels;
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
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SliderController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            SliderViewModel sliderVM = new SliderViewModel
            {
                Sliders = await _context.Sliders.ToListAsync()
            };

            return View(sliderVM);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Slider slider)
        {
            #region CheckSliderAlreadyExist
            if (await _context.Sliders.AnyAsync(a => a.Name.ToLower() == slider.Name.ToLower()))
            {
                ModelState.AddModelError("Name", "Already exist");
                return View();
            }
            #endregion

            #region CheckModelState
            if (!ModelState.IsValid)
            {
                return View(slider);
            }
            #endregion


            if (slider.File != null)
            {
                #region CheckFileLength
                if (slider.File.Length > 2 * (1024 * 1024))
                {
                    ModelState.AddModelError("File", "Should be less than 2mb");
                    return View();
                }
                #endregion

                #region CheckFileContentType
                if (slider.File.ContentType != "image/png" && slider.File.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("File", "Select file type properly");
                    return View();
                }
                #endregion


                string filename = FileManager.Save(_env.WebRootPath, "uploads/sliders", slider.File);

                slider.Photo = filename;
            }
            slider.CreatedAt = DateTime.UtcNow;
            slider.ModifiedAt = DateTime.UtcNow;
            await _context.Sliders.AddAsync(slider);
            await _context.SaveChangesAsync();

            return RedirectToAction("index");
        }
        public async Task<IActionResult> Edit(int id)
        {
            Slider slider = await _context.Sliders.FirstOrDefaultAsync(x => x.Id == id);

            #region ChecksliderNotFound
            if (slider == null)
            {
                return NotFound();
            }
            #endregion

            return View(slider);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Slider slider)
        {
            Slider existSlider = await _context.Sliders.FirstOrDefaultAsync(x => x.Id == slider.Id);

            #region ChecksliderNotFound
            if (existSlider == null)
            {
                return NotFound();
            }
            #endregion

            existSlider.Name = slider.Name;
            existSlider.Desc = slider.Desc;
            existSlider.ModifiedAt = DateTime.UtcNow;

            if (slider.File != null)
            {
                #region CheckFileLength
                if (slider.File.Length > 2 * (1024 * 1024))
                {
                    ModelState.AddModelError("File", "Should be less than 2mb");
                    return View();
                }
                #endregion

                #region CheckFileContentType
                if (slider.File.ContentType != "image/png" && slider.File.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("File", "Select file type properly");
                    return View();
                }
                #endregion

                string filename = FileManager.Save(_env.WebRootPath, "uploads/sliders", slider.File);

                if (!string.IsNullOrWhiteSpace(existSlider.Photo))
                {
                    FileManager.Delete(_env.WebRootPath, "uploads/sliders", existSlider.Photo);
                }

                existSlider.Photo = filename;
            }
            else if (string.IsNullOrWhiteSpace(slider.Photo))
            {
                FileManager.Delete(_env.WebRootPath, "uploads/sliders", existSlider.Photo);

                existSlider.Photo = null;
            }


            await _context.SaveChangesAsync();

            return RedirectToAction("index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            Slider slider = await _context.Sliders.FirstOrDefaultAsync(x => x.Id == id);

            #region CheckAuthorNotFound
            if (slider == null)
            {
                return NotFound();
            }
            #endregion
            return View(slider);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int id)
        {
            Slider slider = await _context.Sliders.FirstOrDefaultAsync(x => x.Id == id);

            #region CheckAuthorNotFound
            if (slider == null)
            {
                return NotFound();
            }
            #endregion

            _context.Sliders.Remove(slider);
            _context.SaveChanges();

            return RedirectToAction("index");
        }


    }
}
