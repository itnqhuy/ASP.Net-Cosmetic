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
    public class FavouritesController : Controller
    {
        private readonly CosmeticContext _context;

        public FavouritesController(CosmeticContext context)
        {
            _context = context;
        }

        // GET: Favourites
        public async Task<IActionResult> Index()
        {
            var cosmeticContext = _context.Favourites.Include(f => f.IdCustomerNavigation).Include(f => f.IdProductNavigation);
            return View(await cosmeticContext.ToListAsync());
        }

        // GET: Favourites/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var favourite = await _context.Favourites
                .Include(f => f.IdCustomerNavigation)
                .Include(f => f.IdProductNavigation)
                .FirstOrDefaultAsync(m => m.IdFavourite == id);
            if (favourite == null)
            {
                return NotFound();
            }

            return View(favourite);
        }

        // GET: Favourites/Create
        public IActionResult Create()
        {
            ViewData["IdCustomer"] = new SelectList(_context.Customers, "IdCustomer", "IdCustomer");
            ViewData["IdProduct"] = new SelectList(_context.Products, "IdProduct", "IdProduct");
            return View();
        }

        // POST: Favourites/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdFavourite,DaySelect,Description,IdProduct,IdCustomer")] Favourite favourite)
        {
            if (ModelState.IsValid)
            {
                _context.Add(favourite);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCustomer"] = new SelectList(_context.Customers, "IdCustomer", "IdCustomer", favourite.IdCustomer);
            ViewData["IdProduct"] = new SelectList(_context.Products, "IdProduct", "IdProduct", favourite.IdProduct);
            return View(favourite);
        }

        // GET: Favourites/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var favourite = await _context.Favourites.FindAsync(id);
            if (favourite == null)
            {
                return NotFound();
            }
            ViewData["IdCustomer"] = new SelectList(_context.Customers, "IdCustomer", "IdCustomer", favourite.IdCustomer);
            ViewData["IdProduct"] = new SelectList(_context.Products, "IdProduct", "IdProduct", favourite.IdProduct);
            return View(favourite);
        }

        // POST: Favourites/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("IdFavourite,DaySelect,Description,IdProduct,IdCustomer")] Favourite favourite)
        {
            if (id != favourite.IdFavourite)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(favourite);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FavouriteExists(favourite.IdFavourite))
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
            ViewData["IdCustomer"] = new SelectList(_context.Customers, "IdCustomer", "IdCustomer", favourite.IdCustomer);
            ViewData["IdProduct"] = new SelectList(_context.Products, "IdProduct", "IdProduct", favourite.IdProduct);
            return View(favourite);
        }

        // GET: Favourites/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var favourite = await _context.Favourites
                .Include(f => f.IdCustomerNavigation)
                .Include(f => f.IdProductNavigation)
                .FirstOrDefaultAsync(m => m.IdFavourite == id);
            if (favourite == null)
            {
                return NotFound();
            }

            return View(favourite);
        }

        // POST: Favourites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var favourite = await _context.Favourites.FindAsync(id);
            if (favourite != null)
            {
                _context.Favourites.Remove(favourite);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FavouriteExists(string id)
        {
            return _context.Favourites.Any(e => e.IdFavourite == id);
        }
    }
}
