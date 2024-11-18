using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Response
{
    public class HeadSubjectRepsonse
    {
        public string? Department { get;set; }

        public List<SubjectResponse> SubjectsList { get; set; } = new List<SubjectResponse>();
    }
}
