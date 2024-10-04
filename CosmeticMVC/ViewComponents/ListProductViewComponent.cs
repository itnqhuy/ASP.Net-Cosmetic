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
            var data = (from pr in db.Products
                join cat in db.Categories on pr.IdCatrgory equals cat.IdCategory
                select new ListProductVM()
                {
                    Id = pr.IdProduct.ToString(),
                    Name = pr.Name.ToString(),
                    Description = pr.Description,
                    Price = (int)pr.Price,
                    Category = cat.Name,
                    DiscountedPrice = isDiscount ? (int)pr.Price - 20 : (int?)null, // Giảm giá nếu isDiscount là true
                    Count = db.Products.Count(p => p.IdProduct == pr.IdProduct) // Đếm số lượng sản phẩm có cùng IdProduct
                }).ToList();

            return View(isDiscount ? "Discount" : "Default", data); // Gọi view dựa trên isDiscount
        }

    }
}