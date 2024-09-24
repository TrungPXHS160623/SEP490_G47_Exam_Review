using System;
using System.Collections.Generic;

namespace Library.Models;

public partial class Department
{
    public int DepartmentId { get; set; }

    public string? DepartmentName { get; set; }

    public int? HeadOfDepartmentId { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual ICollection<ExamAssignment> ExamAssignments { get; set; } = new List<ExamAssignment>();

    public virtual User? HeadOfDepartment { get; set; }

    public virtual ICollection<Subject> Subjects { get; set; } = new List<Subject>();
}
