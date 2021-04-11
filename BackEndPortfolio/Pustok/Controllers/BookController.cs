using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Pustok.DAL;
using Pustok.Models;
using Pustok.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pustok.Controllers
{
    public class BookController : Controller
    {
        private AppDbContext _context;
        public BookController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page=1,int? categoryId=null)
        {
            decimal pageCount = _context.Books.Where(x => (categoryId != null ? x.CategoryId == categoryId : true)).Count() / 4m;

            BookViewModel bookVM = new BookViewModel
            {
                Categories = _context.Categories.ToList(),
            };

            ViewBag.SelectedPage = page;
            ViewBag.PageCount = (int)Math.Ceiling(pageCount);
            ViewBag.CategoryId = categoryId;


            return View(bookVM);
        }

        public IActionResult AddBasket(int id)
        {
            Book book = _context.Books.FirstOrDefault(b => b.Id == id);

            #region CheckBookNotFound
            if (book == null)
                return RedirectToAction("index");
            #endregion

            List<BasketCardItemModel> basketItems = new List<BasketCardItemModel>();
            
            if (Request.Cookies["basket"] == null)
            {

                BasketCardItemModel basketCardItemModel = new BasketCardItemModel
                {
                    Id = id,
                    Count = 1,
                };
                basketItems.Add(basketCardItemModel);
            }
            else
            {
                basketItems = JsonConvert.DeserializeObject<List<BasketCardItemModel>>(Request.Cookies["basket"]);

                if (basketItems.Any(b=>b.Id == id))
                {
                    BasketCardItemModel existBasketItem = basketItems.FirstOrDefault(b => b.Id == id);
                    existBasketItem.Count += 1;
                }
                else
                {
                    BasketCardItemModel basketCardItemModel = new BasketCardItemModel
                    {
                        Id = id,
                        Count = 1
                    };
                    basketItems.Add(basketCardItemModel);
                }
            }

            Response.Cookies.Append("basket", JsonConvert.SerializeObject(basketItems), new CookieOptions { MaxAge = TimeSpan.FromDays(1) });

            return RedirectToAction("index");
        }

        public IActionResult Basket()
        {
            var basket = Request.Cookies["basket"];
            List<BasketCardItemModel> basketItems = new List<BasketCardItemModel>();
           
            if(basket != null)
            {
                basketItems = JsonConvert.DeserializeObject<List<BasketCardItemModel>>(basket);
            }

            return Json(basketItems);
        }
    }
}
