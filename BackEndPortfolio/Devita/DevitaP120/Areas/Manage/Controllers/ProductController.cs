using DevitaP120.Areas.Manage.ViewModels;
using DevitaP120.Data;
using DevitaP120.Data.Entities;
using DevitaP120.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevitaP120.Areas.Manage.Controllers
{
    [Area("manage")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ProductController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            ProductViewModel productVM = new ProductViewModel
            {
                Products = await _context.Products.Include(x => x.Category).ToListAsync()
            };

            return View(productVM);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _context.Categories.Where(c => !c.IsDeleted).ToListAsync();
            ViewBag.Tags = await _context.Tags.ToListAsync();

            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (await _context.Products.AnyAsync(x => x.Slug.ToLower() == product.Slug.Trim().ToLower()))
            {
                ModelState.AddModelError("Slug", "Product already exist!");
                return View();
            }

            if (!await _context.Categories.AnyAsync(c => c.Id == product.CategoryId && !c.IsDeleted))
            {
                ModelState.AddModelError("CategoryId", "Category not found!");
                return View();
            }

            product.ProductTags = await _createProductTags(product.TagIds);

            product.ProductPhotos = new List<ProductPhoto>();
            int photoOrder = 1;
            foreach (var file in product.Files)
            {

                ProductPhoto productPhoto = new ProductPhoto();
                try
                {
                    productPhoto = _createProductPhoto(photoOrder, file);
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("Files", e.Message);
                }
                product.ProductPhotos.Add(productPhoto);
                photoOrder++;
            }

            product.CreatedAt = DateTime.UtcNow;
                product.ModifiedAt = DateTime.UtcNow;
                product.DiscountedPrice = product.DiscountPercent <= 0 ? product.Price : (product.Price * (100 - product.DiscountPercent) / 100);
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();


                return RedirectToAction("index");
            }
        
        public async Task<IActionResult> Edit(int id)
        {
            Product product = await _context.Products
                .Include(x=>x.ProductTags)
                .Include(x=>x.ProductPhotos)
                .FirstOrDefaultAsync(x => x.Id == id);

            if(product == null)
            {
                return NotFound();
            }

            ViewBag.Categories = await _context.Categories.Where(c => !c.IsDeleted).ToListAsync();
            ViewBag.Tags = await _context.Tags.ToListAsync();

            return View(product);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult>Edit(Product product)
        {
            Product existProduct = await _context.Products
                .Include(x=>x.ProductPhotos).Include(x=>x.ProductTags)
                .FirstOrDefaultAsync(x => x.Id == product.Id);

            if (existProduct == null)
                return NotFound();

            if (!await _context.Categories.AnyAsync(x => x.Id == product.CategoryId))
                return NotFound();

            if(await _context.Products.AnyAsync(x=>x.Slug.ToLower() == product.Slug.ToLower() && x.Id != product.Id))
                return NotFound();

            existProduct.ProductTags = await _getUpdatedProductTagsAsync(existProduct.ProductTags, product.TagIds, product.Id);

            List<ProductPhoto> removablePhotos = new List<ProductPhoto>();
            foreach (var item in existProduct.ProductPhotos)
            {
                if (product.FileIds.Any(x => x == item.Id))
                    continue;

                FileManager.Delete(_env.WebRootPath, "uploads/products", item.Name);
                removablePhotos.Add(item);
            }
            existProduct.ProductPhotos = existProduct.ProductPhotos.Except(removablePhotos).ToList();

            var lastPhoto = existProduct.ProductPhotos.OrderByDescending(x => x.Order).FirstOrDefault();
            int photoOrder = lastPhoto != null ? lastPhoto.Order + 1 : 1;
           
            foreach (var file in product.Files)
            {
                ProductPhoto productPhoto = new ProductPhoto();
                try
                {
                    productPhoto =  _createProductPhoto(photoOrder, file);
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("Files", e.Message);
                }
                existProduct.ProductPhotos.Add(productPhoto);
                photoOrder++;
            }


            if (existProduct.Price != product.Price || existProduct.DiscountPercent != product.DiscountPercent)
            {
                existProduct.DiscountedPrice = product.DiscountPercent <= 0 ? product.Price : (product.Price * (100 - product.DiscountPercent) / 100);
            }

            existProduct.CategoryId = product.CategoryId;
            existProduct.Price = product.Price;
            existProduct.ProducingPrice = product.ProducingPrice;
            existProduct.DiscountPercent = product.DiscountPercent;
            existProduct.Desc = product.Desc;
            existProduct.InfoText = product.InfoText;
            existProduct.Name = product.Name;
            existProduct.Slug = product.Slug;
            existProduct.IsAvailable = product.IsAvailable;

            existProduct.ModifiedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();


            return RedirectToAction("index");
        }

        private ProductPhoto _createProductPhoto(int order, IFormFile file)
        {
            #region CheckFileLength
            if (file.Length > 2 * (1024 * 1024))
            {
                throw new Exception("Sekil olcusu 2MB-dan boyuk ola bilmez ,qaqa!!!");
            }
            #endregion

            #region CheckFileContentType
            if (file.ContentType != "image/png" && file.ContentType != "image/jpeg")
            {
                throw new Exception("File png, jpg, jpeg formatinda olmalidir!");
            }
            #endregion            

            string filename = FileManager.Save(_env.WebRootPath, "uploads/products", file);

            ProductPhoto productPhoto = new ProductPhoto
            {
                Name = filename,
                Order = order,
            };

            return productPhoto;
        }
        private async Task<List<ProductTag>> _getUpdatedProductTagsAsync (List<ProductTag> productTags,int[] tagIds,int productId)
        {
            List<ProductTag> removableTags = new List<ProductTag>();
            removableTags.AddRange(productTags);

            foreach (var tagId in tagIds)
            {
                ProductTag tag = productTags.FirstOrDefault(x => x.TagId == tagId);

                if (tag != null)
                {
                    removableTags.Remove(tag);
                }
                else
                {
                    if (!await _context.Tags.AnyAsync(x => x.Id == tagId))
                    {
                        throw new Exception("Tag not found!");
                    }

                    tag = new ProductTag
                    {
                        TagId = tagId,
                        ProductId = productId
                    };

                    productTags.Add(tag);
                }
            }

            productTags = productTags.Except(removableTags).ToList();

            return productTags;
        }
        private async Task<List<ProductTag>> _createProductTags(int[] tagIds)
        {

            List<ProductTag> productTags = new List<ProductTag>();
            foreach (var tagId in tagIds)
            {
                if (!await _context.Tags.AnyAsync(x => x.Id == tagId))
                {
                    throw new Exception("Tag not found!");
                }

                ProductTag productTag = new ProductTag
                {
                    TagId = tagId
                };

                productTags.Add(productTag);
            }

            return productTags;
        }


        public async Task<IActionResult> Review(int productId)
        {
            List<ProductReview> productReviews = await _context.ProductReviews.Where(x => x.ProductId == productId).ToListAsync();

            return View(productReviews);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> DeleteReview(int id)
        {
            ProductReview review = await _context.ProductReviews.FirstOrDefaultAsync(x => x.Id == id);
            Product product = await _context.Products.Include(x=>x.ProductReviews).FirstOrDefaultAsync(x => x.Id == review.ProductId);

            product.Rate = (product.ProductReviews.Sum(x => x.Rate) - review.Rate) / (product.ProductReviews.Count() - 1);

            if (review == null)
                return NotFound();

            _context.ProductReviews.Remove(review);
            await _context.SaveChangesAsync();

            return RedirectToAction("index");
        }
    }
}
