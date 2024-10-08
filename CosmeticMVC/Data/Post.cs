﻿using System;
using System.Collections.Generic;

namespace CosmeticMVC.Data;

public partial class Post
{
    public string IdPost { get; set; } = null!;

    public string Content { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int Hide { get; set; }

    public string Thumbail { get; set; } = null!;

    public string Meta { get; set; } = null!;

    public DateOnly ModifiedAt { get; set; }

    public DateOnly Datebegin { get; set; }

    public int Order { get; set; }

    public string IdProduct { get; set; } = null!;

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual Product IdProductNavigation { get; set; } = null!;
}
