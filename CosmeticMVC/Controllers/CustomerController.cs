using System.Security.Claims;
using AutoMapper;
using CosmeticMVC.Data;
using CosmeticMVC.Helpers;
using CosmeticMVC.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;

namespace CosmeticMVC.Controllers
{
    [Route("khach-hang")]
    public class CustomerController : Controller
    {
        private readonly CosmeticContext db;
        private readonly IMapper _mapper;

        public CustomerController(CosmeticContext context, IMapper mapper)
        {
            db = context;
            _mapper = mapper;
        }

        #region Login
        [HttpGet]
        [Route("dang-nhap/{ReturnUrl?}")]
        public IActionResult Login(string? ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [HttpPost]
        [Route("dang-nhap/{model?}/{ReturnUrl?}")]
        public async Task<IActionResult> LoginAsync(LoginVM model, string? ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            if (ModelState.IsValid)
            {
                var khachHang = db.Customers.SingleOrDefault(kh => kh.IdCustomer == model.UserName);
                if (khachHang == null)
                {
                    ModelState.AddModelError("Lỗi", "Không tồn tại khách hàng này");
                }
                else
                {
                    if ((bool)!khachHang.Permission)
                    {
                        ModelState.AddModelError("Lỗi", "Tài khoản đã bị khóa");
                    }
                    else
                    {
                        if (khachHang.Password != model.Password.ToMd5Hash(khachHang.Randomkey))
                        {
                            ModelState.AddModelError("Lỗi", "Sai thông tin đăng nhập");
                        }
                        else
                        {
                            var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.Email, khachHang.Email),
                                new Claim(ClaimTypes.Name, khachHang.FullName),
                                new Claim(MySetting.CLAIM_CUSTOMERID, khachHang.IdCustomer),
                                new Claim(ClaimTypes.Role, "Customer")
                            };

                            var identity = new ClaimsIdentity(claims, "CustomerCookie");
                            var principal = new ClaimsPrincipal(identity);

                            await HttpContext.SignInAsync("CustomerCookie", principal);

                            if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                            {
                                return Redirect(ReturnUrl);
                            }
                            else
                            {
                                return Redirect("/");
                            }
                        }
                    }
                }
            }
            return View();
        }

        #endregion

        [Authorize(Roles = "Customer")]
        [HttpGet]
        [Route("thong-tin-ca-nhan")]
        public IActionResult Profile()
        {
            var id = User.FindFirstValue("CustomerID");
            var khachHang = db.Customers.SingleOrDefault(kh => kh.IdCustomer == id);

            if (khachHang == null)
            {
                return RedirectToAction("Login");
            }

            var model = new UpdateProfileVM
            {
                IdCustomer = khachHang.IdCustomer,
                FullName = khachHang.FullName,
                Email = khachHang.Email,
                Phone = khachHang.Phone,
                Address = khachHang.Address,
                Avatar = khachHang.Avatar
            };

            return View(model);
        }

        [Authorize(Roles = "Customer")]
        [Route("thong-tin-ca-nhan/{model?}/{Avatar?}")]
        [HttpPost]
        public IActionResult UpdateProfile(UpdateProfileVM model, IFormFile Avatar)
        {
            if (ModelState.IsValid)
            {
                var khachHang = db.Customers.SingleOrDefault(kh => kh.IdCustomer == model.IdCustomer);

                if (khachHang != null)
                {
                    khachHang.FullName = model.FullName;
                    khachHang.Phone = model.Phone;
                    khachHang.Address = model.Address;

                    if (Avatar != null)
                    {
                        khachHang.Avatar = MyUtil.uploadHinh(Avatar, "Customer");
                    }

                    db.SaveChanges();
                    TempData["Success"] = "Thông tin của bạn đã được cập nhật thành công.";
                }
            }

            return RedirectToAction("Profile");
        }

        [Authorize(Roles = "Customer")]
        [HttpGet]
        [Route("dang-xuat")]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync("CustomerCookie");
            return Redirect("/");
        }


        #region Register

        [HttpGet]
        [Route("dang-ky")]
        public IActionResult Registry()
        {
            return View();
        }

        [HttpPost]
        [Route("dang-ky/{register?}/{Hinh?}")]
        public IActionResult Registry(RegisterVM register, IFormFile Hinh)
        {
            if (ModelState.IsValid)
            {
                var khachHang = _mapper.Map<Customer>(register);
                khachHang.Randomkey = MyUtil.GenerateRandomKey();
                khachHang.CreateAt = DateTime.Today;
                khachHang.Password = register.Password.ToMd5Hash(khachHang.Randomkey);
                khachHang.Permission = true; //se xu ly khi dung mail de active
                khachHang.Role = 1;


                if (Hinh != null)
                {
                    var avatarPath = MyUtil.uploadHinh(Hinh, "Customer");
                    khachHang.Avatar = Hinh.FileName;
                }

                db.Add(khachHang);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");

            }
            return View();
        }

        #endregion

    }
}
