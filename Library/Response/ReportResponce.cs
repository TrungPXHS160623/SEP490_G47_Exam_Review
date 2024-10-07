using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Response
{
    public class ReportResponse
    {
        public int? RerportId { get; set; }

        public string? ExamCode { get; set; }

        public string? SubjectName { get; set; }

        public string? ReviewerMail { get; set; }

        public int? QuestionNumber { get; set; }

        public string? ReportContent { get; set; }

        public string? QuestionSolutionDetail { get; set; }

        public float? Score { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

    }
}
