using CosmeticMVC.Data;
using CosmeticMVC.Helpers;
using CosmeticMVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CosmeticMVC.Controllers
{
    [Authorize(AuthenticationSchemes = "CustomerCookie")]
    public class OrderController : Controller
    {
        private readonly CosmeticContext db;

        public OrderController(CosmeticContext context)
        {
            db = context;
        }

        public List<ListCartItemVM> Cart => HttpContext.Session.Get<List<ListCartItemVM>>(MySetting.CART_KEY) ??
                                            new List<ListCartItemVM>();

        [Authorize(Roles = "Customer")]
        [HttpGet]
        public IActionResult Index()
        {
            var customerId = HttpContext.User.Claims.SingleOrDefault(p => p.Type == MySetting.CLAIM_CUSTOMERID)?.Value;

            if (customerId == null)
            {
                return RedirectToAction("Login", "Customer");
            }

            var orders = db.Orders
                .Where(o => o.IdCustomer == customerId)
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

    }
}

