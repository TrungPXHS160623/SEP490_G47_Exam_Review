using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Common
{
    public class EmailSettings
    {
        public string? Host { get; set; }

        public int Port { get; set; }

        public string? Subject { get; set; }

        public string? Message { get; set; }

        public string? Username { get; set; }

        public string? Password { get; set; }
    }

}
