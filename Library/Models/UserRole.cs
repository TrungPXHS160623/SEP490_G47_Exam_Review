using System;
using System.Collections.Generic;

namespace Library.Models;

public partial class UserRole
{
    public int RoleId { get; set; }

    public string? RoleName { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual ICollection<MenuRole> MenuRoles { get; set; } = new List<MenuRole>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
