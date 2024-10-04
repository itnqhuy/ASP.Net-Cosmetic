using System;
using System.Collections.Generic;

namespace CosmeticMVC.Data;

public partial class Brand
{
    public string Id { get; set; } = null!;

    public DateTime Datebegin { get; set; }

    public string Name { get; set; } = null!;

    public string Thumbnail { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
