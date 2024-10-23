using CosmeticMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using CosmeticMVC.Helpers;

namespace CosmeticMVC.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var cart = HttpContext.Session.Get<List<ListCartItemVM>>(MySetting.CART_KEY) ?? new List<ListCartItemVM>();
            return View("CartPanel",new CartModel
            {
                Quantity = cart.Count,
                Total = cart.Sum(p => p.SubTotalCartItem)
            });
        }
    }
}