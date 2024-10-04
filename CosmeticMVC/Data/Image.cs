using System;
using System.Collections.Generic;

namespace CosmeticMVC.Data;

public partial class Image
{
    public string IdImage { get; set; } = null!;

    public string Meta { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Type { get; set; } = null!;

    public DateTime Datebegin { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
