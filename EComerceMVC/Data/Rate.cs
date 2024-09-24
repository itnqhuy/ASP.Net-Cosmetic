using System;
using System.Collections.Generic;

namespace EComerceMVC.Data;

public partial class Rate
{
    public string IdRate { get; set; } = null!;

    public int Star { get; set; }

    public string Description { get; set; } = null!;

    public string Image { get; set; } = null!;

    public int Order { get; set; }

    public string Meta { get; set; } = null!;

    public DateTime Datebegin { get; set; }

    public bool Hide { get; set; }

    public string IdCustomer { get; set; } = null!;

    public string IdProduct { get; set; } = null!;

    public virtual Customer IdCustomerNavigation { get; set; } = null!;

    public virtual Product IdProductNavigation { get; set; } = null!;
}
