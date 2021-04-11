using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Pustok.DAL;
using Pustok.Models;
using Pustok.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pustok.Services
{
    public class LayoutViewModelService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _contextAccessor;

        public LayoutViewModelService(AppDbContext context,IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _contextAccessor = contextAccessor;
        }
        public List<Category> GetCategories()
        {
            List<Category> categories = _context.Categories.ToList();
            return categories;
        }

        public Setting GetSiteSetting()
        {
            return _context.Settings.FirstOrDefault();
        }

        public BasketCardViewModel GetBasket()
        {
            BasketCardViewModel basketVM = new BasketCardViewModel();

            var basket = _contextAccessor.HttpContext.Request.Cookies["basket"];

            List<BasketCardItemModel> basketCardItemModel = new List<BasketCardItemModel>();

            if(basket != null)
            {
                basketCardItemModel = JsonConvert.DeserializeObject<List<BasketCardItemModel>>(basket);
            }

            foreach (var basketItem in basketCardItemModel)
            {
                Book book = _context.Books.Include(x=>x.BookPhotos).FirstOrDefault(x => x.Id == basketItem.Id);

                #region CheckBookNotFound
                if (book == null)
                    continue;
                #endregion

                BasketBookItemViewModel basketItemVM = new BasketBookItemViewModel
                {
                    Id = basketItem.Id,
                    Count = basketItem.Count,
                    Name = book.Name,
                    Price = book.Price,
                    Poster = book.BookPhotos.FirstOrDefault(x => x.PosterStatus == true)?.Name,
                };

                basketVM.TotalPrice += book.Price * basketItem.Count;
                basketVM.BasketBookItems.Add(basketItemVM);
            }
            
            return basketVM;
        }
    }
}