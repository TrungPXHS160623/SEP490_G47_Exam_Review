using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    public class Exam
    {
        [Key]
        public int ExamId { get; set; }

        [Required]
        [StringLength(50)]
        public string ExamCode { get; set; }

        [Required]
        [StringLength(10)]
        public string ExamDuration { get; set; }

        [Required]
        [StringLength(50)]
        public string ExamType { get; set; }

        [Required]
        public int SubjectId { get; set; }
        [Required]
        public int CreaterId { get; set; }
        [Required]
        public int ExamStatusID { get; set; }
        [Required]
        public DateTime EstimatedTimeTest { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }

        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        [ForeignKey("SubjectId")]
        public virtual Subject Subject { get; set; }
            
        [ForeignKey("CreaterId")]
        public virtual User Creator { get; set; }

        [ForeignKey("ExamStatusID")]
        public virtual ExamStatus ExamStatus { get; set; }

        public virtual ICollection<ExamAssignment> ExamAssignments { get; set; }
        public virtual ICollection<InstructorAssignment> InstructorAssignments { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
    }
}
