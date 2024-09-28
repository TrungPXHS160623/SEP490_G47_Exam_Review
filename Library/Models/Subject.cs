using System;
using System.Collections.Generic;

namespace Library.Models;

public partial class Subject
{
    public int SubjectId { get; set; }

    public string? SubjectCode { get; set; }

    public string? SubjectName { get; set; }

    public int HeadOfDepartmentId { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual ICollection<Exam> Exams { get; set; } = new List<Exam>();

    public virtual User HeadOfDepartment { get; set; } = null!;
}
