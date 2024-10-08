﻿using System;
using System.Collections.Generic;

namespace CosmeticMVC.Data;

public partial class Category
{
    public string IdCategory { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Meta { get; set; } = null!;

    public bool Hide { get; set; }

    public int Order { get; set; }

    public DateTime Datebegin { get; set; }

    public string Link { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
