using Microsoft.AspNetCore.Http;

namespace Library.Response
{
    public class ReportResponse
    {
        public int? ReportId { get; set; }

        public string? ExamCode { get; set; }

        public string? SubjectName { get; set; }

        public string? ReviewerMail { get; set; }

        public int? QuestionNumber { get; set; }

        public string? ReportContent { get; set; }
        public IFormFile? Files { get; set; }

        public string? QuestionSolutionDetail { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

    }
}
