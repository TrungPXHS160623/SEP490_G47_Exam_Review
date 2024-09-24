using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class ExamStatus
    {
        [Key]
        public int ExamStatusID { get; set; }

        [Required]
        [StringLength(255)]
        public string StatusContent { get; set; }

        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual ICollection<Exam> Exams { get; set; }
    }
}
