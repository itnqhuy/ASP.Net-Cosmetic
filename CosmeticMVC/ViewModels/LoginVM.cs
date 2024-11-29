using System.ComponentModel.DataAnnotations;

namespace CosmeticMVC.ViewModels
{
    public class LoginVM
    {
        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập")]
        [MaxLength(11, ErrorMessage = "Tối đa 11 ký tự")]
        public string UserName { get; set; }
        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
