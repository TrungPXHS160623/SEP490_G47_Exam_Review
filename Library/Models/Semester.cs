namespace Library.Models
{
    public class Semester
    {

        public int SemesterId { get; set; }
        public string SemesterName { get; set; } = null!;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public virtual ICollection<Exam> Exams { get; set; } = new List<Exam>();
    }
}
