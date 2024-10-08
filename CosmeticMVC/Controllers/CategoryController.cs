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

            var result = (from p in products
                join c in db.Categories on p.IdCategory equals c.IdCategory
                join img in db.Images on p.IdImage equals img.IdImage
                          select new ListProductVM
                {
                    Id = p.IdProduct,
                    Name = p.Name,
                    Description = p.Description,
                    IdCategory = p.IdCategory,
                    NameCategory = c.Name,
                    Price = p.Price,
                    Image = $"{img.Name.ToString()}.{img.Type.ToString()}",
                    DiscountedPrice = p.Price *8/10,
                    Count = categoryCount
                }).ToList();

            return View(result);
        }
    }
}