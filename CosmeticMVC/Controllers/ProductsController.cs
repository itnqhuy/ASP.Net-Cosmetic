using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CosmeticMVC.Data;

namespace CosmeticMVC.Controllers
{
    public class ProductsController : Controller
    {
        private readonly CosmeticContext _context;

        public ProductsController(CosmeticContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var cosmeticContext = _context.Products.Include(p => p.IdBrandNavigation).Include(p => p.IdCatrgoryNavigation).Include(p => p.IdImageNavigation).Include(p => p.IdIngredientNavigation);
            return View(await cosmeticContext.ToListAsync());
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
                .Include(p => p.IdCatrgoryNavigation)
                .Include(p => p.IdImageNavigation)
                .Include(p => p.IdIngredientNavigation)
                .FirstOrDefaultAsync(m => m.IdProduct == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["IdBrand"] = new SelectList(_context.Brands, "Id", "Id");
            ViewData["IdCatrgory"] = new SelectList(_context.Categories, "IdCategory", "IdCategory");
            ViewData["IdImage"] = new SelectList(_context.Images, "IdImage", "IdImage");
            ViewData["IdIngredient"] = new SelectList(_context.Ingredients, "IdIngredient", "IdIngredient");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProduct,Description,Name,Price,Quantity,TotalSold,Hide,Meta,Order,Datebegin,Exp,IdBrand,IdCatrgory,IdIngredient,IdImage")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdBrand"] = new SelectList(_context.Brands, "Id", "Id", product.IdBrand);
            ViewData["IdCatrgory"] = new SelectList(_context.Categories, "IdCategory", "IdCategory", product.IdCatrgory);
            ViewData["IdImage"] = new SelectList(_context.Images, "IdImage", "IdImage", product.IdImage);
            ViewData["IdIngredient"] = new SelectList(_context.Ingredients, "IdIngredient", "IdIngredient", product.IdIngredient);
            return View(product);
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
            ViewData["IdCatrgory"] = new SelectList(_context.Categories, "IdCategory", "IdCategory", product.IdCatrgory);
            ViewData["IdImage"] = new SelectList(_context.Images, "IdImage", "IdImage", product.IdImage);
            ViewData["IdIngredient"] = new SelectList(_context.Ingredients, "IdIngredient", "IdIngredient", product.IdIngredient);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("IdProduct,Description,Name,Price,Quantity,TotalSold,Hide,Meta,Order,Datebegin,Exp,IdBrand,IdCatrgory,IdIngredient,IdImage")] Product product)
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
            ViewData["IdCatrgory"] = new SelectList(_context.Categories, "IdCategory", "IdCategory", product.IdCatrgory);
            ViewData["IdImage"] = new SelectList(_context.Images, "IdImage", "IdImage", product.IdImage);
            ViewData["IdIngredient"] = new SelectList(_context.Ingredients, "IdIngredient", "IdIngredient", product.IdIngredient);
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
                .Include(p => p.IdCatrgoryNavigation)
                .Include(p => p.IdImageNavigation)
                .Include(p => p.IdIngredientNavigation)
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
