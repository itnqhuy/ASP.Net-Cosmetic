using System;
using System.Collections.Generic;

namespace CosmeticMVC.Data;

public partial class Product
{
    public string IdProduct { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int Price { get; set; }

    public int Quantity { get; set; }

    public int TotalSold { get; set; }

    public bool Hide { get; set; }

    public string Meta { get; set; } = null!;

    public int Order { get; set; }

    public DateTime Datebegin { get; set; }

    public DateTime Exp { get; set; }

    public string IdBrand { get; set; } = null!;

    public string IdCatrgory { get; set; } = null!;

    public string IdIngredient { get; set; } = null!;

    public string IdImage { get; set; } = null!;

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual ICollection<Favourite> Favourites { get; set; } = new List<Favourite>();

    public virtual Brand IdBrandNavigation { get; set; } = null!;

    public virtual Category IdCatrgoryNavigation { get; set; } = null!;

    public virtual Image IdImageNavigation { get; set; } = null!;

    public virtual Ingredient IdIngredientNavigation { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    public virtual ICollection<Rate> Rates { get; set; } = new List<Rate>();
}
