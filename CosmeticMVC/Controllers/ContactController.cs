using Microsoft.AspNetCore.Mvc;

namespace CosmeticMVC.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
