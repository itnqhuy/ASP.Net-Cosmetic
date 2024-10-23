using CosmeticMVC.Data;
using CosmeticMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CosmeticMVC.ViewComponents
{
    public class ListRateViewComponent : ViewComponent
    {
        private readonly CosmeticContext db;

        public ListRateViewComponent(CosmeticContext context) => db = context;

        public IViewComponentResult Invoke(String idProduct)
        {
            var rates = db.Rates.AsQueryable();
            if (!string.IsNullOrWhiteSpace(idProduct))
            {
                rates = rates.Where(p => p.IdProduct.Equals(idProduct));
            }

            var result = (from r in rates
                join p in db.Products on r.IdProduct equals p.IdProduct
                join c in db.Customers on r.IdCustomer equals c.IdCustomer
                join img in db.Images on r.Image equals img.IdImage
                select new ListRateVM()
                {
                    IdRate = r.IdRate,
                    Star = r.Star,
                    Description = r.Description,
                    NameCustomer = c.IdCustomer,
                    Image = $"{img.Name.ToString()}.{img.Type.ToString()}",
                    Hide = r.Hide,
                    DateBegin = r.Datebegin
                }).ToList();

            return View(result);
        }
    }
}
