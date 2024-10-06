using System;
using System.Collections.Generic;

namespace Library.Models;

public partial class Report
{
    public int ReportId { get; set; }

    public int? AssignemtId { get; set; }

    public string? ReportContent { get; set; }

    public string? QuestionSolutionDetail { get; set; }

    public int? QuestionNumber { get; set; }

    public float? Score { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual InstructorAssignment? Assignemt { get; set; }
}
