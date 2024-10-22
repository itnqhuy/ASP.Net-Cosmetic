namespace CosmeticMVC.ViewModels
{
    public class ListCartItemVM
    {
        public string IdProduct { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public int TotalProduct { get; set; }
        public double Discount { get; set; }
        public int SubTotalCartItem { get; set; }
        public int TotalCartItem { get; set; }
    }
}
