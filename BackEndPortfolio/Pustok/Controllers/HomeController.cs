using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Pustok.DAL;
using Pustok.Enums;
using Pustok.Models;
using Pustok.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Pustok.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            HomeViewModel homeVM = new HomeViewModel
            {
                Sliders = _context.Sliders.OrderBy(x => x.Order).ToList(),
                Features = _context.Features.OrderBy(x => x.Order).Take(4).ToList(),
                Promotions = _context.Promotions.Take(2).ToList(),
                NewSliderBooks = _context.Books.Include(x => x.Author).Include(x => x.BookPhotos).Where(x => x.IsNew).OrderByDescending(x => x.CreatedAt).Take(12).ToList(),
                DiscountedSliderBooks = _context.Books.Include(x => x.Author).Include(x => x.BookPhotos).Where(x => x.DiscountPercent > 0).OrderByDescending(x => x.CreatedAt).Take(12).ToList(),
                AllSliderBooks = _context.Books.Include(x => x.Author).Include(x => x.BookPhotos).OrderByDescending(x => x.CreatedAt).Take(12).ToList(),
            };

            return View(homeVM);
        }

        public IActionResult GetSliderBooks(BookSliderTabType? type = null)
        {
            List<Book> books = new List<Book>();

            switch (type)
            {
                case BookSliderTabType.New:
                    books = _context.Books.Include(x => x.Author).Include(x => x.BookPhotos).Where(x => x.IsNew).OrderByDescending(x => x.CreatedAt).Take(12).ToList();
                    break;
                case BookSliderTabType.Discounted:
                    books = _context.Books.Include(x => x.Author).Include(x => x.BookPhotos).Where(x => x.DiscountPercent > 0).OrderByDescending(x => x.CreatedAt).Take(12).ToList();
                    break;
                default:
                    books = _context.Books.Include(x => x.Author).Include(x => x.BookPhotos).OrderByDescending(x => x.CreatedAt).Take(12).ToList();
                    break;
            }

            return Json(books);
        }

        public IActionResult BookSliderCardPartial(BookSliderTabType? type = null)
        {
            List<Book> books = new List<Book>();

            switch (type)
            {
                case BookSliderTabType.New:
                    books = _context.Books.Include(x => x.Author).Include(x => x.BookPhotos).Where(x => x.IsNew).OrderByDescending(x => x.CreatedAt).Take(12).ToList();
                    break;
                case BookSliderTabType.Discounted:
                    books = _context.Books.Include(x => x.Author).Include(x => x.BookPhotos).Where(x => x.DiscountPercent > 0).OrderByDescending(x => x.CreatedAt).Take(12).ToList();
                    break;
                default:
                    books = _context.Books.Include(x => x.Author).Include(x => x.BookPhotos).OrderByDescending(x => x.CreatedAt).Take(12).ToList();
                    break;
            }

            return PartialView("_BookSliderCardPartial", books);
        }
    }
}
