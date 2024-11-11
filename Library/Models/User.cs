namespace Library.Models;

public partial class User
{
    public User()
    {
        CampusUserSubjects = new HashSet<CampusUserSubject>();
        Exams = new HashSet<Exam>();
        UserHistories = new HashSet<UserHistory>();
    }

    public int UserId { get; set; }
    public string Mail { get; set; } = null!;
    public int? CampusId { get; set; }
    public int? RoleId { get; set; }
    public string FullName { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string? EmailFe { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public bool? Gender { get; set; }
    public string? Address { get; set; }
    public bool IsActive { get; set; }
    public DateTime? CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }

    public virtual Campus? Campus { get; set; }
    public virtual UserRole? Role { get; set; }
    public virtual ICollection<CampusUserSubject> CampusUserSubjects { get; set; }

    public virtual ICollection<CampusUserFaculty> CampusUserFaculties { get; set; } = new List<CampusUserFaculty>();

    public virtual ICollection<Exam> Exams { get; set; }
    public virtual ICollection<UserHistory> UserHistories { get; set; }

}



