using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Request
{
    public class AssignRequest
    {
        public int ExamId { get; set; }

        public int AssignedUserId { get; set; }

        public DateTime? AssignmentDate { get; set; }

        public DateTime? CreateDate { get; set; }

    }
}
