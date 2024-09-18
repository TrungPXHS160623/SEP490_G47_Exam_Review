using System;
using System.Collections.Generic;

namespace Library.Models;

public partial class SubQuestion
{
    public int SubId { get; set; }

    public int? MainId { get; set; }

    public string? SubContent { get; set; }

    public string? IsAnswer { get; set; }

    public virtual MainQuestion? Main { get; set; }
}
