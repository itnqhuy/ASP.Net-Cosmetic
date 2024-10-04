using Microsoft.AspNetCore.Mvc;

namespace CosmeticMVC.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index(String? id)
        {
            return View();
        }
    }
}
