using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class Report
    {
        [Key]
        public int ReviewId { get; set; }

        [Required]
        public int ExamId { get; set; }

        [Required]
        public int UserId { get; set; }

        public string ReportContent { get; set; }
        public string QuestionSolutionDetail { get; set; }

        [Required]
        public int QuestionNumber { get; set; }

        [Range(0, 100)]
        public float Score { get; set; }

        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        [ForeignKey("ExamId")]
        public virtual Exam Exam { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
