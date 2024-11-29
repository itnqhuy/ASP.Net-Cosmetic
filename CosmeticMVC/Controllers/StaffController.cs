using CosmeticMVC.Data;
using CosmeticMVC.Helpers;
using CosmeticMVC.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace CosmeticMVC.Controllers
{
    public class StaffController : Controller
    {
        private readonly CosmeticContext db;
        public StaffController(CosmeticContext context)
        {
            db = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(StaffVM model)
        {
            if (ModelState.IsValid)
            {
                var staff = db.Staff.SingleOrDefault(st => st.Email == model.UserName);
                if (staff == null || staff.Password != model.Password)
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, staff.FullName),
                    new Claim(ClaimTypes.Email, staff.Email),
                    new Claim(ClaimTypes.NameIdentifier, staff.IdStaff.ToString()),
                    new Claim(ClaimTypes.Role, "Staff")  // Ensure this role is added
                };

                var identity = new ClaimsIdentity(claims, "StaffCookie");
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("StaffCookie", principal);

                return RedirectToAction("Index", "Products", new { area = "Admin" });
            }

            return View();
        }

        [Authorize(Roles = "Staff")]
        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync("StaffCookie");
            return RedirectToAction("Index", "Home", new {area = ""});
        }


        [HttpGet]
        public IActionResult Registry()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registry(CustomerController customer)
        {
            return View();
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPassword(StaffVM account)
        {
            return View();
        }

    }
}