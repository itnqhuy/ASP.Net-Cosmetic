using CosmeticMVC.Data;
using CosmeticMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CosmeticMVC.ViewComponents
{
    public class MenuBrandViewComponent : ViewComponent
    {
        private readonly Data.CosmeticContext db;

        public MenuBrandViewComponent(CosmeticContext context) => db = context;

        public IViewComponentResult Invoke()
        {
            var data = db.Brands.Select(mn => new MenuBrandVM()
            {
                Id = mn.Id.ToString(),
                Name = mn.Name,
                Thumbnail = mn.Thumbnail,
                //Order = mn.Order,
                //Hide = mn.Hide
            });

            return View(data);
        }
    }
}
