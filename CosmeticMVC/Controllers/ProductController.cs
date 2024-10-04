using Microsoft.AspNetCore.Mvc;

namespace CosmeticMVC.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
