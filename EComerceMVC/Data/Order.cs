using System;
using System.Collections.Generic;

namespace EComerceMVC.Data;

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

    public DateOnly Datebegin { get; set; }

    public string IdStaff { get; set; } = null!;

    public virtual Staff IdStaffNavigation { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
