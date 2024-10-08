using CosmeticMVC.Data;
using CosmeticMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CosmeticMVC.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index(String? id)
        {
            return View();
        }


    }
}
