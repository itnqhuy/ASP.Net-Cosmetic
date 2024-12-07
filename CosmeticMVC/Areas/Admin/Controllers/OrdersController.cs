using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CosmeticMVC.Data;
using Microsoft.AspNetCore.Authorization;
using CosmeticMVC.Helpers;
using CosmeticMVC.ViewModels;

namespace CosmeticMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(AuthenticationSchemes = "StaffCookie")]
    public class OrdersController : Controller
    {
        private readonly CosmeticContext _context;

        public OrdersController(CosmeticContext context)
        {
            _context = context;
        }

        // GET: Admin/Orders
        [HttpGet]
        public IActionResult Index()
        {
            var orders = _context.Orders
                .Select(o => new ListOrderVM
                {
                    IdOrder = o.IdOrder,
                    ReceiverName = o.ReceiverName,
                    ModifiedAt = o.ModifiedAt,
                    Paymethod = o.Paymethod,
                    Shipcost = o.Shipcost,
                    IdStatus = 1,
                    TotalAmount = (decimal)o.OrderDetails.Sum(ct => ct.Quantity * ct.UnitPrice)
                })
                .ToList();

            return View(orders);
        }

        // GET: Admin/Orders/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.IdStaffNavigation)
                .FirstOrDefaultAsync(m => m.IdOrder == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Admin/Orders/Create
        public IActionResult Create()
        {
            ViewData["IdStaff"] = new SelectList(_context.Staff, "IdStaff", "IdStaff");
            return View();
        }

        // POST: Admin/Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdOrder,ReceiverAddress,ReceiverName,ReceiverPhone,Paymethod,ModifiedAt,Shipcost,Note,Datebegin,IdStaff")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdStaff"] = new SelectList(_context.Staff, "IdStaff", "IdStaff", order.IdStaff);
            return View(order);
        }

        // GET: Admin/Orders/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["IdStaff"] = new SelectList(_context.Staff, "IdStaff", "IdStaff", order.IdStaff);
            return View(order);
        }

        // POST: Admin/Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("IdOrder,ReceiverAddress,ReceiverName,ReceiverPhone,Paymethod,ModifiedAt,Shipcost,Note,Datebegin,IdStaff")] Order order)
        {
            if (id != order.IdOrder)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.IdOrder))
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
            ViewData["IdStaff"] = new SelectList(_context.Staff, "IdStaff", "IdStaff", order.IdStaff);
            return View(order);
        }

        // GET: Admin/Orders/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.IdStaffNavigation)
                .FirstOrDefaultAsync(m => m.IdOrder == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Admin/Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(string id)
        {
            return _context.Orders.Any(e => e.IdOrder == id);
        }


        public IActionResult Revenue()
        {
            return View();
        }
    }
}
