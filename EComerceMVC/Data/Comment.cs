using System;
using System.Collections.Generic;

namespace EComerceMVC.Data;

public partial class Comment
{
    public string IdComment { get; set; } = null!;

    public string Content { get; set; } = null!;

    public DateOnly Datebegin { get; set; }

    public int Hide { get; set; }

    public string ParentComment { get; set; } = null!;

    public int Order { get; set; }

    public string IdPost { get; set; } = null!;

    public string IdCustomer { get; set; } = null!;

    public virtual Customer IdCustomerNavigation { get; set; } = null!;

    public virtual Post IdPostNavigation { get; set; } = null!;
}
