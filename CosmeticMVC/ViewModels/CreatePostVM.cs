using System;
using System.ComponentModel.DataAnnotations;

namespace CosmeticMVC.ViewModels
{
    public class CreatePostVM
    {
        [Required(ErrorMessage = "Nội dung không được để trống.")]
        [Display(Name = "Nội dung")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Mô tả không được để trống.")]
        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        [Required(ErrorMessage = "vui lòng chọn sản phẩm")]
        [Display(Name = "Sản phẩm")]
        public string IdProduct { get; set; }
    }
}
