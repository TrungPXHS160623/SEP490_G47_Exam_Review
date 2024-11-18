using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Response
{
    public class HeadSubjectRepsonse
    {
        public int? DepartmentId { get;set; }

        public string? DepartmentName { get;set; }

        public List<SubjectResponse> SubjectsList { get; set; } = new List<SubjectResponse>();
        public List<SubjectResponse> AddSubjectsList { get; set; } = new List<SubjectResponse>();
    }
}
