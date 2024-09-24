using System;
using System.Collections.Generic;

namespace Library.Models;

public partial class Report
{
    public int ReviewId { get; set; }

    public int ExamId { get; set; }

    public int UserId { get; set; }

    public string? ReportContent { get; set; }

    public string? QuestionSolutionDetail { get; set; }

    public int? QuestionNumber { get; set; }

    public float? Score { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual Exam Exam { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
