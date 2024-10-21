using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Request
{
    public class LogRequest
    {
        public string? Mail {  get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
