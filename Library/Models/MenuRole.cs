using System;
using System.Collections.Generic;

namespace Library.Models;

public partial class MenuRole
{
    public int RoleId { get; set; }

    public int MenuId { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual Menu Menu { get; set; } = null!;

    public virtual UserRole Role { get; set; } = null!;
}
