using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Common
{
    public class AccountClaims
    {
        public string? Email { get;set; }

        public string? FirstName { get;set; }

        public int? Id { get;set; }

        public int? RoleId { get;set; }

        public int? CampusId { get; set; }
    }
}
