using System;
using System.Collections.Generic;

namespace CosmeticMVC.Data;

public partial class CartItem
{
    public int IdCarditem { get; set; }

    public int Quantity { get; set; }

    public string IdProduct { get; set; } = null!;

    public string IdCustomer { get; set; } = null!;

    public virtual Customer IdCustomerNavigation { get; set; } = null!;

    public virtual Product IdProductNavigation { get; set; } = null!;
}
