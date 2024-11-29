namespace CosmeticMVC.ViewModels
{
    public class ListRateVM
    {
        public string IdRate { get; set; } // ID của đánh giá
        public int Star { get; set; } // Số sao đánh giá từ 1 đến 5
        public string Description { get; set; } // Nội dung của đánh giá
        public string Image { get; set; } // Đường dẫn đến ảnh đại diện người dùng
        public string NameCustomer { get; set; } // Tên của khách hàng đánh giá
        public bool Hide { get; set; } // Ẩn hay hiện đánh giá
        public DateTime DateBegin { get; set; } // Ngày đánh giá
    }
}