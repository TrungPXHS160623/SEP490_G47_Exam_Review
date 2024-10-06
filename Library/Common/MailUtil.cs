using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Common
{
    public class MailModel
    {
        public string? Subject { get; set; }
        public string? Body { get; set; }

        public List<MailUtil> MailList { get; set; } = new List<MailUtil>();
    }

    public partial class MailUtil
    {
        public string? MailCd { get; set; }

        public string? MailTo { get; set; }

        public string[]? param { get; set; }

        public string? CcTo { get; set; }

        public string? BccTo { get; set; }
    }
}
