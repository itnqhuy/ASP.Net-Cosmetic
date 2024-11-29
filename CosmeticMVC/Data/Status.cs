using System;
using System.Collections.Generic;

namespace CosmeticMVC.Data;

public partial class Status
{
    public int IdStatus { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
