namespace CosmeticMVC.ViewModels
{
    public class ListProductVM
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }

        public int Price { get; set; }
        public int? DiscountedPrice { get; internal set; }

        public int Count { get; set; }
    }
}
