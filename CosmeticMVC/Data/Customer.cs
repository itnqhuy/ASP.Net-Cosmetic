using System;
using System.Collections.Generic;

namespace CosmeticMVC.Data;

public partial class Customer
{
    public string IdCustomer { get; set; } = null!;

    public string Address { get; set; } = null!;

    public DateTime CreateAt { get; set; }

    public string Email { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public bool? Sex { get; set; }

    public DateTime? Birthday { get; set; }

    public string? Avatar { get; set; }

    public string? Randomkey { get; set; }

    public int? Role { get; set; }

    public bool? Permission { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Favourite> Favourites { get; set; } = new List<Favourite>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Rate> Rates { get; set; } = new List<Rate>();
}
