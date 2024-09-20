using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Range(0, 100)] // Giả định điểm số từ 0 đến 100
        public float Score { get; set; }

        // Navigation properties
        public Exam Exam { get; set; }
        public User User { get; set; }
    }
}
