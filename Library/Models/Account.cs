using System;
using System.Collections.Generic;

namespace Library.Models;

public partial class Account
{
    public int AccountId { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public int? RoleId { get; set; }

    public virtual ICollection<QuestionHistory> QuestionHistories { get; set; } = new List<QuestionHistory>();

    public virtual Role? Role { get; set; }
}
