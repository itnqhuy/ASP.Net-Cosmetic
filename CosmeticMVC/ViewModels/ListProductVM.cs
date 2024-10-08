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
}
