using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Response
{
    public class ReportAdvancedResponse
    {
        public int? ReportId { get; set; }

        public string? ExamCode { get; set; }

        public string? SubjectName { get; set; }

        public string? ReviewerMail { get; set; }

        public int? QuestionNumber { get; set; }

        public string? ReportContent { get; set; }

        public string? QuestionSolutionDetail { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public IList<IFormFile> Files { get; set; } = new List<IFormFile>();

        
    }
}
