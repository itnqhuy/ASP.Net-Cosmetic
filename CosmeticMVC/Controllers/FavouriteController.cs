using CosmeticMVC.Data;
using CosmeticMVC.Helpers;
using CosmeticMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CosmeticMVC.Controllers
{
    public class FavouriteController : Controller
    {
        private readonly CosmeticContext db;

        public FavouriteController(CosmeticContext context)
        {
            db = context;
        }

        public List<ListFavoriteVM> Favourite => HttpContext.Session.Get<List<ListFavoriteVM>>(MySetting.FAVOURITE_KEY) ?? new List<ListFavoriteVM>();

        public IActionResult Index()
        {
            return View(Favourite);
        }

        public IActionResult AddToCartItem(string id, int quantity = 1)
        {
            var yeuThich = Favourite;
            var item = yeuThich.SingleOrDefault(p => p.IdProduct == id);
            if (item == null)
            {
                var hangHoa = db.Products.Include(p => p.IdImageNavigation).SingleOrDefault(p => p.IdProduct == id);
                if (hangHoa == null)
                {
                    TempData["Message"] = $"Không tìm thấy hàng hóa có mã {id}";
                    return Redirect("/404");
                }

                item = new ListFavoriteVM()
                {
                    IdProduct = id,
                    ProductImage =
                        $"{hangHoa.IdImageNavigation.Name.ToString()}.{hangHoa.IdImageNavigation.Type.ToString()}",
                    ProductName = hangHoa.Name,
                    Price = hangHoa.Price,
                };
                yeuThich.Add(item);
            }

            HttpContext.Session.Set(MySetting.FAVOURITE_KEY, yeuThich);
            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromCartItem(string id)
        {
            var yeuThich = Favourite;
            var item = yeuThich.SingleOrDefault(p => p.IdProduct == id);
            if (item != null)
            {
                yeuThich.Remove(item);
                HttpContext.Session.Set(MySetting.FAVOURITE_KEY, yeuThich);
            }

            return RedirectToAction("Index");

        }
    }
}
