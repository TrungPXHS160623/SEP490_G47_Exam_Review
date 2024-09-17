using System;
using System.Collections.Generic;

namespace PRN231_Library.Models;

public partial class Role
{
    public int RoleId { get; set; }

    public string? RoleName { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    public virtual ICollection<MenuRole> MenuRoles { get; set; } = new List<MenuRole>();
}
