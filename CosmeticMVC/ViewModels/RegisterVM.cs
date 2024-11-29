
using System.ComponentModel.DataAnnotations;

namespace CosmeticMVC.ViewModels
{
    public class RegisterVM
    {
        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập")]
        [MaxLength(20, ErrorMessage = "Tối đa 20 ký tự")]
        public string IdCustomer { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        public string Password { get; set; }

        [Display(Name = "Địa chỉ")]
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ")]
        [MaxLength(60, ErrorMessage = "Tối đa 60 ký tự")]
        public string Address { get; set; }

        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Chưa đúng định dạng email")]
        [Required(ErrorMessage = "Vui lòng nhập email")]
        public string Email { get; set; }

        [Display(Name = "Họ và tên")]
        [Required(ErrorMessage = "Vui lòng nhập họ và tên")]
        public string FullName { get; set; }

        [Display(Name = "Số điện thoại")]
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        [RegularExpression(@"0[9875]\d{8}" , ErrorMessage = "Vui lòng nhập đúng định đạng")] 
        public string Phone { get; set; }

        [Display(Name = "Giới tính")]
        [Required(ErrorMessage = "Vui lòng chọn giới tính")]
        public bool Sex { get; set; }

        [Display(Name = "Ảnh đại diện")]
        public string? Avatar { get; set; }
    }
}
