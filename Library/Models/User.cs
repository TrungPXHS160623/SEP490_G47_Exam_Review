using System;
using System.Collections.Generic;

namespace Library.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Mail { get; set; } = null!;

    public int? CampusId { get; set; }

    public int? RoleId { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual Campus? Campus { get; set; }

    public virtual ICollection<Department> Departments { get; set; } = new List<Department>();

    public virtual ICollection<ExamAssignment> ExamAssignments { get; set; } = new List<ExamAssignment>();

    public virtual ICollection<Exam> Exams { get; set; } = new List<Exam>();

    public virtual ICollection<InstructorAssignment> InstructorAssignments { get; set; } = new List<InstructorAssignment>();

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();

    public virtual UserRole? Role { get; set; }
}
