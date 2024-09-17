using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Common
{
    public class AuthenticationResponse
    {
        public bool IsSuccessful { get; set; }

        public string? Message { get; set; }

        public string? Token { get; set; }
    }
}
