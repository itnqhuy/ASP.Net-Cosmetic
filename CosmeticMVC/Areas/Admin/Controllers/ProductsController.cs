using CosmeticMVC.Areas.Admin.ViewModels;
using CosmeticMVC.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System;
using CosmeticMVC.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace CosmeticMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(AuthenticationSchemes = "StaffCookie")]
    public class ProductsController : Controller
    {
        private readonly CosmeticContext _context;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _env;

        public ProductsController(CosmeticContext context, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            return View(await _context.Products.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.IdBrandNavigation)
                .Include(p => p.IdCategoryNavigation)
                .Include(p => p.IdImageNavigation)
                .FirstOrDefaultAsync(m => m.IdProduct == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }


        // GET: Products/Create
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["IdBrand"] = new SelectList(_context.Brands, "Id", "Name");
            ViewData["IdCategory"] = new SelectList(_context.Categories, "IdCategory", "Name");
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateViewModel model, IFormFile Product)
        {
            // Lưu hình ảnh vào bảng Image
            string imageId = MyUtil.GenerateRandomId();  // Tạo ID ngẫu nhiên cho hình ảnh
            string imageName = MyUtil.uploadHinh(Product, "Product");


            // Lưu thông tin hình ảnh vào bảng Image
            var image = new Image
            {
                IdImage = imageId,
                Name = Product.FileName,
                Type = "jpg",
                Meta = "Image meta information",  // Bạn có thể thay đổi trường này nếu cần
                Datebegin = DateTime.Now
            };

            _context.Images.Add(image);
            await _context.SaveChangesAsync();

            // Tạo sản phẩm mới
            var product = new Product
            {
                IdProduct = MyUtil.GenerateSlug(model.Name),
                Name = model.Name,
                Description = model.Description,
                Price = (int)model.Price,
                Quantity = model.Quantity,
                Hide = false,
                Meta = MyUtil.GenerateSlug(model.Name),
                Order = 10,
                Datebegin = DateTime.Today,
                Exp = model.Exp.ToString(),
                TotalSold = 0,
                Mfg = DateTime.Today,
                IdBrand = model.IdBrand,
                IdCategory = model.IdCategory,
                IdImage = imageId
            };

            _context.Add(product);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

            ViewData["IdBrand"] = new SelectList(_context.Brands, "Id", "Name", model.IdBrand);
            ViewData["IdCategory"] = new SelectList(_context.Categories, "IdCategory", "Name", model.IdCategory);
            return View(model);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["IdBrand"] = new SelectList(_context.Brands, "Id", "Id", product.IdBrand);
            ViewData["IdCategory"] = new SelectList(_context.Categories, "IdCategory", "IdCategory", product.IdCategory);
            ViewData["IdImage"] = new SelectList(_context.Images, "IdImage", "IdImage", product.IdImage);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("IdProduct,Description,Name,Price,Quantity,TotalSold,Hide,Meta,Order,Datebegin,Exp,IdBrand,IdCategory,IdIngredient,IdImage")] Product product)
        {
            if (id != product.IdProduct)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.IdProduct))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdBrand"] = new SelectList(_context.Brands, "Id", "Id", product.IdBrand);
            ViewData["IdCategory"] = new SelectList(_context.Categories, "IdCategory", "IdCategory", product.IdCategory);
            ViewData["IdImage"] = new SelectList(_context.Images, "IdImage", "IdImage", product.IdImage);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.IdBrandNavigation)
                .Include(p => p.IdCategoryNavigation)
                .Include(p => p.IdImageNavigation)
                .FirstOrDefaultAsync(m => m.IdProduct == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(string id)
        {
            return _context.Products.Any(e => e.IdProduct == id);
        }
    }
}
