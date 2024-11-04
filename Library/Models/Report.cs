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

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }
    public virtual InstructorAssignment? Assignment { get; set; }

    // Navigation property cho các file đính kèm
    public virtual ICollection<ReportFile> ReportFiles { get; set; } = new List<ReportFile>();
}
