using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CosmeticMVC.Areas.Admin.Controllers
{
    [Authorize(AuthenticationSchemes = "StaffCookie")]
    public class HomeController : Controller
    {
        [Area("Admin")]
        [Authorize(Roles = "Staff")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Staff")]
        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync("StaffCookie");
            return Redirect("/");
        }
    }
}
