using System;
using System.Collections.Generic;

namespace Library.Models;

public partial class Menu
{
    public int MenuId { get; set; }

    public string? MenuName { get; set; }
    public string? MenuLink { get; set; }

    public bool? IsProgram { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual ICollection<MenuRole> MenuRoles { get; set; } = new List<MenuRole>();
}
