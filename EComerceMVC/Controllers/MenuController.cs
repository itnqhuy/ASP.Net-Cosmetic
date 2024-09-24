using Microsoft.AspNetCore.Mvc;

namespace EComerceMVC.Controllers
{
    public class MenuController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
