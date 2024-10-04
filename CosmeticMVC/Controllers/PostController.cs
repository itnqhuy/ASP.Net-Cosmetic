using Microsoft.AspNetCore.Mvc;

namespace CosmeticMVC.Controllers
{
    public class PostController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
