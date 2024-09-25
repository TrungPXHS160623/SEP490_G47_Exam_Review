using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Response
{
    public class TestDepartmentExamResponse
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

        public int? CampusId { get; set; }

        public string? CampusName { get; set; }

        public int? ExamStatusId { get; set; }

        public string? ExamStatusContent { get; set; }

        public DateTime? EstimatedTimeTest { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }
    }
}
