﻿using System;
using System.Collections.Generic;

namespace Library.Models;

public partial class Exam
{
    public int ExamId { get; set; }

    public string? ExamCode { get; set; }

    public string? ExamDuration { get; set; }

    public string? ExamType { get; set; }

    public int SubjectId { get; set; }

    public int CreaterId { get; set; }

    public int? ExamStatusId { get; set; }

    public DateTime? EstimatedTimeTest { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual User Creater { get; set; } = null!;

    public virtual ICollection<ExamAssignment> ExamAssignments { get; set; } = new List<ExamAssignment>();

    public virtual ExamStatus? ExamStatus { get; set; }

    public virtual ICollection<InstructorAssignment> InstructorAssignments { get; set; } = new List<InstructorAssignment>();

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();

    public virtual Subject Subject { get; set; } = null!;
}
