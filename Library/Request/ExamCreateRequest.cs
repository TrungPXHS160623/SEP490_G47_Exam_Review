using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Request
{
    public class ExamCreateRequest
    {
        public string? ExamCode { get; set; }

        public string? ExamDuration { get; set; }

        public string? ExamType { get; set; }

        public int? SubjectId { get; set; }

        public int? CreaterId { get; set; }

        public int? CampusId { get; set; }

        public DateTime? EstimatedTimeTest { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }
    }
}
