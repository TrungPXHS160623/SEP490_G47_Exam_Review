using System;
using System.Collections.Generic;

namespace Library.Models;

public partial class Report
{
    public int ReportId { get; set; }

    public int? AssignmentId { get; set; }

    public string? ReportContent { get; set; }

    public string? QuestionSolutionDetail { get; set; }

    public int? QuestionNumber { get; set; }

    public float? Score { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? FileName { get; set; } = null;
    public string? FileType { get; set; }  = null;
    public byte[]? FileData { get; set; } = null;
    public long? FileSize { get; set; } = null;

    public virtual InstructorAssignment? Assignment { get; set; }
}
