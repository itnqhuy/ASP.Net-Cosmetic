using CosmeticMVC.Data;
using CosmeticMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CosmeticMVC.Controllers
{
    public class PostController : Controller
    {
        private readonly CosmeticContext db;

        public PostController(CosmeticContext context)
        {
            db = context;
        }
        public IActionResult Index(string? category)
        {
            ViewBag.ControllerName = "Post";
            var posts = db.Posts.AsQueryable();
            if (!string.IsNullOrWhiteSpace(category))
            {
                posts = posts
                    .Where(p => db.Products
                        .Any(product => product.IdProduct.Equals(p.IdProduct)
                                        && product.IdCategory.ToString().Equals(category)));

            }

            var result = (from p in posts
                join prod in db.Products on p.IdProduct equals prod.IdProduct
                join img in db.Images on prod.IdImage equals img.IdImage
                          select new ListPostVM()
                {
                    Id_post = p.IdProduct,
                    Content = p.Content,
                    Description = p.Description,
                    Hide = p.Hide,
                    Thumbail = p.Thumbail,
                    Meta = p.Meta,
                    Image = $"{img.Name.ToString()}.{img.Type.ToString()}",
                    Modified_at = p.ModifiedAt,
                    Datebegin = p.Datebegin,
                    Name_product = prod.Name
                }).ToList();

            return View(result);
        }
    }
}
