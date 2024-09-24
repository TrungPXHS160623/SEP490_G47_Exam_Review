using System;
using System.Collections.Generic;

namespace Library.Models;

public partial class ExamStatus
{
    public int ExamStatusId { get; set; }

    public string? StatusContent { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual ICollection<Exam> Exams { get; set; } = new List<Exam>();
}
