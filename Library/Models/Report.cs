using System;
using System.Collections.Generic;

namespace Library.Models;


public partial class Report
{
    public Report()
    {
        ReportFiles = new HashSet<ReportFile>();
    }

    public int ReportId { get; set; }
    public int? ExamId { get; set; }
    public string? ReportContent { get; set; }
    public string? QuestionSolutionDetail { get; set; }
    public int? QuestionNumber { get; set; }

    public DateTime? CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }

    public virtual Exam? Exam { get; set; }
    public virtual ICollection<ReportFile> ReportFiles { get; set; }

}

