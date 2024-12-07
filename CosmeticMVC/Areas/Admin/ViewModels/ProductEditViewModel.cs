namespace CosmeticMVC.Areas.Admin.ViewModels
{
    public class ProductEditViewModel
    {
        public string IdProduct { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public string IdBrand { get; set; }
        public string IdCategory { get; set; }
        public string IdImage { get; set; }
        public string Exp { get; set; }
        public DateTime Datebegin { get; set; }
        public bool Hide { get; set; }
        public int? TotalSold { get; set; }
    }

}
