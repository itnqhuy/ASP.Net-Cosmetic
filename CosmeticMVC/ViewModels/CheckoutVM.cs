using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CosmeticMVC.Data;
using CosmeticMVC.Models; // Make sure to include the CartItem model if it's located in this namespace

namespace CosmeticMVC.ViewModels
{
    public class CheckoutVM
    {
        public bool GiongKhachHang { get; set; } // Sử dụng thông tin khách hàng mặc định hay không

        [Required(ErrorMessage = "Vui lòng nhập họ và tên.")]
        public string HoTen { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập địa chỉ.")]
        public string DiaChi { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại.")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ.")]
        public string DienThoai { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập email.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string GhiChu { get; set; } // Lưu ý đơn hàng

        [Required(ErrorMessage = "Vui lòng chọn hình thức thanh toán.")]
        public string HinhThucThanhToan { get; set; }
    }
}
