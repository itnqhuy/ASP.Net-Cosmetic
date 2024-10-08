using System.ComponentModel.DataAnnotations;

namespace CosmeticMVC.ViewModels
{
    public class ListPostVM
    {
        public string Id_post {get; set; }
        public string Content {get; set; }
        public string Description {get; set; }
        public int Hide {get; set; }
        public string Thumbail {get; set; }
        public string Meta {get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateOnly Modified_at {get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateOnly Datebegin {get; set; }
        public int Order {get; set; }

        public string Name_product {get; set; }
    }
}
