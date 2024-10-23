using CosmeticMVC.Data;
using CosmeticMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CosmeticMVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CosmeticContext db;

        public CategoryController(CosmeticContext context)
        {
            db = context;
        }

        public IActionResult Index(string? category)
        {
            ViewBag.ControllerName = "Category";
            var products = db.Products.AsQueryable();
            if (!string.IsNullOrWhiteSpace(category)) 
            {
                products = products.Where(p => p.IdCategory.Equals(category));
            }

            var categoryCount = db.Categories.Count(); 

            var result = products.Select(p => new ListProductVM
                {
                    Id = p.IdProduct,
                    Name = p.Name,
                    Description = p.Description,
                    IdCategory = p.IdCategory,
                    NameCategory = p.IdCategoryNavigation.Name,
                    Price = p.Price,
                    Image = $"{p.IdImageNavigation.Name.ToString()}.{p.IdImageNavigation.Type.ToString()}",
                    DiscountedPrice = p.Price *8/10,
                    Count = categoryCount
                }).ToList();

            return View(result);
        }

        public IActionResult Search(string? query)
        {
            var products = db.Products.AsQueryable();
            if (query != null)
            {
                products = products.Where(p => p.Name.Contains(query));
            }

            var result = products.Select(p => new ListProductVM
            {
                Id = p.IdProduct,
                Name = p.Name,
                Description = p.Description,
                IdCategory = p.IdCategory,
                NameCategory = p.IdCategoryNavigation.Name,
                Price = p.Price,
                Image = $"{p.IdImageNavigation.Name.ToString()}.{p.IdImageNavigation.Type.ToString()}",
                DiscountedPrice = p.Price * 8 / 10,
            }).ToList();

            return View(result);
        }
    }
}