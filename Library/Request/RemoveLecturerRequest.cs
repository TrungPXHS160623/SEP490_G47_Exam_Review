using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Request
{
    public class RemoveLecturerRequest
    {
        public int UserId { get; set; }
        public int SubjectId { get; set; }
    }
}
