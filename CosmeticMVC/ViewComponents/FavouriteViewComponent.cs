using CosmeticMVC.Helpers;
using CosmeticMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CosmeticMVC.ViewComponents
{
    public class FavouriteViewComponent : ViewComponent
    { public IViewComponentResult Invoke()
        {
            var fav = HttpContext.Session.Get<List<ListFavoriteVM>>(MySetting.FAVOURITE_KEY) ?? new List<ListFavoriteVM>();
            return View("FavouritePanel", new FavouriteModel
            {
                Quantity = fav.Count
            });
        }
    }
}
