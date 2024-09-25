using System;
using System.Collections.Generic;

namespace Library.Models;

public partial class UserHistory
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string? LogContent { get; set; }

    public DateTime? LogDt { get; set; }

    public virtual User? User { get; set; }
}
