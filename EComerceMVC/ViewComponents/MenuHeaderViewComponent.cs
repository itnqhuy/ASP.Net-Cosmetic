using EComerceMVC.Data;
using EComerceMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EComerceMVC.ViewComponents
{
    public class MenuHeaderViewComponent: ViewComponent
    {
        private readonly CosmeticContext db;

        public MenuHeaderViewComponent(CosmeticContext context) => db = context;

        public IViewComponentResult Invoke()
        {
            var data = db.Menus.Select(mn => new MenuHeaderVM
            {
                Id = mn.IdMenu.ToString(), 
                Name = mn.Name, 
                Link = mn.Link
            });

            return View(data);
        }

    }
}
