using CosmeticMVC.Data;
using CosmeticMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CosmeticMVC.ViewComponents
{
    public class MenuCategoryViewComponent : ViewComponent
    {
        private readonly Data.CosmeticContext db;

        public MenuCategoryViewComponent(CosmeticContext context) => db = context;

        public IViewComponentResult Invoke()
        {
            var data = db.Categories.Select(mn => new MenuCategoryVM()
            {
                Id = mn.IdCategory.ToString(),
                Name = mn.Name,
                Meta = mn.Meta,
                Link = mn.Link,
                Hide = mn.Hide
            }).OrderBy(p => p.Name);

            return View(data);
        }
    }
}
