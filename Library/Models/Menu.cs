using System;
using System.Collections.Generic;

namespace PRN231_Library.Models;

public partial class Menu
{
    public int MenuId { get; set; }

    public string? Name { get; set; }

    public string? Link { get; set; }

    public string? Icon { get; set; }

    public virtual ICollection<MenuRole> MenuRoles { get; set; } = new List<MenuRole>();
}
