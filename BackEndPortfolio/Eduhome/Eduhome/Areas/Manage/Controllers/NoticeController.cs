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
    public class NoticeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public NoticeController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            NoticeViewModel NoticeVM = new NoticeViewModel
            {
                Notices = await _context.Notices.ToListAsync()
            };

            return View(NoticeVM);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Notice Notice)
        {
          

            #region CheckModelState
            if (!ModelState.IsValid)
            {
                return View(Notice);
            }
            #endregion



            Notice.CreatedAt = DateTime.UtcNow;
            Notice.ModifiedAt = DateTime.UtcNow;
            await _context.Notices.AddAsync(Notice);
            await _context.SaveChangesAsync();

            return RedirectToAction("index");
        }
        public async Task<IActionResult> Edit(int id)
        {
            Notice Notice = await _context.Notices.FirstOrDefaultAsync(x => x.Id == id);

            #region CheckNoticeNotFound
            if (Notice == null)
            {
                return NotFound();
            }
            #endregion

            return View(Notice);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Notice Notice)
        {
            Notice existNotice = await _context.Notices.FirstOrDefaultAsync(x => x.Id == Notice.Id);

            #region CheckNoticeNotFound
            if (existNotice == null)
            {
                return NotFound();
            }
            #endregion

            existNotice.NoticeDate = Notice.NoticeDate;
            existNotice.NoticeDesc = Notice.NoticeDesc;
            existNotice.ModifiedAt = DateTime.UtcNow;



            await _context.SaveChangesAsync();

            return RedirectToAction("index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            Notice Notice = await _context.Notices.FirstOrDefaultAsync(x => x.Id == id);

            #region CheckAuthorNotFound
            if (Notice == null)
            {
                return NotFound();
            }
            #endregion
            return View(Notice);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int id)
        {
            Notice Notice = await _context.Notices.FirstOrDefaultAsync(x => x.Id == id);

            #region CheckAuthorNotFound
            if (Notice == null)
            {
                return NotFound();
            }
            #endregion

            _context.Notices.Remove(Notice);
            _context.SaveChanges();

            return RedirectToAction("index");
        }


    }
}
