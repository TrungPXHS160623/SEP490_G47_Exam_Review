using System;
using System.Collections.Generic;

namespace Library.Models;

public partial class InstructorAssignment
{
    public int AssignmentId { get; set; }

    public int ExamId { get; set; }

    public int AssignedUserId { get; set; }

    public DateTime? AssignmentDate { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual User AssignedUser { get; set; } = null!;

    public virtual Exam Exam { get; set; } = null!;
}
