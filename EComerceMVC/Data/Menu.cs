using System;
using System.Collections.Generic;

namespace EComerceMVC.Data;

public partial class Menu
{
    public int IdMenu { get; set; }

    public string Name { get; set; } = null!;

    public string Link { get; set; } = null!;

    public string Meta { get; set; } = null!;

    public bool Hide { get; set; }

    public int Order { get; set; }

    public DateOnly Datebegin { get; set; }
}
