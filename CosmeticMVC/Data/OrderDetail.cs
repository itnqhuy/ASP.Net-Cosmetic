using System;
using System.Collections.Generic;

namespace CosmeticMVC.Data;

public partial class OrderDetail
{
    public int Quantity { get; set; }

    public int IdStatus { get; set; }

    public string IdDetail { get; set; } = null!;

    public string IdOrder { get; set; } = null!;

    public string IdProduct { get; set; } = null!;

    public double Discount { get; set; }

    public double UnitPrice { get; set; }

    public virtual Order IdOrderNavigation { get; set; } = null!;

    public virtual Product IdProductNavigation { get; set; } = null!;

    public virtual Status IdStatusNavigation { get; set; } = null!;
}
