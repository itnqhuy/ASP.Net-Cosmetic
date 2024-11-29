using System.ComponentModel.DataAnnotations;

namespace CosmeticMVC.ViewModels
{
    public class StaffVM
    {
        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập")]
        [MaxLength(255, ErrorMessage = "Tối đa 255 ký tự")]
        public string UserName { get; set; }
        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [MaxLength(8, ErrorMessage = "Tối đa 8 ký tự")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
