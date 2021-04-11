using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pustok.DAL;
using Pustok.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pustok.ViewComponents
{
    public class BookViewComponent:ViewComponent
    {
        private readonly AppDbContext _context;

        public BookViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int page=1,int? categoryId=null)
        {
            List<Book> books = _context.Books.Where(x=> (categoryId!=null?x.CategoryId==categoryId:true)).Include(x => x.BookPhotos).Include(x => x.Author).Skip((page - 1) * 4).Take(4).ToList();
            return await Task.FromResult((IViewComponentResult)View("Default", books));
        }
    }
}
