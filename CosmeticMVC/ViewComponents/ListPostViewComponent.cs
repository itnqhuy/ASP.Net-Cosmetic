using CosmeticMVC.Data;
using CosmeticMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CosmeticMVC.ViewComponents
{
    public class ListPostViewComponent: ViewComponent
    {
        private readonly CosmeticContext db;

        public ListPostViewComponent(CosmeticContext context) => db = context;

        public IViewComponentResult Invoke()
        {
            var data = (from p in db.Posts
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
                    Modified_at = p.ModifiedAt,
                    Datebegin = p.Datebegin,
                    Order = p.Order,

                }).ToList();

            return View(data);
        }
    }
}
