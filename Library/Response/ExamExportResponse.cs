using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Response
{
    public class ExamExportResponse
    {
        public string ExamCode { get; set; }

        public string ExamDuration { get; set; }

        public string ExamType { get; set; }

        public string CampusName { get; set; }

        public string SubjectName { get; set; }

        public string CreaterName { get; set; }

        public string StatusContent { get; set; }

        public DateTime? EstimatedTimeTest { get; set; }
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

    }
}
