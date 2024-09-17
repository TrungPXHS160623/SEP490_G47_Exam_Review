using System;
using System.Collections.Generic;

namespace PRN231_Library.Models;

public partial class MenuRole
{
    public int RoleId { get; set; }

    public int MenuId { get; set; }

    public int Id { get; set; }

    public virtual Menu Menu { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;
}
