using CosmeticMVC.Data;
using CosmeticMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CosmeticMVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly CosmeticContext db;

        public ProductController(CosmeticContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Detail(String id)
        {
            ViewBag.ControllerName = "Product";
            var data = db.Products
                .Include(p => p.IdCategoryNavigation)
                .Include(p => p.IdIngredientNavigation)
                .Include(p => p.IdBrandNavigation)
                .Include(p => p.IdImageNavigation)
                .SingleOrDefault(p => p.IdProduct == id);
            if (data == null)
            {
                return Redirect("/404");
            }

            var result = new DetailProductVM()
            {
                Id = data.IdProduct,
                Name = data.Name,
                Price = data.Price,
                Image = $"{data.IdImageNavigation.Name.ToString()}.{data.IdImageNavigation.Type.ToString()}",                
                Description = data.Description,
                IdCategory = data.IdCategoryNavigation.IdCategory,
                NameCategory = data.IdCategoryNavigation.Name,
                NameIngredient = data.IdIngredientNavigation.Name,
                DetailIngredient = data.IdIngredientNavigation.DetailText,
                NameBrand = data.IdBrandNavigation.Name,
                Count = data.Price,
                Exp = data.Exp,
                Mfg = data.Mfg.ToString(),

            };
 
            return View(result);
        }




    }
}
