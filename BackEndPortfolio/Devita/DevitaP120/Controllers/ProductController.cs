using DevitaP120.Data;
using DevitaP120.Data.Entities;
using DevitaP120.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevitaP120.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Detail(int id)
        {
            Product product = await _context.Products
                .Include(x=>x.ProductPhotos)
                .Include(x=>x.Category)
                .Include(x=>x.ProductReviews)
                .Include(x=>x.ProductTags).ThenInclude(x=>x.Tag)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (product == null)
                return NotFound();

            ProductDetailViewModel productVM = new ProductDetailViewModel
            {
                Product = product,
                RelatedProducts = await _context.Products
                .Include(x=>x.Category).Include(x=>x.ProductTags).ThenInclude(x=>x.Tag).Include(x=>x.ProductPhotos)
                .Where(x => x.CategoryId == product.CategoryId).OrderByDescending(x => x.CreatedAt).Take(10).ToListAsync()
            };

            return View(productVM);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Review(ProductReview review)
        {
            Product product = await _context.Products.Include(x=>x.ProductReviews).FirstOrDefaultAsync(x => x.Id == review.ProductId);

            if (product == null)
                return NotFound();


            ProductReview productReview = new ProductReview
            {
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow,
                Email = review.Email,
                FullName = review.FullName,
                Rate = review.Rate,
                ProductId = review.ProductId,
                Message = review.Message
            };

            product.ProductReviews.Add(productReview);
            product.Rate = product.ProductReviews.Sum(x => x.Rate) / product.ProductReviews.Count();

            await _context.SaveChangesAsync();

            return RedirectToAction("index");
        }
    }
}
