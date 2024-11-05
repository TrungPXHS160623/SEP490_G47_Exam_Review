using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Response
{
    public class LectureExamResponse
    {
        public int ExamId { get; set; }

        public string? ExamCode { get; set; }

        public string? ExamDuration { get; set; }

        public string? ExamType { get; set; }

        public int? SubjectId { get; set; }

        public string? SubjectCode { get; set; }

        public string? SubjectName { get; set; }

        public int? CreaterId { get; set; }

        public string? CreaterName { get; set; }

        public int? HeadDepartmentId { get; set; }

        public string? HeadDepartmentName { get; set; }

        public string? Summary { get;set; }

        public List<ReportResponse> ReportList { get; set; } = new List<ReportResponse>();

        public int? CampusId { get; set; }

        public string? CampusName { get; set; }

        public int? AssignStatusId { get; set; }

        public string? AssignStatusContent { get; set; }

        public int? AssignmentId { get; set; }

        public int? AssignmentUserId { get; set; }

        public string? AssignmentUserName { get; set; }

        public DateTime? AssignmentDate { get; set; }

        public DateTime? EstimatedTimeTest { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime? UpdateDate { get; set; }
    }
}
