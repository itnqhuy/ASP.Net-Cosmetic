using System;
using System.Collections.Generic;

namespace CosmeticMVC.Data;

public partial class Ingredient
{
    public string IdIngredient { get; set; } = null!;

    public string DetailText { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string RiskLevel { get; set; } = null!;

    public string Uses { get; set; } = null!;

    public string Guide { get; set; } = null!;

    public string SkinCompatibility { get; set; } = null!;

    public string Meta { get; set; } = null!;

    public bool Hide { get; set; }

    public int Order { get; set; }

    public DateTime Datebegin { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
