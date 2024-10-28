using CosmeticMVC.Helpers;
using CosmeticMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CosmeticMVC.Controllers
{
    public class OrderController : Controller
    {
        public List<ListCartItemVM> Cart => HttpContext.Session.Get<List<ListCartItemVM>>(MySetting.CART_KEY) ?? new List<ListCartItemVM>();

        public IActionResult CheckOut()
        {
            return View(Cart);
        }
    }
}
