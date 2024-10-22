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
    public class CartItemsController : Controller
    {
        private readonly CosmeticContext _context;

        public CartItemsController(CosmeticContext context)
        {
            _context = context;
        }

        // GET: CartItems
        public async Task<IActionResult> Index()
        {
            var cosmeticContext = _context.CartItems.Include(c => c.IdCustomerNavigation).Include(c => c.IdProductNavigation);
            return View(await cosmeticContext.ToListAsync());
        }

        // GET: CartItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartItem = await _context.CartItems
                .Include(c => c.IdCustomerNavigation)
                .Include(c => c.IdProductNavigation)
                .FirstOrDefaultAsync(m => m.IdCarditem == id);
            if (cartItem == null)
            {
                return NotFound();
            }

            return View(cartItem);
        }

        // GET: CartItems/Create
        public IActionResult Create()
        {
            ViewData["IdCustomer"] = new SelectList(_context.Customers, "IdCustomer", "IdCustomer");
            ViewData["IdProduct"] = new SelectList(_context.Products, "IdProduct", "IdProduct");
            return View();
        }

        // POST: CartItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCarditem,Quantity,IdProduct,IdCustomer")] CartItem cartItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cartItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCustomer"] = new SelectList(_context.Customers, "IdCustomer", "IdCustomer", cartItem.IdCustomer);
            ViewData["IdProduct"] = new SelectList(_context.Products, "IdProduct", "IdProduct", cartItem.IdProduct);
            return View(cartItem);
        }

        // GET: CartItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartItem = await _context.CartItems.FindAsync(id);
            if (cartItem == null)
            {
                return NotFound();
            }
            ViewData["IdCustomer"] = new SelectList(_context.Customers, "IdCustomer", "IdCustomer", cartItem.IdCustomer);
            ViewData["IdProduct"] = new SelectList(_context.Products, "IdProduct", "IdProduct", cartItem.IdProduct);
            return View(cartItem);
        }

        // POST: CartItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCarditem,Quantity,IdProduct,IdCustomer")] CartItem cartItem)
        {
            if (id != cartItem.IdCarditem)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cartItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartItemExists(cartItem.IdCarditem))
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
            ViewData["IdCustomer"] = new SelectList(_context.Customers, "IdCustomer", "IdCustomer", cartItem.IdCustomer);
            ViewData["IdProduct"] = new SelectList(_context.Products, "IdProduct", "IdProduct", cartItem.IdProduct);
            return View(cartItem);
        }

        // GET: CartItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartItem = await _context.CartItems
                .Include(c => c.IdCustomerNavigation)
                .Include(c => c.IdProductNavigation)
                .FirstOrDefaultAsync(m => m.IdCarditem == id);
            if (cartItem == null)
            {
                return NotFound();
            }

            return View(cartItem);
        }

        // POST: CartItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cartItem = await _context.CartItems.FindAsync(id);
            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartItemExists(int id)
        {
            return _context.CartItems.Any(e => e.IdCarditem == id);
        }
    }
}
