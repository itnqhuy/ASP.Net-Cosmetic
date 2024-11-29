using CosmeticMVC.Data;
using CosmeticMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CosmeticMVC.Controllers
{
    [Route("san-pham")]
    public class ProductController : Controller
    {
        private readonly CosmeticContext db;

        public ProductController(CosmeticContext context)
        {
            db = context;
        }

        [Route("{meta}/{id}")]
        public IActionResult Detail(String meta, String id)
        {
            ViewBag.ControllerName = "Product";
            var data = db.Products
                .Include(p => p.IdCategoryNavigation)
                .Include(p => p.IdBrandNavigation)
                .Include(p => p.IdImageNavigation)
                .SingleOrDefault(p => p.IdProduct == id);

            if (data == null)
            {
                return Redirect("/404");
            }

            if (data.Hide == true)
            {
                return Redirect("/404");
            }

            var result = new ListProductVM.DetailProductVM()
            {
                Id = data.IdProduct,
                Name = data.Name,
                Price = data.Price,
                Image = data.IdImageNavigation.Name.ToString(),                
                Description = data.Description,
                IdCategory = data.IdCategoryNavigation.IdCategory,
                NameCategory = data.IdCategoryNavigation.Name,
                Meta = data.Meta,
                NameBrand = data.IdBrandNavigation.Name,
                Count = data.Price,
                Exp = data.Exp,
                Mfg = data.Mfg.ToString(),

            };

            // Lấy danh sách sản phẩm liên quan có cùng loại
            var relatedProducts = db.Products
                .Include(p => p.IdImageNavigation)
                .Where(p => p.IdCategory == data.IdCategory && p.IdProduct != data.IdProduct)
                .Select(p => new ListProductVM
                {
                    Id = p.IdProduct,
                    Name = p.Name,
                    Price = p.Price,
                    Image = p.IdImageNavigation.Name
                }).Take(4).ToList(); // Lấy tối đa 4 sản phẩm

            ViewBag.RelatedProducts = relatedProducts;

            return View(result);
        }
    }
}
