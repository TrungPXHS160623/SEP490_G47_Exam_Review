using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Request
{
    public class UserImportRequest
    {
        public string Mail { get; set; } = null!;
        /*
        public string? CampusName { get; set; }

        public string? RoleName { get; set; }
        */
        public string? FullName { get; set; } 
        public string? PhoneNumber { get; set; } 
        public string? EmailFe { get; set; } = "Unknown";
        public string? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }
    }
}
