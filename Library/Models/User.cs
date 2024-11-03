using System;
using System.Collections.Generic;

namespace Library.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Mail { get; set; } = null!;

    public int? CampusId { get; set; }

    public int? RoleId { get; set; }
    public string FullName { get; set; } = "Unknown";
    public string PhoneNumber { get; set; } = "Unknown";
    public string? EmailFe { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public bool? Gender { get; set; }
    public string? Address { get; set; }
    public bool IsActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual Campus? Campus { get; set; }

    public virtual ICollection<CampusUserSubject> CampusUserSubjects { get; set; } = new List<CampusUserSubject>();

    public virtual ICollection<Exam> Exams { get; set; } = new List<Exam>();

    public virtual ICollection<InstructorAssignment> InstructorAssignments { get; set; } = new List<InstructorAssignment>();

    public virtual UserRole? Role { get; set; }

    public virtual ICollection<UserHistory> UserHistories { get; set; } = new List<UserHistory>();

    public virtual ICollection<Faculty> Faculties { get; set; } = new List<Faculty>(); // Mối quan hệ tới Faculty
}



