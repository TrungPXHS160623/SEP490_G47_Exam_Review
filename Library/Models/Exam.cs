namespace Library.Models;

public partial class Exam
{
    public Exam()
    {
        Reports = new HashSet<Report>();
        TestTimeInMinute = 0; // Mặc định giá trị là 0
    }

    public int ExamId { get; set; }
    public string? ExamCode { get; set; }
    public int? ExamDuration { get; set; }
    public string? TermDuration { get; set; }
    public string? ExamType { get; set; }
    public int? SubjectId { get; set; }
    public int? CreaterId { get; set; }
    public int? CampusId { get; set; }
    public int? SemesterId { get; set; }
    public int? ExamStatusId { get; set; }
    public DateTime? ExamDate { get; set; }
    public DateTime? EstimatedTimeTest { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int? AssignedUserId { get; set; }
    public DateTime? AssignmentDate { get; set; }
    public string? GeneralFeedback { get; set; }
    public bool? IsReady { get; set; }
    public DateTime? CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public int? TestTimeInMinute { get; set; } // Số phút test đề
    public virtual Campus? Campus { get; set; }
    public virtual User Creater { get; set; } = null!;
    public virtual ExamStatus? ExamStatus { get; set; }
    public virtual Semester? Semester { get; set; }
    public virtual Subject? Subject { get; set; }
    public virtual ICollection<Report> Reports { get; set; }
}
