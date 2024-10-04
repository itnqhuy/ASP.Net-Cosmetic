using System;
using System.Collections.Generic;

namespace CosmeticMVC.Data;

public partial class Favourite
{
    public string IdFavourite { get; set; } = null!;

    public DateTime DaySelect { get; set; }

    public string Description { get; set; } = null!;

    public string IdProduct { get; set; } = null!;

    public string IdCustomer { get; set; } = null!;

    public virtual Customer IdCustomerNavigation { get; set; } = null!;

    public virtual Product IdProductNavigation { get; set; } = null!;
}
