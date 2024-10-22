using CosmeticMVC.Data;
using CosmeticMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CosmeticMVC.Helpers;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CosmeticMVC.Controllers
{
    public class CartItemController : Controller
    {
        private readonly CosmeticContext db;

        public CartItemController(CosmeticContext context)
        {
            db = context;
        }

        public List<ListCartItemVM> Cart => HttpContext.Session.Get<List<ListCartItemVM>>(MySetting.CART_KEY) ?? new List<ListCartItemVM>();
        public IActionResult Index()
        {
            return View(Cart);
        }

        public IActionResult AddToCartItem(string id, int quantity = 1)
        {
            var gioHang = Cart;
            var item = gioHang.SingleOrDefault(p => p.IdProduct == id);
            if (item == null)
            {
                var hangHoa = db.Products.Include(p => p.IdImageNavigation).SingleOrDefault(p => p.IdProduct == id);
                if (hangHoa == null)
                {
                    TempData["Message"] = $"Không tìm thấy hàng hóa có mã {id}";
                    return Redirect("/404");
                }

                item = new ListCartItemVM
                {
                    IdProduct = id,
                    Quantity = quantity,
                    ProductImage =
                        $"{hangHoa.IdImageNavigation.Name.ToString()}.{hangHoa.IdImageNavigation.Type.ToString()}",
                    ProductName = hangHoa.Name,
                    Price = hangHoa.Price,
                    TotalProduct = hangHoa.Price * quantity,
                };
                gioHang.Add(item);
            }
            else
            {
                item.Quantity += quantity;
                item.TotalProduct = item.Price * item.Quantity;

            }

            HttpContext.Session.Set(MySetting.CART_KEY, gioHang);
            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromCartItem(string id)
        {
            var gioHang = Cart;
            var item = gioHang.SingleOrDefault(p => p.IdProduct == id);
            if (item != null)
            {
                gioHang.Remove(item);
                HttpContext.Session.Set(MySetting.CART_KEY,gioHang);
            }

            return RedirectToAction("Index");

        }
    }
}