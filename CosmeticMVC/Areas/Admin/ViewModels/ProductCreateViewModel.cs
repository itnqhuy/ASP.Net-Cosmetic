using System.ComponentModel.DataAnnotations;
using CosmeticMVC.Data;

namespace CosmeticMVC.Areas.Admin.ViewModels
{
    public class ProductCreateViewModel
    {
        [Display(Name = "Tên sản phẩm")]
        [Required(ErrorMessage = "Tên sản phẩm là bắt buộc.")]
        public string Name { get; set; }

        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        [Display(Name = "Giá")]
        [Required(ErrorMessage = "Giá sản phẩm là bắt buộc.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Giá sản phẩm phải lớn hơn 0.")]
        public decimal Price { get; set; }

        [Display(Name = "Số lượng")]
        [Required(ErrorMessage = "Số lượng sản phẩm là bắt buộc.")]
        [Range(0, int.MaxValue, ErrorMessage = "Số lượng sản phẩm không thể âm.")]
        public int Quantity { get; set; }

        [Display(Name = "Ẩn sản phẩm")]
        public bool Hide { get; set; }

        [Display(Name = "Meta thông tin")]
        public string Meta { get; set; }

        [Display(Name = "Thứ tự hiển thị")]
        [Required(ErrorMessage = "Thứ tự hiển thị là bắt buộc.")]
        [Range(0, int.MaxValue, ErrorMessage = "Thứ tự hiển thị phải là số dương.")]
        public int Order { get; set; }

        [Display(Name = "Ngày bắt đầu")]
        [Required(ErrorMessage = "Ngày bắt đầu là bắt buộc.")]
        [DataType(DataType.Date, ErrorMessage = "Ngày bắt đầu không hợp lệ.")]
        public DateTime Datebegin { get; set; }

        [Display(Name = "Ngày sản xuất")]
        [Required(ErrorMessage = "Ngày sản xuất là bắt buộc.")]
        [DataType(DataType.Date, ErrorMessage = "Ngày sản xuất không hợp lệ.")]
        public DateTime Exp { get; set; }

        [Display(Name = "Ngày hết hạn")]
        [Required(ErrorMessage = "Ngày hết hạn là bắt buộc.")]
        [DataType(DataType.Date, ErrorMessage = "Ngày hết hạn không hợp lệ.")]
        public DateTime Mfg { get; set; }

        // Các trường mới với Display Name
        [Display(Name = "Tên thương hiệu")]
        [Required(ErrorMessage = "Tên thương hiệu là bắt buộc.")]
        public string IdBrand { get; set; }

        [Display(Name = "Danh mục sản phẩm")]
        [Required(ErrorMessage = "Danh mục sản phẩm là bắt buộc.")]
        public string IdCategory { get; set; }

        [Display(Name = "URL hình ảnh")]
        [Required(ErrorMessage = "URL hình ảnh là bắt buộc.")]
        [Url(ErrorMessage = "URL hình ảnh không hợp lệ.")]
        public string? IdImage { get; set; }
    }
}
