using System;
using System.Collections.Generic;

namespace Library.Models;

public partial class Subject
{
    public int SubjectId { get; set; }

    public int? FacultyId { get; set; }

    public string? SubjectCode { get; set; }

    public string? SubjectName { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual ICollection<CampusUserSubject> CampusUserSubjects { get; set; } = new List<CampusUserSubject>();

    public virtual ICollection<Exam> Exams { get; set; } = new List<Exam>();

    public virtual Faculty Faculty { get; set; }
}
