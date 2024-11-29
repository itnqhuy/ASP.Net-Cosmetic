using System.ComponentModel.DataAnnotations;

namespace CosmeticMVC.ViewModels
{
    public class ListOrderVM
    {
        [Display(Name = "Mã đơn hàng")]
        public string IdOrder { get; set; } // Unique identifier for the order
        [Display(Name = "Tên khách hàng")]
        public string CustomerName { get; set; } // Customer name associated with the order
        [Display(Name = "Tên người nhận hàng")]
        public string ReceiverName { get; set; } // Receiver's name
        [Display(Name = "Địa chỉ nhận hàng")]
        public string ReceiverAddress { get; set; } // Receiver's address
        [Display(Name = "Số điện thoại")]
        public string ReceiverPhone { get; set; } // Receiver's phone number
        [Display(Name = "Thời gian đặt hàng")]
        public DateOnly ModifiedAt { get; set; } // Date and time when the order was modified
        [Display(Name = "Hình thức thanh toán")]
        public string Paymethod { get; set; } // Payment method used
        [Display(Name = "Phí giao hàng")]
        public decimal Shipcost { get; set; } // Shipping cost of the order
        public int IdStatus { get; set; } // Status of the order
        [Display(Name = "Ghi chú")]
        public string Note { get; set; } // Notes regarding the order
        [Display(Name = "Tổng tiền")]
        public decimal TotalAmount { get; set; } // Total amount for the order
    }
}
