namespace CosmeticMVC.ViewModels
{
    public class ListProductVM
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IdCategory { get; set; }
        public string NameCategory { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }
        public int? DiscountedPrice { get; internal set; }

        public int Count { get; set; }
    }

    public class DetailProductVM
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IdCategory { get; set; }
        public string NameCategory { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }
        public int? DiscountedPrice { get; internal set; }
        public int Count { get; set; }
        public int Rate { get; set; }
        public string TotalSold { get; set; }
        public string NameIngredient { get; set; }
        public string DetailIngredient { get; set; }
        public string NameBrand { get; set; }
        public string Exp { get; set; }
        public string Mfg { get; set; }
    }
}
