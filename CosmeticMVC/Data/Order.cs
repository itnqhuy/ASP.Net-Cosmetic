using System;
using System.Collections.Generic;

namespace CosmeticMVC.Data;

public partial class Order
{
    public string IdOrder { get; set; } = null!;

    public string ReceiverAddress { get; set; } = null!;

    public string ReceiverName { get; set; } = null!;

    public string ReceiverPhone { get; set; } = null!;

    public string Paymethod { get; set; } = null!;

    public DateOnly ModifiedAt { get; set; }

    public int Shipcost { get; set; }

    public string Note { get; set; } = null!;

    public DateTime Datebegin { get; set; }

    public string IdStaff { get; set; } = null!;

    public string? IdCustomer { get; set; }

    public int? IdStatus { get; set; }

    public virtual Customer? IdCustomerNavigation { get; set; }

    public virtual Staff IdStaffNavigation { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
