using System;
using System.Collections.Generic;

namespace Library.Models;

public partial class Subject
{
    public int SubjectId { get; set; }

    public string? SubjectName { get; set; }

    public int DepartmentId { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual Department Department { get; set; } = null!;

    public virtual ICollection<Exam> Exams { get; set; } = new List<Exam>();
}
