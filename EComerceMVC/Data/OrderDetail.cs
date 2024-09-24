using System;
using System.Collections.Generic;

namespace EComerceMVC.Data;

public partial class OrderDetail
{
    public int Quantity { get; set; }

    public string Status { get; set; } = null!;

    public string IdDetail { get; set; } = null!;

    public string IdOrder { get; set; } = null!;

    public string IdProduct { get; set; } = null!;

    public virtual Order IdOrderNavigation { get; set; } = null!;

    public virtual Product IdProductNavigation { get; set; } = null!;
}
