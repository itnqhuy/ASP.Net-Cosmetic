using CosmeticMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using CosmeticMVC.ViewModels;
using CosmeticMVC.Data;
using Microsoft.Extensions.Logging;

namespace CosmeticMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly CosmeticContext db;
        private readonly ILogger<HomeController> _logger;

        // Combine both constructors into one
        public HomeController(CosmeticContext context, ILogger<HomeController> logger)
        {
            db = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewBag.ControllerName = "Category";
            var categories = db.Categories.AsQueryable();
            ViewBag.Categories = categories;
            return View();
        }

        [Route("/404")]
        public IActionResult PageNotFound()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
