using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Request
{
    public class ExamSearchRequest
    {
        public string? ExamCode { get; set; }

        public int? StatusId { get; set; }
    }
}
