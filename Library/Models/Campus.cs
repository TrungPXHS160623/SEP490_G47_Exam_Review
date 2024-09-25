using System;
using System.Collections.Generic;

namespace Library.Models;

public partial class Campus
{
    public int CampusId { get; set; }

    public string? CampusName { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();

    public virtual ICollection<Exam> Exams { get; set; } = new List<Exam>();
}
