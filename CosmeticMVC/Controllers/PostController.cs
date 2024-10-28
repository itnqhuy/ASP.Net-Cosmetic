using Azure;
using CosmeticMVC.Data;
using CosmeticMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CosmeticMVC.Controllers
{
    public class PostController : Controller
    {
        private readonly CosmeticContext db;

        public PostController(CosmeticContext context)
        {
            db = context;
        }
        public IActionResult Index(string? category, int? page)
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
                    Id_post = p.IdPost,
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

            int pageSize = 6;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            PagedList<ListPostVM> lst = new PagedList<ListPostVM>(result, pageNumber, pageSize);

            return View(lst);
        }

        public IActionResult Search(string? query, int? page)
        {
            ViewBag.ControllerName = "Post";
            var posts = db.Posts.AsQueryable();
            if (query != null)
            {
                posts = posts.Where(p => p.Content.Contains(query));
            }

            var result = posts.Select(p => new ListPostVM()
            {
                Id_post = p.IdProduct,
                Content = p.Content,
                Description = p.Description,
                Hide = p.Hide,
                Thumbail = p.Thumbail,
                Meta = p.Meta,
                Image = $"{p.IdProductNavigation.IdImageNavigation.Name.ToString()}.{p.IdProductNavigation.IdImageNavigation.Type.ToString()}",
                Modified_at = p.ModifiedAt,
                Datebegin = p.Datebegin,
                Name_product = p.IdProductNavigation.Name
            }).ToList();

            int pageSize = 6;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            PagedList<ListPostVM> lst = new PagedList<ListPostVM>(result, pageNumber, pageSize);

            return View(lst);
        }

        public IActionResult Detail(String id)
        {
            ViewBag.ControllerName = "Post";
            var p = db.Posts
                .Include(p => p.IdProductNavigation)
                .Include(p => p.IdProductNavigation.IdImageNavigation)
                .SingleOrDefault(p => p.IdPost == id);

            if (p == null)
            {
                return Redirect("/404");
            }

            var result = new DetailPostVM()
            {
                Id_post = p.IdProduct,
                Content = p.Content,
                Description = p.Description,
                Hide = p.Hide,
                Thumbail = p.Thumbail,
                Meta = p.Meta,
                Image = $"{p.IdProductNavigation.IdImageNavigation.Name.ToString()}.{p.IdProductNavigation.IdImageNavigation.Type.ToString()}",
                Modified_at = p.ModifiedAt,
                Datebegin = p.Datebegin,
                Name_product = p.IdProductNavigation.Name

            };

            return View(result);
        }

        public IActionResult Add()
        {
            return RedirectToAction("Create","Posts");
        }
    }
}
