using CosmeticMVC.Data;
using CosmeticMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CosmeticMVC.Helpers;
using Microsoft.AspNetCore.Authorization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CosmeticMVC.Controllers
{
    [Authorize(AuthenticationSchemes = "CustomerCookie")]
    [Route("gio-hang")]

    public class CartItemController : Controller
    {
        private readonly CosmeticContext db;

        public CartItemController(CosmeticContext context)
        {
            db = context;
        }

        public List<ListCartItemVM> Cart => HttpContext.Session.Get<List<ListCartItemVM>>(MySetting.CART_KEY) ?? new List<ListCartItemVM>();

        [Authorize(Roles = "Customer")]
        public IActionResult Index()
        {
            return View(Cart);
        }

        [Route("them-vao-gio-hang-{id}-{quantity}")]
        [Authorize(Roles = "Customer")]
        [HttpPost]
        public JsonResult AddToCartItem(string id, int quantity = 1)
        {
            var gioHang = Cart;
            var item = gioHang.SingleOrDefault(p => p.IdProduct == id);
            if (item == null)
            {
                var hangHoa = db.Products.Include(p => p.IdImageNavigation).SingleOrDefault(p => p.IdProduct == id);
                if (hangHoa == null)
                {
                    return Json(new { success = false, message = $"Không tìm thấy hàng hóa có mã {id}" });
                }

                item = new ListCartItemVM
                {
                    IdProduct = id,
                    Quantity = quantity,
                    ProductImage = hangHoa.IdImageNavigation.Name.ToString(),
                    ProductName = hangHoa.Name,
                    Price = hangHoa.Price,
                    TotalProduct = hangHoa.Price * quantity,
                };
                gioHang.Add(item);
            }
            else
            {
                item.Quantity += quantity;
                item.TotalProduct = item.Price * item.Quantity;
            }

            HttpContext.Session.Set(MySetting.CART_KEY, gioHang);

            // Returning the updated cart quantity
            return Json(new
            {
                success = true,
                message = "Sản phẩm đã được thêm vào giỏ hàng!",
                cartQuantity = gioHang.Sum(i => i.Quantity)
            });
        }

        [Route("lay-so-luong-gio-hang")]
        [Authorize(Roles = "Customer")]
        [HttpGet]
        public JsonResult GetCartQuantity()
        {
            var gioHang = Cart;
            int quantity = gioHang.Sum(item => item.Quantity); // Tính tổng số lượng sản phẩm trong giỏ hàng
            return Json(new { cartQuantity = quantity });
        }

        [Route("xoa-khoi-gio-hang/{id}")]
        [Authorize(Roles = "Customer")]
        [HttpPost]
        public JsonResult RemoveFromCartItem(string id)
        {
            var gioHang = Cart;
            var item = gioHang.SingleOrDefault(p => p.IdProduct == id);
            if (item != null)
            {
                gioHang.Remove(item);
                HttpContext.Session.Set(MySetting.CART_KEY, gioHang);
            }

            // Trả về JSON với số lượng sản phẩm trong giỏ và thông báo
            return Json(new { success = true, cartQuantity = gioHang.Sum(i => i.Quantity), message = "Sản phẩm đã được xóa khỏi giỏ hàng!" });
        }

        [Route("cap-nhat-gio-hang/{id}/{quantity}")]
        [Authorize(Roles = "Customer")]
        [HttpPost]
        public JsonResult UpdateCartItemQuantity(string id, int quantity)
        {
            var gioHang = Cart;
            var item = gioHang.SingleOrDefault(p => p.IdProduct == id);
            if (item != null)
            {
                item.Quantity = quantity;
                item.TotalProduct = item.Price * item.Quantity;
                HttpContext.Session.Set(MySetting.CART_KEY, gioHang);

                // Trả về thông tin cập nhật
                return Json(new
                {
                    success = true,
                    productId = id,
                    newTotal = item.TotalProduct,
                    cartTotal = gioHang.Sum(i => i.TotalProduct)
                });
            }

            return Json(new { success = false, message = "Sản phẩm không tồn tại trong giỏ hàng." });
        }

        [Route("xac-nhan")]
        [Authorize(Roles = "Customer")]
        [HttpGet]
        public IActionResult Checkout()
        {
            if (Cart.Count == 0)
            {
                return Redirect("/");
            }

            return View(Cart);
        }

        [Route("xac-nhan-don-hang")]
        [Authorize(Roles = "Customer")]
        [HttpPost]
        public IActionResult Checkout(CheckoutVM model)
        {
            var customerId = HttpContext.User.Claims.SingleOrDefault(p => p.Type == MySetting.CLAIM_CUSTOMERID).Value;
            var khachHang = new Customer();
            if (model.GiongKhachHang)
            {
                khachHang = db.Customers.SingleOrDefault(kh => kh.IdCustomer == customerId);
            }

            var hoadon = new Order
            {
                IdOrder = MyUtil.GenerateRandomId(),
                IdCustomer = customerId,
                ReceiverName = model.HoTen ?? khachHang.FullName,
                ReceiverAddress = model.DiaChi ?? khachHang.Address,
                ReceiverPhone = model.DienThoai ?? khachHang.Phone,
                Datebegin = DateTime.Now,
                Paymethod = "COD",
                Shipcost = 30000,
                IdStatus = 0,
                Note = model.GhiChu
            };

            db.Database.BeginTransaction();
            try
            {
                db.Database.CommitTransaction();
                db.Add(hoadon);
                db.SaveChanges();

                var cthds = new List<OrderDetail>();
                foreach (var item in Cart)
                {
                    cthds.Add(new OrderDetail
                    {
                        IdOrder = hoadon.IdOrder,
                        IdDetail = MyUtil.GenerateRandomId(),
                        Quantity = item.Quantity,
                        UnitPrice = item.Price,
                        IdProduct = item.IdProduct,

                    });
                }
                db.AddRange(cthds);
                db.SaveChanges();

                HttpContext.Session.Set<List<CartItem>>(MySetting.CART_KEY, new List<CartItem>());

                return View("Success");
            }
            catch
            {
                db.Database.RollbackTransaction();
            }

            return View(Cart);
        }

    }
}