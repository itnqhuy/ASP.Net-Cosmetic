using Microsoft.AspNetCore.Mvc;

namespace CosmeticMVC.Controllers
{
    [Route("lien-he")]

    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
