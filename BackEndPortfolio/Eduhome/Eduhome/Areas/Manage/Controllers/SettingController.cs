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
    public class SettingController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SettingController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            SettingViewModel SettingVM = new SettingViewModel
            {
                Settings = await _context.Settings.ToListAsync()
            };

            return View(SettingVM);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Settings Setting)
        {
           

            #region CheckModelState
            if (!ModelState.IsValid)
            {
                return View(Setting);
            }
            #endregion



            Setting.CreatedAt = DateTime.UtcNow;
            Setting.ModifiedAt = DateTime.UtcNow;
            await _context.Settings.AddAsync(Setting);
            await _context.SaveChangesAsync();

            return RedirectToAction("index");
        }
        public async Task<IActionResult> Edit(int id)
        {
            Settings Setting = await _context.Settings.FirstOrDefaultAsync(x => x.Id == id);

            #region CheckSettingNotFound
            if (Setting == null)
            {
                return NotFound();
            }
            #endregion

            return View(Setting);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Settings Setting)
        {
            Settings existSetting = await _context.Settings.FirstOrDefaultAsync(x => x.Id == Setting.Id);

            #region CheckSettingNotFound
            if (existSetting == null)
            {
                return NotFound();
            }
            #endregion

            existSetting.About = Setting.About;
            existSetting.Adress = Setting.Adress;
       
            existSetting.Number = Setting.Number;
            existSetting.TestimonialAbout = Setting.TestimonialAbout;
            existSetting.TestimonialName = Setting.TestimonialName;
            existSetting.TestimonialTitle = Setting.TestimonialTitle;
            existSetting.VideoUrl = Setting.VideoUrl;


            existSetting.ModifiedAt = DateTime.UtcNow;



            await _context.SaveChangesAsync();

            return RedirectToAction("index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            Settings Setting = await _context.Settings.FirstOrDefaultAsync(x => x.Id == id);

            #region CheckAuthorNotFound
            if (Setting == null)
            {
                return NotFound();
            }
            #endregion
            return View(Setting);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int id)
        {
            Settings Setting = await _context.Settings.FirstOrDefaultAsync(x => x.Id == id);

            #region CheckAuthorNotFound
            if (Setting == null)
            {
                return NotFound();
            }
            #endregion

            _context.Settings.Remove(Setting);
            _context.SaveChanges();

            return RedirectToAction("index");
        }


    }
}
