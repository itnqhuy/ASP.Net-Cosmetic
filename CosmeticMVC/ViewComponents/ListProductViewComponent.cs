using CosmeticMVC.Data;
using CosmeticMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CosmeticMVC.ViewComponents
{
    public class ListProductViewComponent : ViewComponent
    {
        private readonly CosmeticContext db;

        public ListProductViewComponent(CosmeticContext context) => db = context;

        public IViewComponentResult Invoke(bool isDiscount = false)
        {
            var products = db.Products.AsQueryable();
            var categoryCount = db.Categories.Count();
            var data = (from pr in products
                join img in db.Images on pr.IdImage equals img.IdImage
                select new ListProductVM
                {
                Id = pr.IdProduct.ToString(),
                Name = pr.Name.ToString(),
                Description = pr.Description,
                Price = (int)pr.Price,
                IdCategory = pr.IdCategory,
                DiscountedPrice = isDiscount ? (int)pr.Price - 20 : (int?)null,
                Count = categoryCount
                });

            return View(isDiscount ? "Discount" : "Default", data);
        }

    }
}