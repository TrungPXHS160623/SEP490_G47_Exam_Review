using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Request
{
    public class ReportRequest
    {
        public int ExamId { get; set; }

        public int UserId { get; set; }

        public string? ReportContent { get; set; }

        public string? QuestionSolutionDetail { get; set; }

        public int? QuestionNumber { get; set; }

        public float? Score { get; set; }

        public DateTime? CreateDate { get; set; }

    }
}
