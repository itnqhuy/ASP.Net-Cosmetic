using Azure;
using CosmeticMVC.Data;
using CosmeticMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using X.PagedList;

namespace CosmeticMVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CosmeticContext db;

        public CategoryController(CosmeticContext context)
        {
            db = context;
        }

        public IActionResult Index(string? category, int? page)
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

            ViewBag.Count = result.Count;
            int pageSize = 6;
            int pageNumber = page==null||page<0 ?1 : page.Value;
            PagedList<ListProductVM> lst = new PagedList<ListProductVM>(result, pageNumber, pageSize);

            return View(lst);
        }

        public IActionResult FindBrand(string? brand, int? page)
        {
            ViewBag.ControllerName = "Category";
            var products = db.Products.AsQueryable();
            if (!string.IsNullOrWhiteSpace(brand))
            {
                products = products.Where(p => p.IdBrand.Equals(brand));
            }

            var categoryCount = db.Brands.Count();

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
                Count = categoryCount
            }).ToList();

            int pageSize = 6;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            PagedList<ListProductVM> lst = new PagedList<ListProductVM>(result, pageNumber, pageSize);

            return View(lst);
        }

        public IActionResult Search(string? query, int? page)
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

            int pageSize = 6;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            PagedList<ListProductVM> lst = new PagedList<ListProductVM>(result, pageNumber, pageSize);

            return View(lst);
        }
    }
}