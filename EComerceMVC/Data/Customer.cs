using System;
using System.Collections.Generic;

namespace EComerceMVC.Data;

public partial class Customer
{
    public string IdCustomer { get; set; } = null!;

    public int Address { get; set; }

    public int CreateAt { get; set; }

    public int Email { get; set; }

    public int FullName { get; set; }

    public int Password { get; set; }

    public int Phone { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Favourite> Favourites { get; set; } = new List<Favourite>();

    public virtual ICollection<Rate> Rates { get; set; } = new List<Rate>();
}
